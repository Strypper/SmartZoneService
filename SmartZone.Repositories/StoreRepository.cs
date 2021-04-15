using SmartZone.Contracts;
using SmartZone.Entities;
using SmartZone.Repositories.Extensions;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SmartZone.Repositories
{
    public class StoreRepository : RepositoryBase<Store>, IStoreRepository
    {
        public StoreRepository(SmartZoneContext szc) : base(szc) { }

        public override IQueryable<Store> FindAll(Expression<Func<Store, bool>>? predicate)
            => _dbSet.Where(s => !s.IsDeleted).WhereIf(predicate != null, predicate!);
    }
}
