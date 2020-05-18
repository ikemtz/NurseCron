using IkeMtz.NRSRx.Core.Models;

namespace IkeMtz.NRSRx.Certifications.Abstraction
{
  public interface ICertification : IAuditable, IIdentifiable
  {
    string Name { get; }
  }
}
