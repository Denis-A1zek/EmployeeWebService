using EmployeeWebService.Domain.Aggregates;
using EmployeeWebService.Domain.Entities;

namespace EmployeeWebService.Domain;

public interface IDepartmentRepository
{
    Task<Department> GetDepartmentByIdAsync(int id);
    Task<IReadOnlyCollection<DepartmentAggregate>> GetDepartmentsAsync();
}
