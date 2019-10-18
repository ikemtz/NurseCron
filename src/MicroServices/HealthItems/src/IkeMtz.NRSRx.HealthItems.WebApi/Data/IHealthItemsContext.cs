using IkeMtz.NRSRx.Core.EntityFramework;
using IkeMtz.NRSRx.HealthItems.Models;
using Microsoft.EntityFrameworkCore;

namespace IkeMtz.NRSRx.HealthItems.WebApi.Data
{
    public interface IHealthItemsContext : IAuditableDbContext
    {
        DbSet<HealthItem> HealthItems { get; set; }
    }
}