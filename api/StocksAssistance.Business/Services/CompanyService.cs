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
        CompanyTagRepository companyTagRepository;

        public CompanyService(CompanyRepository companyRepository, CompanyTagRepository companyTagRepository)
        {
            this.companyRepository = companyRepository;
            this.companyTagRepository = companyTagRepository;
        }

        public async Task AddCompanies(List<CompanySetupDto> companySetupDtos)
        {
            List<string> symbols = new List<string>();
            foreach (var companySetupDto in companySetupDtos)
            {
                if (companySetupDto.Attributes.Any(a => a.Type == CompanyAttributeType.YahooSymbol))
                {
                    CompanyAttributeDto yahooSymbol = companySetupDto.Attributes.First(a => a.Type == CompanyAttributeType.YahooSymbol);
                    symbols.Add(yahooSymbol.Value);
                }  
            }
            
            if(symbols.Any())
            {
                QuoteRoot? quote = await YahooApi.GetCompaniesQuotes(symbols);

                if (quote != null && quote.quoteResponse.error == null)
                {
                    foreach (var result in quote.quoteResponse.result)
                    {
                        bool isCompanyNew = false;

                        var dtoAttrs = companySetupDtos.First(c => c.Attributes
                                    .Any(a => a.Type == CompanyAttributeType.YahooSymbol
                                        && a.Value == result.symbol)).Attributes;

                        var dtoTags = companySetupDtos.First(c => c.Attributes
                                    .Any(a => a.Type == CompanyAttributeType.YahooSymbol 
                                        && a.Value == result.symbol)).Tags;

                        Company company = await companyRepository.Get(result.symbol, CompanyAttributeType.YahooSymbol);

                        if (company == null)
                        {
                            isCompanyNew = true;
                            company = new Company
                            {
                                Name = result.longName,
                                Price = result.regularMarketPrice,
                                MarketCap = result.marketCap / 1000000000,
                                LTM_PE = result.trailingPE,
                                NTM_PE = result.forwardPE,
                                PriceBookRatio = result.priceToBook,
                                DividendYield = result.trailingAnnualDividendYield,
                                Last52WeekHigh = result.fiftyTwoWeekHigh,
                                Last52WeekLow = result.fiftyTwoWeekLow
                            };
                        }

                        foreach (var dtoTag in dtoTags)
                        {
                            CompanyTag tag = await companyTagRepository.Get(dtoTag.Name, dtoTag.Type);
                            if (tag == null)
                            {
                                tag = new CompanyTag { Name = dtoTag.Name, Type = dtoTag.Type };
                                await companyTagRepository.Add(tag);
                            }
                         
                            company.Tags.Add(tag);
                        }

                        foreach(var dtoAttr in dtoAttrs)
                        {
                            CompanyAttribute? attr = company.Attributes.FirstOrDefault(a => a.Type == dtoAttr.Type);
                            if (attr == null)
                            {
                                attr = new CompanyAttribute { Type = dtoAttr.Type, Value = dtoAttr.Value };
                                company.Attributes.Add(attr);
                            }
                            else if(attr.Value != dtoAttr.Value)
                            {
                                attr.Value = dtoAttr.Value;
                            }
                        }


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

                        if (isCompanyNew)
                        {
                            await companyRepository.Add(company);
                        }
                        else
                        {
                            await companyRepository.Update(company);
                        }
                    }
                }
                else
                {
                    // We can save a log here
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

        public async Task UpdateCompanies()
        {
            DateTime now = DateTime.UtcNow;
            var companies = await companyRepository.GetAll().Where(c => c.Attributes.Any(a => a.Type == CompanyAttributeType.YahooSymbol) &&
                (!c.Logs.Any(l => l.Type == CompanyLogType.Updated
                || c.Logs.Any(l => l.Type == CompanyLogType.Updated && l.TimeStamp < now.AddHours(-1)))))
                .ToListAsync();

            List<string> symbols = new List<string>();

            foreach (var company in companies)
            {             
                symbols.Add(company.Attributes.First(a => a.Type == CompanyAttributeType.YahooSymbol).Value);              
            }

            if (symbols.Any())
            {
                QuoteRoot? quote = await YahooApi.GetCompaniesQuotes(symbols);

                if (quote != null && quote.quoteResponse.error == null)
                {
                    foreach (var result in quote.quoteResponse.result)
                    {
                        Company company = companies.First(c => c.Attributes
                                                           .Any(a => a.Type == CompanyAttributeType.YahooSymbol
                                                                  && a.Value == result.symbol));
                        company.Price = result.regularMarketPrice;
                        company.MarketCap = result.marketCap / 1000000000;
                        company.LTM_PE = result.trailingPE;
                        company.NTM_PE = result.forwardPE;
                        company.PriceBookRatio = result.priceToBook;
                        company.DividendYield = result.trailingAnnualDividendYield;
                        company.Last52WeekHigh = result.fiftyTwoWeekHigh;
                        company.Last52WeekLow = result.fiftyTwoWeekLow;
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

                        CompanyLog log = company.Logs.FirstOrDefault(l => l.Type == CompanyLogType.Updated);
                        if(log != null)
                        {
                            log.TimeStamp = now;
                        }
                        else
                        {
                            log = new CompanyLog { TimeStamp = now, Type = CompanyLogType.Updated };
                            company.Logs.Add(log);
                        }

                        await companyRepository.Update(company);
                    }
                }
            }
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
