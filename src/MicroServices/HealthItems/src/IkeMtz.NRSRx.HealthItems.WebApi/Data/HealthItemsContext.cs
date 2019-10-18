using IkeMtz.NRSRx.Core.EntityFramework;
using IkeMtz.NRSRx.HealthItems.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace IkeMtz.NRSRx.HealthItems.WebApi.Data
{
    public partial class HealthItemsContext : AuditableDbContext, IHealthItemsContext
    {
        public HealthItemsContext(DbContextOptions<HealthItemsContext> options, IHttpContextAccessor httpContextAccessor)
            : base(options, httpContextAccessor)
        {
        }

        public virtual DbSet<HealthItem> HealthItems { get; set; }
    }
}
