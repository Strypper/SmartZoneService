using SmartZone.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace SmartZone.Contracts
{
    public interface IOrderRepository : IRepositoryBase<Order>
    {
        public IQueryable<Order> FindAllByStoreId(int storeId, Expression<Func<Order, bool>>? predicate = null);
        public IQueryable<Order> FindByStoreIdToday(int storeId, Expression<Func<Order, bool>>? predicate = null);
        public IQueryable<Order> FindAllByCustomerId(string customerId, Expression<Func<Order, bool>>? predicate = null);
        //public Task<Order> FindLatestByCustomerId(int customerId, CancellationToken cancellationToken = default);
        IQueryable<Order> FindStatusByCustomerId(string customerId, OrderStatus status, Expression<Func<Order, bool>> predicate = null);
        IQueryable<Order> FindStatusByStoreId(int storeId, OrderStatus status, Expression<Func<Order, bool>> predicate = null);
    }
}
