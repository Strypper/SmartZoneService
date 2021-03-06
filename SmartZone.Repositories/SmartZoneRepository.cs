using SmartZone.Contracts;
using ESZ = SmartZone.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using SmartZone.Repositories.Extensions;
using Microsoft.EntityFrameworkCore;

namespace SmartZone.Repositories
{
    public class SmartZoneRepository : RepositoryBase<ESZ.SmartZone>, ISmartZoneRepository  //extends 1 CLASS AND 1 INTERFACE ==> get all methods
    {
        public SmartZoneRepository(ESZ.SmartZoneContext szc) : base(szc) { }

        public override IQueryable<ESZ.SmartZone> FindAll(Expression<Func<ESZ.SmartZone, bool>>? predicate)
            => _dbSet.Where(sz => !sz.IsDeleted).WhereIf(predicate != null, predicate!);

        public IQueryable<ESZ.SmartZone> FindAllIncludesDeleted(Expression<Func<ESZ.SmartZone, bool>>? predicate = null)
            => _dbSet.WhereIf(predicate != null, predicate!);

        public async Task<ESZ.SmartZone?> FindByGuidAsync(string guid)
            => await FindAll(sz => sz.Guid == guid).FirstOrDefaultAsync();

        public override void Delete(ESZ.SmartZone entity)
        {
            entity.IsDeleted = true;
            _dbSet.Update(entity);
        }

        public IQueryable<ESZ.SmartZone> FindTop(int amount) => _dbSet.Take(amount);
    }
}
