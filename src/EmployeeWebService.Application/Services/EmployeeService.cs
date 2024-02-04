using EmployeeWebService.Application.Models;
using EmployeeWebService.Domain;
using EmployeeWebService.Domain.Aggregates;
using EmployeeWebService.Domain.Entities;

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
        //var department = await _departmentRepository
        //                            .GetDepartmentByIdAsync(employee.DepartmentId);
        //if (department is null)
        //    throw new NotFoundException($"Entity department with id:{employee.DepartmentId} not found");

        var employeeEntity = employee.ToEmployee();
        var employeeId = await _employeeRepository.CreateAsync(employeeEntity);
        return employeeId;
    }

    public async Task<int> UpdateAsync(EmployeeUpdateModel employee)
    {
        return await _employeeRepository.UpdateAsync(employee);
    }

    public async Task<int> DeleteEmployeeAsync(int id)
        => await _employeeRepository.DeleteAsync(id);

    public async Task<IEnumerable<EmployeeView>> GetEmployeesAsync()
    {
        var employeeList = await _employeeRepository.GetEmployeesAsync();
        
        if(!employeeList.Any() || employeeList is null) 
            return Enumerable.Empty<EmployeeView>();
        
        return employeeList.Select(e => EmployeeView.Convert(e));
    }

    public async Task<IEnumerable<EmployeeView>> GetEmployeesByFilterAsync(EmployeeQuery query)
    {
        var filtreadEmployee = await _employeeRepository.GetEmployesByFilter(query.CompanyId, query.DepartmentId);
        
        if (!filtreadEmployee.Any() || filtreadEmployee is null)
            return Enumerable.Empty<EmployeeView>();

        return filtreadEmployee.Select(e => EmployeeView.Convert(e));
    }
}
