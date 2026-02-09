using Microsoft.EntityFrameworkCore;
using TestWebApplication.Models;

namespace TestWebApplication.DataAccess;

public class ApplicationDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }
    public DbSet<Skill> Skills { get; set; }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PersonSeed());
        modelBuilder.ApplyConfiguration(new PersonSeed.SkillSeed());
    }
}