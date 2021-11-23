using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using LocalPhoneDomain;

namespace LocalPhoneDomain.Services
{
    public abstract class BaseService<TModel> where TModel : class
    {
        protected abstract ImmutableDictionary<string, Expression<Func<TModel, object>>> PropertyNamesAndTheirOrderByExpressions { get; }

        protected Expression<Func<TModel, object>> GetOrderByExpressionForPropertyName(string orderBy)
        {
            if (string.IsNullOrEmpty(orderBy))
            {
                return null;
            }

            orderBy = orderBy.ToLower();

            if (PropertyNamesAndTheirOrderByExpressions.ContainsKey(orderBy))
            {
                return PropertyNamesAndTheirOrderByExpressions[orderBy];
            }

            return null;
        }
    }
}
