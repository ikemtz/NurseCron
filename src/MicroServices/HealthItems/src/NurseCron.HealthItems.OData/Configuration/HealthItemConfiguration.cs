using IkeMtz.NRSRx.Core.OData;
using NurseCron.HealthItems.Models;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNetCore.Mvc;

namespace NurseCron.HealthItems.OData.Configuration
{
  public class HealthItemConfiguration : IModelConfiguration
  {
    public void Apply(ODataModelBuilder builder, ApiVersion apiVersion, string? routePrefix)
    {
      ODataConfigurationBuilder<HealthItem>.EntitySetBuilder(builder);
    }
  }
}
