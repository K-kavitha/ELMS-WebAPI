using Microsoft.EntityFrameworkCore;

namespace ELMSWebAPI.Models;

public class ELMSWebAPIContext : DbContext
{
    public ELMSWebAPIContext(DbContextOptions<ELMSWebAPIContext> options)
        : base(options)
    {
    }

    public DbSet<Employee> Employee { get; set; } = null!;
    
}