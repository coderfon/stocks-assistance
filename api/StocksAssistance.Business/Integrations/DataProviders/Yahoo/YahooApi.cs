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
        public static async Task<QuoteRoot> GetCompany(string symbol)
        {
            HttpClient client = new HttpClient();

            var response = await client.GetAsync($"https://query2.finance.yahoo.com/v7/finance/quote?symbols={symbol}");
            var json = await response.Content.ReadAsStringAsync();

            QuoteRoot quoteRoot = string.IsNullOrEmpty(json) ? new QuoteRoot() : JsonConvert.DeserializeObject<QuoteRoot>(json);

            //CompanyDto company = null;
            return quoteRoot;
        }
    }
}
