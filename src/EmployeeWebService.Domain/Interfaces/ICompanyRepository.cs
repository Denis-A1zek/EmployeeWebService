using EmployeeWebService.Domain.Entities;
using EmployeeWebService.Domain.Interfaces;

namespace EmployeeWebService.Domain;

public interface ICompanyRepository : IBaseRepository
{
    Task<IReadOnlyCollection<Company>> GetCompaniesAsync();
}
