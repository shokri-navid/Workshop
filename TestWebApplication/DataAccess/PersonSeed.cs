using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestWebApplication.Models;

namespace TestWebApplication.DataAccess;

public static class PersonIdHolder
{
    public  static string NavidId { get; } = "C5312D91-00C5-4C4E-99CF-6E9F5FCB778E";
    public static string AliId { get; } = "C5312D91-11C5-4C4E-99CF-6E9F5FCB778E";
    public static string SaeedId = "C5312D91-00C5-4C4E-99CF-6E9F5FCB779E";
}

public class PersonSeed : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
       
        var navid = new Person()
        {
            Id = Guid.Parse(PersonIdHolder.NavidId) ,
            Address = "Shahmirzad",
            Name = "Navid",
            Familly = "Shokri",
            BirthDay = new DateTime(1988, 09, 14),
            
        };

        
        
        var ali = new Person()
        {
            Id =  Guid.Parse(PersonIdHolder.AliId),
            Address = "Shahmirzad",
            Name = "Ali",
            Familly = "Shokri",
            BirthDay = new DateTime(1986, 04, 14),
            
        };
       

        var saeed = new Person()
        {
            Id = Guid.Parse(PersonIdHolder.SaeedId),
            Address = "Semnan",
            Name = "Saeed",
            Familly = "Salavati",
            BirthDay = new DateTime(1988, 07, 14),
        };
       
        builder.HasData(
            new List<Person>()
            {
                navid,
                ali,
                saeed,
            });
       
    }
    public class SkillSeed : IEntityTypeConfiguration<Skill>
    {
        public void Configure(EntityTypeBuilder<Skill> builder)
        {
            var skills = new List<Skill>()
            {
                new Skill { PersonId =  Guid.Parse(PersonIdHolder.NavidId), Title = "SWE", Id = Guid.Parse("C5312D91-10C5-4C4E-99CF-6E9F5FCB778E") },
                new Skill
                {
                    PersonId =  Guid.Parse(PersonIdHolder.NavidId), Title = "Welding", Id = Guid.Parse("C5312D91-18C5-4C4E-99CF-6E9F5FCB778E")
                },
                new Skill
                {
                    PersonId =  Guid.Parse(PersonIdHolder.NavidId), Title = "Coaching", Id = Guid.Parse("C5312D91-20C5-4C4E-99CF-6E9F5FCB778E"),
                }
            };
            skills.AddRange( new List<Skill>()
            {
                new Skill { PersonId = Guid.Parse(PersonIdHolder.AliId), Title = "Construction", Id = Guid.Parse("C5312D91-12C5-4C4E-99CF-6E9F5FCB778E") },
                new Skill { PersonId = Guid.Parse(PersonIdHolder.AliId), Title = "Communication", Id = Guid.Parse("C5312D91-15C5-4C4E-99CF-6E9F5FCB778E"), },
                new Skill { PersonId = Guid.Parse(PersonIdHolder.AliId), Title = "Coaching", Id = Guid.Parse("C5312D91-00C5-3C4E-99CF-6E9F5FCB778E"), }
            });
            skills .AddRange( new List<Skill>
            {
                new Skill { PersonId = Guid.Parse(PersonIdHolder.SaeedId), Title = "Mechanics", Id =Guid.Parse("C5312D91-45C5-4C4E-99CF-6E9F5FCB778E"), },
                new Skill { PersonId = Guid.Parse(PersonIdHolder.SaeedId), Title = "Accounting", Id = Guid.Parse("C5312D91-00C5-4C4E-10CF-6E9F5FCB778E"), }
            });
             builder.HasData(skills);
        }
    }
}