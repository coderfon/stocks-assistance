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

        [HttpPost("filter")]
        public async Task<IActionResult> Get(CompanySearchFilterDto filter)
        {
            return Ok(await companyService.GetCompanies(filter));
        }

        [HttpGet("{symbol}")]
        public async Task<IActionResult> Get(string symbol)
        {
            return Ok(symbol);
        }

        [HttpGet("{sector}/industries")]
        public async Task<IActionResult> GetIndustries(string sector)
        {
            return Ok(await companyService.GetCompaniesOptions());
        }

        [HttpGet("options")]
        public async Task<IActionResult> GetOptions()
        {
            return Ok(await companyService.GetCompaniesOptions());
        }

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
