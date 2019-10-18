using IkeMtz.NRSRx.HealthItems.Models;
using Microsoft.EntityFrameworkCore;

namespace IkeMtz.NRSRx.HealthItems.OData.Data
{
    public interface IHealthItemsContext 
    {
        DbSet<HealthItem> HealthItems { get; set; }
    }
}