using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SmartZone.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SmartZone.Repositories
{
    public class CustomerManager : UserManager<Customer>
    {
        public CustomerManager(
            IUserStore<Customer> store,
            IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<Customer> passwordHasher,
            IEnumerable<IUserValidator<Customer>> userValidators,
            IEnumerable<IPasswordValidator<Customer>> passwordValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            IServiceProvider services,
            ILogger<UserManager<Customer>> logger
        ) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger) { }

        public async Task<IdentityResult> Delete(Customer entity)
        {
            entity.IsDeleted = true;
            return await UpdateAsync(entity);
        }

        public async Task<Customer?> FindByGuidAsync(string guid, CancellationToken cancellationToken = default)
           => await Users.Where(emp => emp.Guid == guid).FirstOrDefaultAsync(cancellationToken);
    }
}
