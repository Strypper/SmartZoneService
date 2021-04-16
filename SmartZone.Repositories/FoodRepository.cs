using SmartZone.Entities;
using SmartZone.Contracts;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SmartZone.Repositories
{
    public class FoodRepository : RepositoryBase<Food>, IFoodRepository
    {
        public FoodRepository(SmartZoneContext szc) : base(szc) { }

        public void DeleteAllByStore(ICollection<Food> Foods)
        {
            _dbSet.RemoveRange(Foods);
        }
    }
}
