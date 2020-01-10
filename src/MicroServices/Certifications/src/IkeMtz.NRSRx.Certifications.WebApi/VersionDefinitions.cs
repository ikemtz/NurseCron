using IkeMtz.NRSRx.Core.Web;
using System.Collections.Generic;

namespace IkeMtz.NRSRx.Certifications
{
  public class VersionDefinitions : IApiVersionDefinitions
  {
    public const string V1_0 = "1.0";

    public IEnumerable<string> Versions => new[] { V1_0 };
  }
}
