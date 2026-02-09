using System.Linq.Expressions;
using TestWebApplication.Models;

namespace TestWebApplication.DataAccess.Specification;

public class GeneralSpecification<T> : BaseSpecification<T>
{
    
    public GeneralSpecification(Expression<Func<T, bool>> criteria) : base(criteria)
    {
        
    }
    public void AddInclude(Expression<Func<T, object>> includeExpression)
        => base.AddInclude(includeExpression);

    public void AddInclude(string includeString)
        => base.AddInclude(includeString);

    public void ApplyPaging(int skip, int take)
    {
        base.ApplyPaging(skip, take);
    }

    public void ApplyOrderBy(Expression<Func<T, object>> orderByExpression)
        => base.ApplyOrderBy(orderByExpression);

    public void ApplyOrderByDescending(Expression<Func<T, object>> orderByDescExpression)
        => base.ApplyOrderByDescending(orderByDescExpression);
}