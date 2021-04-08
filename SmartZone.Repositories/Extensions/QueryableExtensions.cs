using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SmartZone.Repositories.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, bool>> predicateIfTrue)
            => condition ? query.Where(predicateIfTrue) : query;
    }
}
