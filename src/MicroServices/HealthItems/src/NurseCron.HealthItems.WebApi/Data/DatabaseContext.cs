using IkeMtz.NRSRx.Core.EntityFramework;
using NurseCron.HealthItems.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace NurseCron.HealthItems.WebApi.Data
{
  public partial class DatabaseContext : AuditableDbContext
  {
    public DatabaseContext(DbContextOptions<DatabaseContext> options, IHttpContextAccessor httpContextAccessor)
        : base(options, httpContextAccessor)
    {
    }

    public virtual DbSet<HealthItem> HealthItems { get; set; }
  }
}
