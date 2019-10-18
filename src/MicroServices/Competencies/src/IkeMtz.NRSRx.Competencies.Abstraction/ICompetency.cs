using IkeMtz.NRSRx.Core.Models;

namespace IkeMtz.NRSRx.Competencies.Abstraction
{
    public interface ICompetency : IAuditable, IIdentifiable, IDisableable
    {
        string Name { get; }
    }
}
