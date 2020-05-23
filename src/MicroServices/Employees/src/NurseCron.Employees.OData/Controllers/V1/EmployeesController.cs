using System.Linq;
using IkeMtz.NRSRx.Core.Models;
using NurseCron.Employees.Models;
using NurseCron.Employees.OData.Data;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Microsoft.AspNet.OData.Query.AllowedQueryOptions;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace NurseCron.Employees.OData.Controllers.V1
{
  //TODO: Need to re-add Authorize attribute
  [ApiVersion(VersionDefinitions.V1_0)]
  [ODataRoutePrefix(nameof(Employees))]
  public class EmployeesController : ODataController
  {
    private readonly IEmployeesContext _ctx;

    public EmployeesController(IEmployeesContext ctx)
    {
      _ctx = ctx;
    }

    [Produces("application/json")]
    [ODataRoute]
    [ProducesResponseType(typeof(ODataEnvelope<Employee>), Status200OK)]
    [EnableQuery(MaxTop = 100, AllowedQueryOptions = All)]
    public IQueryable Get()
    {
      return _ctx.Employees
        .Include(t => t.Certifications)
        .Include(t => t.Competencies)
        .Include(t => t.HealthItems)
        .Where(t => t.IsEnabled);
    }
  }
}
