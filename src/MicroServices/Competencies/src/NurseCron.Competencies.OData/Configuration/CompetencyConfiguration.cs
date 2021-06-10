using NurseCron.Competencies.Abstraction.Models;
using IkeMtz.NRSRx.Core.OData;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNetCore.Mvc;

namespace NurseCron.Competencies.OData.Configuration
{
  public class CompetencyConfiguration : IModelConfiguration
  {
    public void Apply(ODataModelBuilder builder, ApiVersion apiVersion, string? routePrefix)
    {
      ODataConfigurationBuilder<Competency>.EntitySetBuilder(builder, nameof(Competencies));
    }
  }
}
