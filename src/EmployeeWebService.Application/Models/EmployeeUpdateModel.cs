using EmployeeWebService.Domain.Entities;
using EmployeeWebService.Domain.Interfaces;

namespace EmployeeWebService.Application.Models;

public class EmployeeUpdateModel : IRenewableEmployeeField
{
    public EmployeeUpdateModel(int employeeId, IEmployeeFieldChanges changes)
    {
        EmployeeId = employeeId;
        Changes = changes;
    }

    public int EmployeeId { get; set; }
    public IEmployeeFieldChanges Changes { get; set; }
}


public class EmployeeFieldChanges : IEmployeeFieldChanges
{
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? Phone { get; set; }
    public int? DepartmentId { get; set; }
    public string? PassportType { get; set; }
    public string? PassportNumber { get; set; }
}