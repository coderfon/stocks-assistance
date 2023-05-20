using Microsoft.EntityFrameworkCore;
using StocksAssistance.Business.Integrations.DataProviders.Yahoo;
using StocksAssistance.Business.Integrations.DataProviders.Yahoo.ResponseDtos.v7;
using StocksAssistance.Common.DTOs;
using StocksAssistance.Common.Enums;
using StocksAssistance.EF.Models;
using StocksAssistance.EF.Repositories;
using Attribute = StocksAssistance.EF.Models.Attribute;

namespace StocksAssistance.Business.Services
{
    public class CompanyService
    {
        CompanyRepository companyRepository;
        SectorRepository sectorRepository;
        IndustryRepository industryRepository;
        TagRepository tagRepository;

        public CompanyService(CompanyRepository companyRepository, SectorRepository sectorRepository, IndustryRepository industryRepository, TagRepository tagRepository)
        {
            this.companyRepository = companyRepository;
            this.sectorRepository = sectorRepository;
            this.industryRepository = industryRepository;
            this.tagRepository = tagRepository;
        }

        public async Task AddCompanies(List<CompanySetupDto> companySetupDtos)
        {
            List<string> symbols = new List<string>();
            foreach (var companySetupDto in companySetupDtos)
            {
                if (companySetupDto.Attributes.Any(a => a.Type == AttributeType.YahooSymbol))
                {
                    CompanyAttributeDto yahooSymbol = companySetupDto.Attributes.First(a => a.Type == AttributeType.YahooSymbol);
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
                                    .Any(a => a.Type == AttributeType.YahooSymbol
                                        && a.Value == result.symbol)).Attributes;

                        var dtoTags = companySetupDtos.First(c => c.Attributes
                                    .Any(a => a.Type == AttributeType.YahooSymbol 
                                        && a.Value == result.symbol)).Tags;

                        Company company = await companyRepository.Get(result.symbol, AttributeType.YahooSymbol);

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
                            Tag tag = await tagRepository.Get(dtoTag.Name, dtoTag.Type);
                            if (tag == null)
                            {
                                tag = new Tag { Name = dtoTag.Name, Type = dtoTag.Type };
                                await tagRepository.Add(tag);
                            }
                         
                            company.Tags.Add(tag);
                        }

                        foreach(var dtoAttr in dtoAttrs)
                        {
                            Attribute attr = company.Attributes.FirstOrDefault(a => a.Type == dtoAttr.Type);
                            if (attr == null)
                            {
                                attr = new Attribute { Type = dtoAttr.Type, Value = dtoAttr.Value };
                                company.Attributes.Add(attr);
                            }
                            else if(attr.Value != dtoAttr.Value)
                            {
                                attr.Value = dtoAttr.Value;
                            }
                        }


                        QuoteSummaryRoot? quoteSummaryRoot = await YahooApi.GetCompanyModules(company.Attributes.First(a => a.Type == AttributeType.YahooSymbol).Value,
                            new List<string> { "financialData", "assetProfile" });

                        if (quoteSummaryRoot != null && quoteSummaryRoot.quoteSummary != null && quoteSummaryRoot.quoteSummary.error == null
                            && quoteSummaryRoot.quoteSummary.result.Any() && quoteSummaryRoot.quoteSummary.result.First().assetProfile != null)
                        {
                            await UpdateCompanyFromYahooModules(company, quoteSummaryRoot.quoteSummary);
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

        public async Task<CompanyOptionsDto> GetCompaniesOptions()
        {
            CompanyOptionsDto result = new CompanyOptionsDto 
            {
                Sectors = sectorRepository.GetAll().Select(s => ToDto(s)).OrderBy(s => s.Name).ToList(),
                Industries = industryRepository.GetAll().Select(s => ToDto(s)).OrderBy(s => s.Name).ToList(),
                Tags =  tagRepository.GetAll().Select(t => ToDto(t)).OrderBy(t => t).ToList()
            };

            return result;
        }

        public async Task<IEnumerable<CompanyDto>> GetCompanies()
        {
            List<CompanyDto> result = new List<CompanyDto>();
            var companies = await companyRepository.GetAll().ToListAsync();
            foreach (var company in companies)
            {
                result.Add(ToDto(company));
            }
            return result;
        }

        public async Task UpdateCompanies()
        {
            DateTime now = DateTime.UtcNow;
            var companies = await companyRepository.GetAll().Where(c => c.Attributes.Any(a => a.Type == AttributeType.YahooSymbol) &&
                (!c.Logs.Any(l => l.Type == LogType.Updated
                || c.Logs.Any(l => l.Type == LogType.Updated && l.TimeStamp < now.AddHours(-1)))))
                .ToListAsync();

            List<string> symbols = new List<string>();

            foreach (var company in companies)
            {             
                symbols.Add(company.Attributes.First(a => a.Type == AttributeType.YahooSymbol).Value);              
            }

            if (symbols.Any())
            {
                QuoteRoot? quote = await YahooApi.GetCompaniesQuotes(symbols);

                if (quote != null && quote.quoteResponse.error == null)
                {
                    foreach (var result in quote.quoteResponse.result)
                    {
                        Company company = companies.First(c => c.Attributes
                                                           .Any(a => a.Type == AttributeType.YahooSymbol
                                                                  && a.Value == result.symbol));
                        company.Price = result.regularMarketPrice;
                        company.MarketCap = result.marketCap / 1000000000;
                        company.LTM_PE = result.trailingPE;
                        company.NTM_PE = result.forwardPE;
                        company.PriceBookRatio = result.priceToBook;
                        company.DividendYield = result.trailingAnnualDividendYield;
                        company.Last52WeekHigh = result.fiftyTwoWeekHigh;
                        company.Last52WeekLow = result.fiftyTwoWeekLow;
                        QuoteSummaryRoot? quoteSummaryRoot = await YahooApi.GetCompanyModules(company.Attributes.First(a => a.Type == AttributeType.YahooSymbol).Value,
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

                        Log log = company.Logs.FirstOrDefault(l => l.Type == LogType.Updated);
                        if(log != null)
                        {
                            log.TimeStamp = now;
                        }
                        else
                        {
                            log = new Log { TimeStamp = now, Type = LogType.Updated };
                            company.Logs.Add(log);
                        }

                        await companyRepository.Update(company);
                    }
                }
            }
        }

        private CompanyDto ToDto(Company company)
        {
            CompanyDto dto = new CompanyDto
            { 
                Id = company.Id,
                Name = company.Name,
                Sector = ToDto(company.Sector),
                Industry = ToDto(company.Industry)
            };

            return dto;
        }

        private IndustryDto ToDto(Industry industry)
        {
            IndustryDto dto = new IndustryDto
            {
                Id = industry.Id,
                Name = industry.Name,
                SectorId = industry.SectorId,
            };
            return dto;
        }
        private SectorDto ToDto(Sector sector)
        {
            SectorDto dto = new SectorDto
            {
                Id = sector.Id,
                Name = sector.Name,
                Industries = sector.Industries.Select(i => ToDto(i)).ToList()
            };
            return dto;
        }

        private TagDto ToDto(Tag tag)
        {
            TagDto dto = new TagDto
            {
                Id = tag.Id,
                Name = tag.Name,
                Type = tag.Type
            };
            return dto;
        }

        private async Task UpdateCompanyFromYahooModules(Company company, QuoteSummary quoteSummary)
        {
            // AssetProfile
            var assetProfile = quoteSummary.result.First().assetProfile;
            company.Description = assetProfile.longBusinessSummary?.Length > 5000 ? assetProfile.longBusinessSummary?.Substring(0, 4999)
                : assetProfile.longBusinessSummary;
            company.Country = assetProfile.country;
            company.Website = assetProfile.website;

            string sectorName = string.IsNullOrEmpty(assetProfile.sector) ? "Unknown" : assetProfile.sector;
            string industryName = string.IsNullOrEmpty(assetProfile.industry) ? "Unknown" : assetProfile.industry;

            Sector sector = await sectorRepository.Get(sectorName);
            if (sector == null)
            {
                sector = new Sector { Name = sectorName };
                await sectorRepository.Add(sector);
            }

            Industry industry = await industryRepository.Get(sector.Id, industryName);
            if (industry == null)
            {
                industry = new Industry { Name = industryName, SectorId = sector.Id };
                await industryRepository.Add(industry);
            }

            company.Sector = sector;
            company.Industry = industry;

            // FinancialData
            var financialData = quoteSummary.result.First().financialData;
            company.ROE = financialData.returnOnEquity.raw;
        }
    }   
}
