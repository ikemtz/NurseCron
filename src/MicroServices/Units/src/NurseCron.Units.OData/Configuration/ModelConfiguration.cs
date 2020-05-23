using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNetCore.Mvc;
using NurseCron.Units.Abstraction;

namespace NurseCron.Units.OData.Configuration
{
  public class ModelConfiguration : IModelConfiguration
  {
    public void Apply(ODataModelBuilder builder, ApiVersion apiVersion)
    {
      byte[] array = new byte[5];
      _ = builder.EntitySet<Building>($"{nameof(Building)}s");
      _ = builder.EntitySet<Unit>($"{nameof(Unit)}s");
    }
  }
}
