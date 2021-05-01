using SmartZone.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SmartZone.Contracts
{
    public interface ICustomerRepository : IRepositoryBase<Customer>
    {
        IQueryable<Customer> FindAllIncludesDeleted(Expression<Func<Customer, bool>> predicate = null);
        Task<Customer> FindByGuidAsync(string guid);
    }
}
