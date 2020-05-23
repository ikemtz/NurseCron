using IkeMtz.NRSRx.Core.Models;

namespace NurseCron.Certifications.Abstraction
{
  public interface ICertification : IAuditable, IIdentifiable
  {
    string Name { get; }
  }
}
