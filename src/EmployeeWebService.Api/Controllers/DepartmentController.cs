using EmployeeWebService.Domain;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeWebService.Api.Controllers;

[ApiController]
[Route("api/departments")]
public class DepartmentController : ControllerBase
{
    private readonly IDepartmentRepository _departmentRepository;

    public DepartmentController(IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var departments = await _departmentRepository.GetDepartmentsAsync();
        return Ok(departments);
    }
}
