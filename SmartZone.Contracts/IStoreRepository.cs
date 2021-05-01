using SmartZone.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace SmartZone.Contracts
{
    public interface IStoreRepository : IRepositoryBase<Store>
    {
        public IQueryable<Store> FindAllBySmartZoneId(int smartZoneId, Expression<Func<Store, bool>>? predicate = null);
        IQueryable<Store> FindAllIncludesDeleted(Expression<Func<Store, bool>> predicate = null);
        public Task<IEnumerable<Store>> FindNearestStore(int smartZoneId, string location, CancellationToken cancellationToken);
    }
}
