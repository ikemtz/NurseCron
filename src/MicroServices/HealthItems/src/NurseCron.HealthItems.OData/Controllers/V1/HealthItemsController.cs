using System.Linq;
using IkeMtz.NRSRx.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using NurseCron.HealthItems.Models;
using NurseCron.HealthItems.OData.Data;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace NurseCron.HealthItems.OData.Controllers.V1
{
  [ApiVersion(VersionDefinitions.V1_0)]
  [Authorize]
  public class HealthItemsController : ODataController
  {
    private readonly DatabaseContext _ctx;

    public HealthItemsController(DatabaseContext ctx)
    {
      _ctx = ctx;
    }

    [Produces("application/json")]

    [ProducesResponseType(typeof(ODataEnvelope<HealthItem>), Status200OK)]
    [EnableQuery(MaxTop = 100, AllowedQueryOptions = AllowedQueryOptions.All)]
    public IActionResult Get()
    {
      return Ok(_ctx.HealthItems.Where(t => t.IsEnabled));
    }
  }
}
