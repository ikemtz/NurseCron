using IkeMtz.NRSRx.Core.OData;
using IkeMtz.NRSRx.HealthItems.Models;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNetCore.Mvc;

namespace IkeMtz.NRSRx.HealthItems.OData.Configuration
{
  public class HealthItemConfiguration : IModelConfiguration
  {
    public void Apply(ODataModelBuilder builder, ApiVersion apiVersion)
    {
      ODataConfigurationBuilder<HealthItem>.EntitySetBuilder(builder);
    }
  }
}
