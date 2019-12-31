using IkeMtz.NRSRx.Core.Models;
using IkeMtz.NRSRx.Employees.Models;
using IkeMtz.NRSRx.Employees.OData.Data;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using static Microsoft.AspNet.OData.Query.AllowedQueryOptions;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace IkeMtz.NRSRx.Employees.OData.Controllers.V1
{
  [ApiVersion(VersionDefinitions.v1_0)]
  [Authorize]
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
    public IActionResult Get()
    {
      return Ok(_ctx.Employees.Where(t => t.IsEnabled));
    }
  }
}
