using IkeMtz.NRSRx.Core.Models;

namespace NurseCron.Competencies.Abstraction
{
  public interface ICompetency : IAuditable, IIdentifiable
  {
    string Name { get; }
  }
}
