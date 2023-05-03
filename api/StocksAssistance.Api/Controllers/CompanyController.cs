using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StocksAssistance.Business.Integrations.DataProviders.Yahoo;
using StocksAssistance.Business.Integrations.DataProviders.Yahoo.ResponseDtos.v7;

namespace StocksAssistance.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        [HttpGet(Name = "GetCompany")]
        public async Task<QuoteRoot> Get()
        {
            var result = await YahooApi.GetCompany("AAPL");

            return result;
        }
    }
}
