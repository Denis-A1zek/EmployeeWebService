using EmployeeWebService.Domain.Aggregates;
using EmployeeWebService.Domain.Entities;
using EmployeeWebService.Domain.Interfaces;

namespace EmployeeWebService.Domain;

public interface IDepartmentRepository : IBaseRepository
{
    Task<IReadOnlyCollection<DepartmentAggregate>> GetDepartmentsAsync();
}
