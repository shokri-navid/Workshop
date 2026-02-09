using System.Linq.Expressions;
using TestWebApplication.DataAccess.Specification;
using TestWebApplication.Models;

namespace TestWebApplication.DataAccess;

public interface IPersonRepository
{
    Task<PaginatedResult<Person>> GetAllAsync(ISpecification<Person>  spec);
    Task<PaginatedResult<Person>> GetAllAsync(Expression<Func<Person,bool>> query, string orderBy, int skip, int take);
}