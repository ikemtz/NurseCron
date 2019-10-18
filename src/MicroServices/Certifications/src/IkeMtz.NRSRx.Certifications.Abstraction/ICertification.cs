using IkeMtz.NRSRx.Core.Models;

namespace IkeMtz.NRSRx.Certifications.Abstraction
{
    public interface ICertification : IAuditable, IIdentifiable, IDisableable
    {
        string Name { get; }
    }
}
