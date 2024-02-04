using EmployeeWebService.Application.Models;
using EmployeeWebService.Domain;

namespace EmployeeWebService.Application.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IDepartmentRepository _departmentRepository;

    public EmployeeService
        (IEmployeeRepository employeeRepository, 
        IDepartmentRepository departmentRepository)
    {
        _employeeRepository = employeeRepository;
        _departmentRepository = departmentRepository;
    }

    public async Task<int> CreateEmployeeAsync(EmployeeModel employee)
    {
        await ThrowExceptionIfDepartmentNotExsist(employee.DepartmentId);

        var employeeEntity = employee.ToEmployee();
        var employeeId = await _employeeRepository.CreateAsync(employeeEntity);
        return employeeId;
    }

    public async Task<int> UpdateAsync(EmployeeUpdateModel employee)
    {
        await ThrowExceptionIfEmployeeNotExsist(employee.EmployeeId);
        return await _employeeRepository.UpdateAsync(employee);
    }

    public async Task<int> DeleteEmployeeAsync(int id)
    {
        await ThrowExceptionIfEmployeeNotExsist(id);
        return await _employeeRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<EmployeeView>> GetEmployeesAsync()
    {
        var employeeList = await _employeeRepository.GetEmployeesAsync();
        
        if(!employeeList.Any() || employeeList is null) 
            return Enumerable.Empty<EmployeeView>();
        
        return employeeList.Select(e => EmployeeView.Convert(e));
    }

    public async Task<IEnumerable<EmployeeView>> GetEmployeesByFilterAsync(EmployeeQuery query)
    {
        var filtreadEmployee 
            = await _employeeRepository
                        .GetEmployesByFilter(query.CompanyId, query.DepartmentId);
        
        if (!filtreadEmployee.Any() || filtreadEmployee is null)
            return Enumerable.Empty<EmployeeView>();

        return filtreadEmployee.Select(e => EmployeeView.Convert(e));
    }

    public async Task<EmployeeView> GetByIdAsync(int id)
    {
        await ThrowExceptionIfEmployeeNotExsist(id);

        var employee = await _employeeRepository.GetById(id);
        return EmployeeView.Convert(employee);
    }

    private async Task ThrowExceptionIfEmployeeNotExsist(int employeeId)
    {
        var exsist = await _employeeRepository.IsExsist("employees", employeeId);
        if (!exsist || employeeId == 0)
            throw new NotFoundException($"Пользователь с id:{employeeId} не найден");
    }

    private async Task ThrowExceptionIfDepartmentNotExsist(int departmentId)
    {
        var exsist = await _employeeRepository.IsExsist("departments", departmentId);
        if (!exsist || departmentId == 0)
            throw new NotFoundException($"Отдел с id:{departmentId} не найден");
    }
}
