using SmartZone.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SmartZone.Contracts
{
    public interface IFoodRepository : IRepositoryBase<Food>
    {
        public void DeleteRange(ICollection<Food> Foods);
        IQueryable<Food> FindAll(int storeId, Expression<Func<Food, bool>> predicate = null);
        public IQueryable<Food> FindFoodByOrderId(int storeId, Expression<Func<Food, bool>>? predicate = null);

    }
}
