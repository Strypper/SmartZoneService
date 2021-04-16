using SmartZone.Entities;
using System.Collections.Generic;

namespace SmartZone.Contracts
{
    public interface IFoodRepository : IRepositoryBase<Food>
    {
        public void DeleteAllByStore(ICollection<Food> Foods);
    }
}
