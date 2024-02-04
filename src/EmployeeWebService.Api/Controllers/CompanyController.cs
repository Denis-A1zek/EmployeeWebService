using EmployeeWebService.Domain;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeWebService.Api.Controllers;

[ApiController]
[Route("api/companies")]
public class CompanyController : ControllerBase
{
    private readonly ICompanyRepository _companyRepository;

    public CompanyController(ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var companies = await _companyRepository.GetCompaniesAsync();
        return Ok(companies);
    }
}
