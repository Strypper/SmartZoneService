using Microsoft.EntityFrameworkCore;
using SmartZone.Contracts;
using SmartZone.Entities;
using SmartZone.Repositories.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace SmartZone.Repositories
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(SmartZoneContext szc) : base(szc) { }

        public IQueryable<Order> FindAllByCustomerId(string customerId, Expression<Func<Order, bool>>? predicate = null)
            => _dbSet.Where(ord => ord.CustomerId.Equals(customerId)).WhereIf(predicate != null, predicate!);

        public IQueryable<Order> FindAllByStoreId(int storeId, Expression<Func<Order, bool>> predicate = null)
            => _dbSet.Where(ord => ord.StoreId == storeId).WhereIf(predicate != null, predicate!);

        public IQueryable<Order> FindByStoreIdToday(int storeId, Expression<Func<Order, bool>> predicate = null)
            => _dbSet.Where(ord => ord.StoreId == storeId 
                            && DateTime.Today <= ord.CreatedAt.Date 
                            && DateTime.Today.AddDays(1) >= ord.CreatedAt.Date)
                        .WhereIf(predicate != null, predicate);

        public IQueryable<Order> FindStatusByCustomerId(string customerId, OrderStatus status, Expression<Func<Order, bool>> predicate = null)
            => _dbSet.Where(ord => ord!.CustomerId.Equals(customerId) && ord!.OrderStatus == status).WhereIf(predicate != null, predicate!);

        public IQueryable<Order> FindStatusByStoreId(int storeId, OrderStatus status, Expression<Func<Order, bool>> predicate = null)
            => _dbSet.Where(ord => ord!.StoreId == storeId && ord!.OrderStatus == status).WhereIf(predicate != null, predicate!);

        //public override void Delete(Order entity)
        //{
        //    entity.IsDeleted = true;
        //    _dbSet.Update(entity);
        //}

        //public async Task<Order> FindLatestByCustomerId(int customerId, CancellationToken cancellationToken = default)
        //    => await FindAll(ord => ord.CustomerId == customerId).LastOrDefaultAsync(cancellationToken);

        //public IQueryable<Order> FindUnpaidByCustomerId(int customerId, Expression<Func<Order, bool>> predicate = null)
        //    => _dbSet.Where(ord => ord!.CustomerId == customerId && ord!.Status == Status.Unpaid).WhereIf(predicate != null, predicate!);
    }
}
