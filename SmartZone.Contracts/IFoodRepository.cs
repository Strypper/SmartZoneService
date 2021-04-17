using SmartZone.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SmartZone.Contracts
{
    public interface IFoodRepository : IRepositoryBase<Food>
    {
        public void DeleteAllByStore(ICollection<Food> Foods);
        Task<ICollection<Food>> FindFoodByStoreId(string storeId, CancellationToken cancellationToken);
    }
}
