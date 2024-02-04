using EmployeeWebService.Domain.Entities;
using EmployeeWebService.Domain.Interfaces;

namespace EmployeeWebService.Application.Models;

public class EmployeeUpdateModel : IRenewableEmployee
{
    public int Id { get; set; }
    public string? Name { get; set; } = null;
    public string? Surname { get; set; } = null;
    public string? Phone { get; set; } = null;
    public int? DepartmentId { get; set; } = null;
    public Passport? Passport { get; set; } = null;
}
