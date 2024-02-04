using EmployeeWebService.Domain.Entities;

namespace EmployeeWebService.Domain.Interfaces;

public interface IRenewableEmployee
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; } 
    public string? Phone { get; set; } 
    public int? DepartmentId { get; set; }
    public Passport? Passport { get; set; } 
}
