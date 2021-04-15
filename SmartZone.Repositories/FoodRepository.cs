using SmartZone.Entities;
using SmartZone.Contracts;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SmartZone.Repositories
{
    public class FoodRepository : RepositoryBase<Food>, IFoodRepository
    {
        public FoodRepository(SmartZoneContext szc) : base(szc) { }
    }
}
