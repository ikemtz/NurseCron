using IkeMtz.NRSRx.Core.OData;
using NurseCron.Employees.Models;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNetCore.Mvc;

namespace NurseCron.Employees.OData.Configuration
{
  public class EmployeeConfiguration : IModelConfiguration
  {
    public void Apply(ODataModelBuilder builder, ApiVersion apiVersion, string? routePrefix)
    {
      ODataConfigurationBuilder<Employee>.EntitySetBuilder(builder);
      ODataConfigurationBuilder<EmployeeCertification>.EntitySetBuilder(builder);
      ODataConfigurationBuilder<EmployeeCompetency>.EntitySetBuilder(builder);
      ODataConfigurationBuilder<EmployeeHealthItem>.EntitySetBuilder(builder);
    }
  }
}
