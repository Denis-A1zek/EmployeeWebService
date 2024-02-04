using Asp.Versioning;
using EmployeeWebService.Domain;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeWebService.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/departments")]
public class DepartmentController : ControllerBase
{
    private readonly IDepartmentRepository _departmentRepository;

    public DepartmentController
        (IDepartmentRepository departmentRepository)
            => _departmentRepository = departmentRepository;

    /// <summary>
    /// Получить отделы
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
        => Ok(await _departmentRepository.GetDepartmentsAsync());
}
