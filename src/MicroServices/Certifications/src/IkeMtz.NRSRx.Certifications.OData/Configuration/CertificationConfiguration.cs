using IkeMtz.NRSRx.Certifications.Abstraction.Models;
using IkeMtz.NRSRx.Core.OData;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNetCore.Mvc;

namespace IkeMtz.NRSRx.Certifications.OData.Configuration
{
  public class CertificationConfiguration : IModelConfiguration
  {
    public void Apply(ODataModelBuilder builder, ApiVersion apiVersion)
    {
      ODataConfigurationBuilder<Certification>.EntitySetBuilder(builder);
    }
  }
}
