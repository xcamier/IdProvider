using Microsoft.EntityFrameworkCore;
using IdProvider.Entities;


namespace IdProvider.DataAccess;

public class DataContext : DbContext
{
    protected readonly IConfiguration Configuration;
    
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }

    public DbSet<Ids> Ids { get; set; }
}
