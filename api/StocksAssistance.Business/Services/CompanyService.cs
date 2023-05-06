using Microsoft.EntityFrameworkCore;
using StocksAssistance.Business.Integrations.DataProviders.Yahoo;
using StocksAssistance.Business.Integrations.DataProviders.Yahoo.ResponseDtos.v7;
using StocksAssistance.Common.DTOs;
using StocksAssistance.Common.Enums;
using StocksAssistance.EF.Models;
using StocksAssistance.EF.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StocksAssistance.Business.Services
{
    public class CompanyService
    {
        CompanyRepository companyRepository;    

        public CompanyService(CompanyRepository companyRepository)
        {
            this.companyRepository = companyRepository;
        }

        public async Task AddCompanies(List<CompanySetupDto> companySetupDtos)
        {
            List<string> symbols = new List<string>();
            foreach (var companySetupDto in companySetupDtos)
            {
                if (companySetupDto.Attributes.Any(a => a.Type == CompanyAttributeType.YahooSymbol))
                {
                    CompanyAttributeDto attribute = companySetupDto.Attributes.First(a => a.Type == CompanyAttributeType.YahooSymbol);
                    Company company = await companyRepository.Get(attribute.Value, attribute.Type);
                    if (company == null) 
                    { 
                        symbols.Add(attribute.Value);
                    }
                }  
            }
            
            if(symbols.Any())
            {
                List<Company> companiesToAdd = new List<Company>();

                QuoteRoot? quote = await YahooApi.GetCompaniesQuotes(symbols);

                if (quote != null && quote.quoteResponse.error == null)
                {
                    foreach (var result in quote.quoteResponse.result)
                    {
                        var dtoAttrs = companySetupDtos.First(c => c.Attributes
                                    .Any(a => a.Type == CompanyAttributeType.YahooSymbol
                                        && a.Value == result.symbol)).Attributes;
                        var dtoTags = companySetupDtos.First(c => c.Attributes
                                    .Any(a => a.Type == CompanyAttributeType.YahooSymbol 
                                        && a.Value == result.symbol)).Tags;

                        Company company = new Company
                        {
                            Name = result.longName,
                            Price = result.regularMarketPrice,
                            MarketCap = result.marketCap/1000000000,
                            LTM_PE = result.trailingPE,
                            NTM_PE = result.forwardPE,
                            PriceBookRatio = result.priceToBook,
                            DividendYield = result.trailingAnnualDividendYield,
                            Last52WeekHigh = result.fiftyTwoWeekHigh,
                            Last52WeekLow = result.fiftyTwoWeekLow,                           
                            Attributes = dtoAttrs.Select(a => new CompanyAttribute { Type = a.Type, Value = a.Value }).ToList(),
                            Tags = dtoTags.Select(t => new CompanyTag { Name = t.Name, Type = t.Type }).ToList()
                        };
                        
                        companiesToAdd.Add(company);
                    }
                }
                else
                {
                    // We can save a log here
                }

                foreach (Company company in companiesToAdd)
                {
                    QuoteSummaryRoot? quoteSummaryRoot = await YahooApi.GetCompanyModules(company.Attributes.First(a => a.Type == CompanyAttributeType.YahooSymbol).Value,
                    new List<string> { "financialData", "assetProfile" });

                    if (quoteSummaryRoot != null && quoteSummaryRoot.quoteSummary != null && quoteSummaryRoot.quoteSummary.error == null 
                        && quoteSummaryRoot.quoteSummary.result.Any() && quoteSummaryRoot.quoteSummary.result.First().assetProfile != null)
                    {
                        UpdateCompanyFromYahooModules(company, quoteSummaryRoot.quoteSummary);
                    }
                    else
                    {
                        // We can save a log here
                    }

                    await companyRepository.Add(company);
                }
            }
        }

        public async Task<Company> GetCompany(int id)
        {
            return await companyRepository.Get(id);
        }

        public async Task<IEnumerable<Company>> GetCompanies()
        {
            return await companyRepository.GetAll().ToListAsync();
        }

        private void UpdateCompanyFromYahooModules(Company company, QuoteSummary quoteSummary)
        {
            // AssetProfile
            var assetProfile = quoteSummary.result.First().assetProfile;
            company.Description = assetProfile.longBusinessSummary?.Length > 5000 ? assetProfile.longBusinessSummary?.Substring(0, 4999)
                : assetProfile.longBusinessSummary;
            company.Sector = assetProfile.sector;
            company.Industry = assetProfile.industry;
            company.Country = assetProfile.country;
            company.Website = assetProfile.website;

            // FinancialData
            var financialData = quoteSummary.result.First().financialData;
            company.ROE = financialData.returnOnEquity.raw;
        }
    }   
}
