using NurseCron.Competencies.Abstraction.Models;
using Microsoft.EntityFrameworkCore;

namespace NurseCron.Competencies.OData.Data
{
  public partial class DatabaseContext : DbContext
  {
    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Competency> Competencies { get; set; }
  }
}
