using NurseCron.Units.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace NurseCron.Units.OData.Data
{
  public class DatabaseContext : DbContext, IDatabaseContext
  {
    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Building> Buildings { get; set; }
    public virtual DbSet<Unit> Units { get; set; }
  }
}
