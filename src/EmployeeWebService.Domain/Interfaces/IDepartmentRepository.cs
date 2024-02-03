using EmployeeWebService.Domain.Aggregates;

namespace EmployeeWebService.Domain;

public interface IDepartmentRepository
{
    Task<IReadOnlyCollection<DepartmentAggregate>> GetDepartmentsAsync();
}
