using SmartZone.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SmartZone.Contracts
{
    public interface IEmployeeRepository : IRepositoryBase<Employee>
    {
        IQueryable<Employee> FindAll(int StoreId, Expression<Func<Employee, bool>>? predicate = null);
    }
}
