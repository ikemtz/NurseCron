using System.Linq;
using IkeMtz.NRSRx.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using NurseCron.Employees.Models;
using NurseCron.Employees.OData.Data;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace NurseCron.Employees.OData.Controllers.V1
{
  [Authorize]
  [ApiVersion(VersionDefinitions.V1_0)]
  public class EmployeesController : ODataController
  {
    private readonly DatabaseContext _ctx;

    public EmployeesController(DatabaseContext ctx)
    {
      _ctx = ctx;
    }

    [Produces("application/json")]
    [ProducesResponseType(typeof(ODataEnvelope<Employee>), Status200OK)]
    [EnableQuery(MaxTop = 100, AllowedQueryOptions = AllowedQueryOptions.All)]
    public IQueryable Get()
    {
      return _ctx.Employees
        .Where(t => t.IsEnabled);
    }
  }
}
