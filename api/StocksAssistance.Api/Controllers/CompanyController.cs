using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StocksAssistance.Business.Integrations.DataProviders.Yahoo;
using StocksAssistance.Business.Integrations.DataProviders.Yahoo.ResponseDtos.v7;
using StocksAssistance.Business.Services;
using StocksAssistance.Common.DTOs;
using StocksAssistance.EF.Models;

namespace StocksAssistance.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private CompanyService companyService;

        public CompanyController(CompanyService companyService) 
        {
            this.companyService = companyService;
        }

        /*
        [HttpGet("{symbol}")]
        public async Task<QuoteRoot> Get(string symbol)
        {
            //var result = await YahooApi.GetCompany(symbol);

            return (;
        }
        */

        [HttpPost()]
        public async Task<IActionResult> Setup(List<CompanySetupDto> companies)
        {
            try
            {
                await companyService.AddCompanies(companies);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut()]
        public async Task<IActionResult> Update()
        {
            try
            {
                await companyService.UpdateCompanies();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
