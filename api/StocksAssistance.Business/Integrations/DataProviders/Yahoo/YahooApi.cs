using Newtonsoft.Json;
using StocksAssistance.Business.Integrations.DataProviders.Yahoo.ResponseDtos.v7;
using StocksAssistance.Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StocksAssistance.Business.Integrations.DataProviders.Yahoo
{
    public static class YahooApi
    {
        // TODO: Add GetCompany method that makes a GET request to Yahoo API and returns a CompanyDto object
        public static async Task<QuoteRoot?> GetCompaniesQuotes(List<string> symbols)
        {
            string symbolsString = string.Join(",", symbols);
            HttpClient client = new HttpClient();

            string version = "v7";

            HttpResponseMessage response = await client.GetAsync($"https://query2.finance.yahoo.com/{version}/finance/quote?symbols={symbolsString}");

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                version = "v6";
                response = await client.GetAsync($"https://query2.finance.yahoo.com/{version}/finance/quote?symbols={symbolsString}");
            }

            string json = await response.Content.ReadAsStringAsync();

            QuoteRoot? quoteRoot = string.IsNullOrEmpty(json) ? null : JsonConvert.DeserializeObject<QuoteRoot>(json);

            return quoteRoot;
        }

        public static async Task<QuoteSummaryRoot?> GetCompanyModules(string symbol, List<string> modules)
        {
            string modulesString = string.Join(",", modules);
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"https://query1.finance.yahoo.com/v10/finance/quoteSummary/{symbol}?modules={modulesString}");
            string json = await response.Content.ReadAsStringAsync();
            QuoteSummaryRoot? quoteSummary = string.IsNullOrEmpty(json) ? null : JsonConvert.DeserializeObject<QuoteSummaryRoot>(json);
            return quoteSummary;
        }
    }
}
