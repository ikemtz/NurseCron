using IkeMtz.NRSRx.Competencies.Abstraction.Models;
using IkeMtz.NRSRx.Core.OData;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNetCore.Mvc;

namespace IkeMtz.NRSRx.Competencies.OData.Configuration
{
  public class CompetencyConfiguration : IModelConfiguration
  {
    public void Apply(ODataModelBuilder builder, ApiVersion apiVersion)
    {
      ODataConfigurationBuilder<Competency>.EntitySetBuilder(builder, nameof(Competencies));
    }
  }
}
