using NurseCron.HealthItems.Models;
using Microsoft.EntityFrameworkCore;

namespace NurseCron.HealthItems.OData.Data
{
  public partial class DatabaseContext : DbContext
  {
    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<HealthItem> HealthItems { get; set; }
  }
}
