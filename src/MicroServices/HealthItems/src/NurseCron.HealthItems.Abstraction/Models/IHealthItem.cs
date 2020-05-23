using IkeMtz.NRSRx.Core.Models;

namespace NurseCron.HealthItems.Models
{
    public interface IHealthItem : IAuditable, IIdentifiable
    {
        string Name { get; }
    }
}
