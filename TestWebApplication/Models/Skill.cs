
namespace TestWebApplication.Models;

public class Skill
{
    public Skill()
    {
        
    }

    public Skill(string title)
    {
        Title = title;
    }
    public Guid Id { get; set; } =  Guid.NewGuid();
    public string Title { get; set; }
    public Guid PersonId { get; set; }
}