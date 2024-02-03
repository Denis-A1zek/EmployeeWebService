using EmployeeWebService.Domain.Entities;

namespace EmployeeWebService.Domain;

public interface IEmployeeRepository
{
    Task<int> CreateEmployeeAsync(Employee employee);
}
