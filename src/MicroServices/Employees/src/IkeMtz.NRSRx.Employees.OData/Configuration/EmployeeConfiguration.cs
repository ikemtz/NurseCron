using IkeMtz.NRSRx.Core.OData;
using IkeMtz.NRSRx.Employees.Models;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNetCore.Mvc;

namespace IkeMtz.NRSRx.Employees.OData.Configuration
{
  public class EmployeeConfiguration : IModelConfiguration
  {
    public void Apply(ODataModelBuilder builder, ApiVersion apiVersion)
    {
      ODataConfigurationBuilder<Employee>.EntitySetBuilder(builder);
    }
  }
}
