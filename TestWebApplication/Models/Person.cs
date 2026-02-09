namespace TestWebApplication.Models;

public class Person
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public string Familly { get; set; }
    public DateTime BirthDay { get; set; }
    public string Address { get; set; }
    public List<Skill> Skills { get; set; }
}