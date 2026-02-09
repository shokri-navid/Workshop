using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace TestWebApplication.DataAccess.Specification;

public static class QueryableExtensions
{
    public static IQueryable<T> ApplySpecification<T>(
        this IQueryable<T> query, 
        ISpecification<T> spec) where T : class
    {
        // Apply criteria
        if (spec.Criteria != null)
        {
            query = query.Where(spec.Criteria);
        }

        // Apply includes
        query = spec.Includes.Aggregate(query, 
            (current, include) => current.Include(include));

        // Apply string includes
        query = spec.IncludeStrings.Aggregate(query,
            (current, include) => current.Include(include));

        // Apply ordering
        if (spec.OrderBy != null)
        {
            query = query.OrderBy(spec.OrderBy);
        }
        else if (spec.OrderByDescending != null)
        {
            query = query.OrderByDescending(spec.OrderByDescending);
        }

        // Apply pagination
        if (spec.IsPagingEnabled)
        {
            query = query.Skip(spec.Skip).Take(spec.Take);
        }

        return query;
    }

    public static Expression<Func<T, bool>> And<T>(
        this Expression<Func<T, bool>> left, 
        Expression<Func<T, bool>> right)
    {
        var param = Expression.Parameter(typeof(T), "x");
        var body = Expression.AndAlso(
            Expression.Invoke(left, param),
            Expression.Invoke(right, param)
        );
        return Expression.Lambda<Func<T, bool>>(body, param);
    }

    public static Expression<Func<T, bool>> Or<T>(
        this Expression<Func<T, bool>> left, 
        Expression<Func<T, bool>> right)
    {
        var param = Expression.Parameter(typeof(T), "x");
        var body = Expression.OrElse(
            Expression.Invoke(left, param),
            Expression.Invoke(right, param)
        );
        return Expression.Lambda<Func<T, bool>>(body, param);
    }
}