using IkeMtz.NRSRx.Core.Models;

namespace IkeMtz.NRSRx.HealthItems.Models
{
    public interface IHealthItem : IAuditable, IIdentifiable
    {
        string Name { get; }
    }
}
