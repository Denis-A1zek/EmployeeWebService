using EmployeeWebService.Application.Models;

namespace EmployeeWebService.Application;

public interface IEmployeeService
{
    Task<EmployeeView> GetByIdAsync(int id);    
    Task<int> UpdateAsync(EmployeeUpdateModel employee);
    Task<IEnumerable<EmployeeView>> GetEmployeesByFilterAsync(EmployeeQuery query);
    Task<IEnumerable<EmployeeView>> GetEmployeesAsync();
    Task<int> DeleteEmployeeAsync(int id);
    Task<int> CreateEmployeeAsync(EmployeeModel employee);
}
