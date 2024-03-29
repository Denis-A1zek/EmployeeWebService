﻿using EmployeeWebService.Domain.Aggregates;
using EmployeeWebService.Domain.Entities;
using EmployeeWebService.Domain.Interfaces;

namespace EmployeeWebService.Domain;

public interface IEmployeeRepository : IBaseRepository
{  
    Task<EmployeeAggregate> GetById(int id);    
    Task<int> UpdateAsync(IRenewableEmployeeField updatedEmployee);
    Task<IEnumerable<EmployeeAggregate>> GetEmployesByFilter(int? companyId, int? departmentId);
    Task<IEnumerable<EmployeeAggregate>> GetEmployeesAsync();
    Task<int> DeleteAsync(int id);
    Task<int> CreateAsync(Employee employee);
}
