using System.Collections.Generic;
using IkeMtz.NRSRx.Core.OData;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OData.Edm;
using NurseCron.Employees.Models;

namespace NurseCron.Employees.OData.Configuration
{
  public class ODataModelProvider : BaseODataModelProvider
  {
    public static IEdmModel GetV1EdmModel() =>
      ODataEntityModelFactory(builder =>
      {
        _ = builder.EntitySet<Employee>($"{nameof(Employee)}s");
        _ = builder.EntitySet<EmployeeCertification>($"{nameof(EmployeeCertification)}s");
        _ = builder.EntitySet<EmployeeCompetency>($"{nameof(EmployeeCompetency)}s");
        _ = builder.EntitySet<EmployeeHealthItem>($"{nameof(EmployeeHealthItem)}s");
      });

    public override IDictionary<ApiVersionDescription, IEdmModel> GetModels() =>
        new Dictionary<ApiVersionDescription, IEdmModel>
        {
          [ApiVersionFactory(1, 0)] = GetV1EdmModel(),
        };
  }
}
