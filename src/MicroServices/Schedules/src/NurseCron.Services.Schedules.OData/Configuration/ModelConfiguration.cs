using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNetCore.Mvc;
using NurseCron.Services.Schedules.Abstraction;

namespace NurseCron.Services.Schedules.OData.Configuration
{
  public class ModelConfiguration : IModelConfiguration
  {
    public void Apply(ODataModelBuilder builder, ApiVersion apiVersion)
    {
 _ = builder.EntitySet<Schedule>($"{nameof(Schedule)}s");
    }
  }
}
