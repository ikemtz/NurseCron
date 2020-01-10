using System.Collections.Generic;
using IkeMtz.NRSRx.Core.Web;

namespace IkeMtz.NRSRx.Competencies
{
  public class VersionDefinitions : IApiVersionDefinitions
  {
    public const string V1_0 = "1.0";

    public IEnumerable<string> Versions => new[] { V1_0 };
  }
}
