using System;
using ESZ = SmartZone.Entities;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SmartZone.Contracts
{
    public interface ISmartZoneRepository : IRepositoryBase<ESZ.SmartZone>
    {
        IQueryable<ESZ.SmartZone> FindAllIncludesDeleted(Expression<Func<ESZ.SmartZone, bool>>? predicate);
        Task<ESZ.SmartZone?> FindByGuidAsync(string guid);
        IQueryable<ESZ.SmartZone> FindTop(int amount);
    }
}
