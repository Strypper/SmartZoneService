using SmartZone.Entities;
using SmartZone.Contracts;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using SmartZone.Repositories.Extensions;

namespace SmartZone.Repositories
{
    public class FoodRepository : RepositoryBase<Food>, IFoodRepository
    {
        public FoodRepository(SmartZoneContext szc) : base(szc) { }

        public IQueryable<Food> FindAll(int storeId, Expression<Func<Food, bool>>? predicate = null)
            => _dbSet.Where(food => food.StoreId == storeId).WhereIf(predicate != null, predicate!);

        public IQueryable<Food> FindFoodByOrderId(int orderId, Expression<Func<Food, bool>>? predicate = null)
            => _dbSet.Where(food => food!.Orders.Any(ord => ord.Id == orderId)).WhereIf(predicate != null, predicate!);

        public IQueryable<Food> FindFoodByName(string name, Expression<Func<Food, bool>>? predicate = null)
        {
            return FindAll(predicate);
        }


        public void DeleteRange(ICollection<Food> foods)
        {
            _dbSet.RemoveRange(foods);
        }

    }
}
