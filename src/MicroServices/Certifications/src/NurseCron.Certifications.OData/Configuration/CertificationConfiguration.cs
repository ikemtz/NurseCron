using NurseCron.Certifications.Abstraction.Models;
using IkeMtz.NRSRx.Core.OData;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNetCore.Mvc;

namespace NurseCron.Certifications.OData.Configuration
{
  public class CertificationConfiguration : IModelConfiguration
  {
    public void Apply(ODataModelBuilder builder, ApiVersion apiVersion, string? routePrefix)
    {
      ODataConfigurationBuilder<Certification>.EntitySetBuilder(builder);
    }
  }
}
