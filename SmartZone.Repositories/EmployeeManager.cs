using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
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
    public class EmployeeManager : UserManager<Employee>
    {
        public EmployeeManager(
            IUserStore<Employee> store,
            IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<Employee> passwordHasher,
            IEnumerable<IUserValidator<Employee>> userValidators,
            IEnumerable<IPasswordValidator<Employee>> passwordValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            IServiceProvider services,
            ILogger<UserManager<Employee>> logger
        ) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger) { }

        public IQueryable<Employee> FindAll(int StoreId, Expression<Func<Employee, bool>>? predicate = null)
            => Users.Where(emp => emp.StoreId == StoreId).WhereIf(predicate != null, predicate!);

        public async Task<IdentityResult> Delete(Employee entity) 
        {
            entity.IsDeleted = true;
            return await UpdateAsync(entity);
        }

        public async Task<Employee?> FindByGuidAsync(string guid, CancellationToken cancellationToken = default)
           => await Users.Where(emp => emp.Guid == guid).FirstOrDefaultAsync(cancellationToken);
    }
}
