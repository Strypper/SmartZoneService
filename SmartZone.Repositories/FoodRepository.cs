using SmartZone.Entities;
using SmartZone.Contracts;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;
using System.Threading;
using Microsoft.EntityFrameworkCore;

namespace SmartZone.Repositories
{
    public class FoodRepository : RepositoryBase<Food>, IFoodRepository
    {
        public FoodRepository(SmartZoneContext szc) : base(szc) { }

        public void DeleteAllByStore(ICollection<Food> Foods)
        {
            _dbSet.RemoveRange(Foods);
        }

        public async Task<ICollection<Food>> FindFoodByStoreId(string storeId, CancellationToken cancellationToken) 
            => await FindAll(f => f.StoreId == storeId).ToListAsync(cancellationToken);
    }
}
