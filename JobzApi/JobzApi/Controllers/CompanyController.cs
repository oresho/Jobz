using JobzApi.Models.Dtos.Requests;
using JobzApi.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobzApi.Controllers
{
    [Route("api/v1/companies")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpPost]
        public async Task<IActionResult> AddCompany([FromBody] CreateCompanyRequest companyRequest)
        {
            var response = await _companyService.CreateCompanyAsync(companyRequest);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetCompaniesByName([FromQuery] string name)
        {
            var response = await _companyService.GetCompaniesByNameAsync(name);
            return Ok(response);
        }
    }
}
