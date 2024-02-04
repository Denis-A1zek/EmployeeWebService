using Asp.Versioning;
using EmployeeWebService.Domain;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeWebService.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/companies")]
public class CompanyController : ControllerBase
{
    private readonly ICompanyRepository _companyRepository;

    public CompanyController(ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;
    }

    /// <summary>
    /// Получить компании
    /// </summary>
    /// <remarks>
    /// GET
    /// 
    ///     /api/v1/companies
    /// 
    /// </remarks>
    /// <returns>Список компаний</returns>
    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(await _companyRepository.GetCompaniesAsync());
}
