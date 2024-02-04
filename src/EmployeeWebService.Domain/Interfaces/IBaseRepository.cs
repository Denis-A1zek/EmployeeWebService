using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeWebService.Domain.Interfaces;

public interface IBaseRepository
{
    Task<bool> IsExsist(string tableName, int entityId);
}
