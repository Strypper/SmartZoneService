using Microsoft.EntityFrameworkCore;
using SmartZone.Contracts;
using SmartZone.Entities;
using SmartZone.Repositories.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace SmartZone.Repositories
{
    public class StoreRepository : RepositoryBase<Store>, IStoreRepository
    {
        public StoreRepository(SmartZoneContext szc) : base(szc) { }

        public override IQueryable<Store> FindAll(Expression<Func<Store, bool>>? predicate)
            => _dbSet.Where(s => !s.IsDeleted).WhereIf(predicate != null, predicate!);

        public IQueryable<Store> FindAllBySmartZoneId(int smartZoneId, Expression<Func<Store, bool>>? predicate)
            => _dbSet.Where(sto => sto.SmartZoneId == smartZoneId).WhereIf(predicate != null, predicate!);

        public IQueryable<Store> FindAllIncludesDeleted(Expression<Func<Store, bool>>? predicate = null)
            => _dbSet.WhereIf(predicate != null, predicate!);

        public async Task<IEnumerable<Store>> FindNearestStore(int smartZoneId, string location, CancellationToken cancellationToken)
            => await FindAll(null).ToListAsync(cancellationToken);
            
    }
}
