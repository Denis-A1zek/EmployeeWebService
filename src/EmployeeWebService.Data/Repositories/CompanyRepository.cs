using Dapper;
using EmployeeWebService.Domain;
using EmployeeWebService.Domain.Entities;

namespace EmployeeWebService.Data.Repositories;

internal class CompanyRepository : BaseRepository, ICompanyRepository
{
    public CompanyRepository(DapperContext context) : base(context)
    {
    }

    public async Task<IReadOnlyCollection<Company>> GetCompanies()
    {
        var query = "SELECT * FROM companies";
        using var connection = Context.CreateConnection();

        var companies = await connection.QueryAsync<Company>(query);
        return companies.ToList();
    }
}
