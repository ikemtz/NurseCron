using IkeMtz.NRSRx.Core.EntityFramework;
using NurseCron.HealthItems.Models;
using Microsoft.EntityFrameworkCore;

namespace NurseCron.HealthItems.WebApi.Data
{
    public interface IHealthItemsContext : IAuditableDbContext
    {
        DbSet<HealthItem> HealthItems { get; set; }
    }
}
