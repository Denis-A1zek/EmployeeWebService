using EmployeeWebService.Application;
using EmployeeWebService.Application.Models;
using EmployeeWebService.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeWebService.Api.Controllers;

[ApiController]
[Route("api/employees")]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _employeeService;

    public EmployeeController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpPut]
    public async Task<IActionResult> Update(EmployeeUpdateModel employee)
    {
        var d = await _employeeService.UpdateAsync(employee);
        return Ok(d);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(int? companyId, int? departmentId)
    {
        if (companyId is null & departmentId is null) return Ok(await _employeeService.GetEmployeesAsync());

        return Ok(await _employeeService.GetEmployeesByFilterAsync(new EmployeeQuery()
        {
            CompanyId = companyId,
            DepartmentId = departmentId
        }));
    }

    [HttpPost]
    public async Task<IActionResult> Create(EmployeeModel employee)
        => Ok(await _employeeService.CreateEmployeeAsync(employee));

    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
        => Ok(await _employeeService.DeleteEmployeeAsync(id));

}
