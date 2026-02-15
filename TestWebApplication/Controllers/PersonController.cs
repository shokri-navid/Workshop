using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using TestWebApplication.Abstraction;
using TestWebApplication.Abstraction.Application;
using TestWebApplication.Abstraction.Exceptions;
using TestWebApplication.Abstraction.RequestDto;
using TestWebApplication.DataAccess;
using TestWebApplication.DataAccess.Specification;
using TestWebApplication.Models;

namespace TestWebApplication.Controllers;
[ApiController]
[Route("[controller]")]
public class PersonController : ControllerBase
{
    private readonly IPersonRepository _personRepository;
    private readonly ISimpleService _simpleService;

    public PersonController(IPersonRepository personRepository, ISimpleService simpleService)
    {
        _personRepository = personRepository;
        _simpleService = simpleService;
    }
    [HttpGet]
    public async Task<ActionResult<Person>> Test([FromQuery] string? name, [FromQuery]string? family)
    {
        Expression<Func<Person, bool>> criteria = person => 1 == 1;  
        if (!string.IsNullOrEmpty(name))
        {
            criteria = criteria.And<Person>( p => p.Name.Contains(name));
        }

        if (!string.IsNullOrEmpty(family))
        {
            criteria = criteria.And<Person>(person => person.Familly.Contains(family) );
        }

        var sps = new GeneralSpecification<Person>(criteria);
        
        sps.AddInclude(x=>x.Skills);
        sps.ApplyOrderBy(x=>x.Name);
        //sps.ApplyPaging(1, 1);
        var response = await _personRepository.GetAllAsync(sps);
        return Ok(response);
    }
    
    [HttpGet("simple")]
    public async Task<ActionResult<Person>> TestSimple([FromQuery] PersonFilterPaginationRequestDto? requestDto)
    {
        var PersonQueryCommand = new PersonEnquieryCommand
        {
            Filter = new Filter()
            {
                Args  =  requestDto?.Args?.ToArray() ?? new object[]{},
                FilterBody = requestDto?.FilterBody,
            },
            Paging = new Paging()
            {
                PageNumber = (int) requestDto.PageNumber,
                PageSize = (int) requestDto.PageSize
            },
            Sort = new Sorting()
            {
                IsAscending = requestDto.IsAscending,
                SortBy =  requestDto.SortBy,
            }
        };
        var exp = PersonQueryCommand.GenerateFilterExpression<Person>();
        var response = await _personRepository.GetAllAsync(exp, PersonQueryCommand.Sort.ToString(),PersonQueryCommand.Paging.Skip, PersonQueryCommand.Paging.PageSize);
        return Ok(response);
    }

    [HttpGet("exception")]
    public async Task<IActionResult> TestException([FromQuery] string? type) => type switch
    {
        "BCE" => throw new CustomBusinessException(new List<string>() { "first detail", "second detail" }),
        "PDE" => throw new PermissionDeniedException("WTF!!! How dare you!!!"),
        "SAE" => throw new ServiceAuthenticationException("Who are you in the darkness!!!"),
        "NFE" => throw new ItemNotFoundException("You are searching in wron place", "somethings"),
        _ => throw new InvalidCastException()
    };

    [HttpGet("di")]
    public async Task<IActionResult> DiInvoke()
    {
        return Ok(_simpleService.GetName());
    }

}