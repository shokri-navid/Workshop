using Microsoft.EntityFrameworkCore;
using TestWebApplication.DataAccess.Specification;
using TestWebApplication.Models;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;


namespace TestWebApplication.DataAccess;

public class PersonRepository : IPersonRepository
{
    private readonly ApplicationDbContext _context;

    public PersonRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<PaginatedResult<Person>> GetAllAsync(ISpecification<Person> spec)
    {
        PaginatedResult<Person> result = new PaginatedResult<Person>();
        result.PageNumber = spec.Skip;
        result.PageSize = spec.Take;
        result.Items = await _context.Persons.ApplySpecification(spec).ToListAsync();
        result.TotalCount = await _context.Persons.CountAsync(spec.Criteria);
        return result;
    }

    public async Task<PaginatedResult<Person>> GetAllAsync(Expression<Func<Person,bool>> query, string orderBy, int skip, int take)
    {
        PaginatedResult<Person> result = new PaginatedResult<Person>();
        result.TotalCount=  _context.Persons.Count(query);
        result.Items = await _context.Persons.Where(query).OrderBy(orderBy).Take(take).Skip(skip).ToListAsync();
        result.PageNumber = skip;
        result.PageSize = take;
        return result;
    }
}