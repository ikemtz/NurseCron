using System.Collections.Generic;
using IkeMtz.NRSRx.Core.OData;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OData.Edm;
using NurseCron.Services.Schedules.Abstraction;

namespace NurseCron.Services.Schedules.OData.Configuration
{
  public class ODataModelProvider : BaseODataModelProvider
  {
    public static IEdmModel GetV1EdmModel() =>
      ODataEntityModelFactory(builder =>
      {
        _ = builder.EntitySet<Schedule>($"{nameof(Schedule)}s");
      });

    public override IDictionary<ApiVersionDescription, IEdmModel> GetModels() =>
        new Dictionary<ApiVersionDescription, IEdmModel>
        {
          [ApiVersionFactory(1, 0)] = GetV1EdmModel(),
        };
  }
}
