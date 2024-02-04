using EmployeeWebService.Domain.Entities;

namespace EmployeeWebService.Domain.Interfaces;

public interface IRenewableEmployeeField
{
    public int EmployeeId { get; set; }
    public IEmployeeFieldChanges Changes { get; set; }
}

public interface IEmployeeFieldChanges
{
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? Phone { get; set; }
    public int? DepartmentId { get; set; }
    public Passport? Passport { get; set; }
}

