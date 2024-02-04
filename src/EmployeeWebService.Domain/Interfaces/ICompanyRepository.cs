using EmployeeWebService.Domain.Entities;

namespace EmployeeWebService.Domain;

public interface ICompanyRepository
{
    Task<IReadOnlyCollection<Company>> GetCompaniesAsync();
}
