using Microsoft.EntityFrameworkCore;
using NurseCron.Services.Schedules.Abstraction;

namespace NurseCron.Services.Schedules.OData.Data
{
  public class DatabaseContext : DbContext, IDatabaseContext
  {
    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Schedule> Schedules { get; set; }
  }
}
