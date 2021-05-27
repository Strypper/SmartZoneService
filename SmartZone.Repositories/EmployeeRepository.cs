using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using SmartZone.Contracts;
using SmartZone.Entities;
using SmartZone.Repositories.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ESZ = SmartZone.Entities;

namespace SmartZone.Repositories
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(SmartZoneContext szc) : base(szc) { }

        public IQueryable<Employee> FindAll(int StoreId, Expression<Func<Employee, bool>>? predicate = null)
            => _dbSet.Where(emp => emp.StoreId == StoreId).WhereIf(predicate != null, predicate!);

        public override void Delete(Employee entity)
        {
            entity.IsDeleted = true;
            _dbSet.Update(entity);
        }

        public async Task<Employee?> FindByGuidAsync(string guid, CancellationToken cancellationToken = default)
           => await FindAll(emp => emp.Guid == guid).FirstOrDefaultAsync(cancellationToken);


    }
}
