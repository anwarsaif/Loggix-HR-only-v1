using System;
using System.Linq;
using System.Linq.Expressions;

namespace Logix.MVC.Extentions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> OrderByProperty<T>(this IQueryable<T> source, string propertyName, string direction)
        {
            var nm = propertyName.Remove(0, 1);
            nm = propertyName.ToUpper().First() + nm;
            var entityType = typeof(T);
            var propertyInfo = entityType.GetProperty(nm);

            if (propertyInfo == null)
            {
                throw new ArgumentException($"Property '{propertyName}' does not exist on type '{entityType}'.");
            }

            var parameter = Expression.Parameter(entityType, "x");
            var property = Expression.Property(parameter, propertyInfo);
            var lambda = Expression.Lambda(property, parameter);

            var methodName = direction.ToLower() == "desc" ? "OrderByDescending" : "OrderBy";
            var orderByExpression = Expression.Call(typeof(Queryable), methodName, new[] { entityType, propertyInfo.PropertyType }, source.Expression, Expression.Quote(lambda));

            return source.Provider.CreateQuery<T>(orderByExpression);
        }
    }

}


