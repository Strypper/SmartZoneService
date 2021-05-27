using Microsoft.EntityFrameworkCore;
using SmartZone.Contracts;
using SmartZone.Entities;
using SmartZone.Repositories.Extensions;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SmartZone.Repositories
{
    public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
    {
        public CustomerRepository(SmartZoneContext szc) : base(szc) { }

        public override IQueryable<Customer> FindAll(Expression<Func<Customer, bool>>? predicate)
            => _dbSet.Where(sz => !sz.IsDeleted).WhereIf(predicate != null, predicate!);

        public IQueryable<Customer> FindAllIncludesDeleted(Expression<Func<Customer, bool>>? predicate = null)
            => _dbSet.WhereIf(predicate != null, predicate!);

        public async Task<Customer?> FindByGuidAsync(string guid)
            => await FindAll(cus => cus.Guid == guid).FirstOrDefaultAsync();
    }
}
