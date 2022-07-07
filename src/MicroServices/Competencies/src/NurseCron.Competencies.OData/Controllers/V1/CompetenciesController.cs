using System.Linq;
using IkeMtz.NRSRx.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using NurseCron.Competencies.Abstraction.Models;
using NurseCron.Competencies.OData.Data;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace NurseCron.Competencies.OData.ODataControllers.V1
{
  [ApiVersion(VersionDefinitions.V1_0)]
  [Authorize]
  public class CompetenciesController : ODataController
  {
    private readonly DatabaseContext _ctx;

    public CompetenciesController(DatabaseContext ctx)
    {
      _ctx = ctx;
    }

    [Produces("application/json")]

    [ProducesResponseType(typeof(ODataEnvelope<Competency>), Status200OK)]
    [EnableQuery(MaxTop = 100, AllowedQueryOptions = AllowedQueryOptions.All)]
    public IActionResult Get()
    {
      return Ok(_ctx.Competencies.Where(t => t.IsEnabled));
    }
  }
}
