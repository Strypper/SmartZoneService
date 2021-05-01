using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using SmartZone.Contracts;
using SmartZone.Entities;
using SmartZone.Repositories.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SmartZone.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : BaseEntity
    {
        protected readonly SmartZoneContext _context;
        protected readonly DbSet<T> _dbSet;
        public RepositoryBase(SmartZoneContext repositoryContext)
        {
            _context = repositoryContext;
            _dbSet = _context.Set<T>();
        }
        public virtual IQueryable<T> FindAll(Expression<Func<T, bool>>? predicate = null)
            => _dbSet.WhereIf(predicate != null, predicate!);
                    
        public virtual async Task<T?> FindByIdAsync(int Id, CancellationToken cancellationToken = default)
            => await _dbSet.FindAsync(Id);

        public void Add(T entities)
            => _dbSet.Add(entities);
        public void AddRange(IEnumerable<T> entities)
            => _dbSet.AddRange(entities);
        public void Update(T entity)
            => _dbSet.Update(entity);
        public virtual void Delete(T entity)
            => _dbSet.Remove(entity);
        public Task SaveChangesAsync(CancellationToken cancellationToken = default)
            => _context.SaveChangesAsync(cancellationToken);
        public Task<IDbContextTransaction> BeginTransactionAsync()
            => _context.Database.BeginTransactionAsync();
    }
}
