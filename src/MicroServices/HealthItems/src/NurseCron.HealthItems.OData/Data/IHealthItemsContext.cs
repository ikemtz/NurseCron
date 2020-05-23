using NurseCron.HealthItems.Models;
using Microsoft.EntityFrameworkCore;

namespace NurseCron.HealthItems.OData.Data
{
    public interface IHealthItemsContext 
    {
        DbSet<HealthItem> HealthItems { get; set; }
    }
}
