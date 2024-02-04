using Asp.Versioning;
using EmployeeWebService.Application;
using EmployeeWebService.Application.Models;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeWebService.Api.Controllers;

/// <summary>
/// Группа методов для работы с сотрудниками
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/employees")]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _employeeService;
    private readonly IValidator<EmployeeModel> _employeeValidator;
    private readonly IValidator<EmployeeFieldChanges> _employeeFieldValidator;

    public EmployeeController(
        IEmployeeService employeeService,
        IValidator<EmployeeModel> employeeValidator,
        IValidator<EmployeeFieldChanges> employeeFieldValidator)
    {
        _employeeService = employeeService;
        _employeeValidator = employeeValidator;
        _employeeFieldValidator = employeeFieldValidator;
    }


    /// <summary>
    /// Получить сотрудников
    /// </summary>
    /// <remarks>
    /// Получение сотрудников со всеми полями, а так же фильтрацией по компаниям и отделениям
    /// 
    /// GET (Вернуть все записи по сотрудникам)
    ///     
    ///     /api/v1/employees
    /// 
    /// GET (Два параметра вместе для выборки по компании и отделу)
    ///     
    ///     /api/v1/employees?company_id=1
    ///     /api/v1/employees?department_id=1
    ///     
    /// </remarks>
    /// <param name="companyId">Фильтрация по компании</param>
    /// <param name="departmentId">Фильтрация по отделу</param>
    /// <returns>Список сотрудников EmployeeView</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesErrorResponseType(typeof(ProblemDetails))]
    public async Task<IActionResult> GetAll
        ([FromQuery(Name = "company_id")] int? companyId = null,
         [FromQuery(Name = "department_id")] int? departmentId = null)
    {
        if (companyId is null & departmentId is null)
            return Ok(await _employeeService.GetEmployeesAsync());

        return Ok(await _employeeService.GetEmployeesByFilterAsync(new EmployeeQuery()
        {
            CompanyId = companyId,
            DepartmentId = departmentId
        }));
    }

    /// <summary>
    /// Получить сотрудника по id
    /// </summary>
    /// <remarks>
    /// GET
    ///     
    ///     /api/v1/employees/1    
    /// 
    /// </remarks>
    /// <param name="id">Id сотрудника</param>
    /// <returns>EmployeeView</returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesErrorResponseType(typeof(ProblemDetails))]
    public async Task<IActionResult> GetById(int id)
        => Ok(await _employeeService.GetByIdAsync(id));

    /// <summary>
    /// Добавить нового сотрудника
    /// </summary>
    /// <remarks>
    /// POST
    /// 
    ///     {
    ///         "name" : "Kirill",
    ///         "surname" : "Avoskin",
    ///         "phone" : "+78952545353",
    ///         "departmentId" : "1",
    ///         "passportType" : "ТН",
    ///         "passportNumber" : "456-2346"
    ///     }
    /// 
    /// </remarks>
    /// <param name="employee"></param>
    /// <returns>Id созданного сотрудника</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesErrorResponseType(typeof(ProblemDetails))]
    public async Task<IActionResult> Create(EmployeeModel employee)
    {
        ValidationResult result = await _employeeValidator.ValidateAsync(employee);
        if (!result.IsValid)
            return BadRequest(
                new
                {
                    errors = result.Errors
                                    .Select(e => $"{e.PropertyName}: {e.ErrorMessage}")
                });
        var employeeId = await _employeeService.CreateEmployeeAsync(employee);
        return Created("employees", new { employeeId = employeeId });
    }

    /// <summary>
    /// Частично обновить данные сотрудника
    /// </summary>
    /// <remarks>
    /// PATH
    ///     
    ///     /api/v1/employees/1
    ///     
    ///     Body
    ///     {
    ///         "name" : "Nikita"
    ///         //любые поля типа EmployeeFieldChanges
    ///     }
    /// 
    /// * Обязательно проверяйте правильность введенных данных 
    /// и полей которые хотите изменить!
    /// </remarks>
    /// <param name="id"></param>
    /// <param name="changes"></param>
    /// <returns></returns>
    [HttpPatch("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesErrorResponseType(typeof(ProblemDetails))]
    public async Task<IActionResult> Update(int id, EmployeeFieldChanges changes)
    {
        var changesValidaton = await _employeeFieldValidator.ValidateAsync(changes);
        if (!changesValidaton.IsValid)
            return BadRequest(
                    new
                    {
                        errors = changesValidaton.Errors
                                        .Select(e => $"{e.PropertyName}: {e.ErrorMessage}")
                    });

        var updatedCount = await _employeeService.UpdateAsync(new EmployeeUpdateModel(id, changes));
        return Ok(new { updated = updatedCount });
    }

    /// <summary>
    /// Удалить сотрудника
    /// </summary>
    /// <remarks>
    /// DELETE
    ///  
    ///     /api/v1/employees/1
    /// 
    /// </remarks>
    /// <param name="id">Id сотрудника</param>
    /// <returns></returns>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesErrorResponseType(typeof(ProblemDetails))]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _employeeService.DeleteEmployeeAsync(id);
        return Ok(new { deleted = deleted });
    }

}
