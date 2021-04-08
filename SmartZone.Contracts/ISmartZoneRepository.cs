using System;
using System.Collections.Generic;
using SZ = SmartZone.Entities;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SmartZone.Contracts
{
    public interface ISmartZoneRepository : IRepositoryBase<SZ.SmartZone>
    {
        IQueryable<SZ.SmartZone> FindAllIncludesDeleted(Expression<Func<SZ.SmartZone, bool>>? predicate);
        Task<SZ.SmartZone?> FindByGuidAsync(string guid);
    }
}
