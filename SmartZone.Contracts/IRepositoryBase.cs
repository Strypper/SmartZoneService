using Microsoft.EntityFrameworkCore.Storage;
using SmartZone.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace SmartZone.Contracts
{
    public interface IRepositoryBase<T> where T : BaseEntity
    {
        IQueryable<T> FindAll(Expression<Func<T, bool>> predicate = null);
        Task<T?> FindByIdAsync(int id, CancellationToken cancellationToken = default);
        void Add(T entities);
        void AddRange(IEnumerable<T> entities);
        void Delete(T entity);
        void Update(T entity);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
        Task<IDbContextTransaction> BeginTransactionAsync();
    }
}
