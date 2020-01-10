using System.Linq;
using IkeMtz.NRSRx.Competencies.Abstraction.Models;
using IkeMtz.NRSRx.Competencies.OData.Data;
using IkeMtz.NRSRx.Core.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.AspNet.OData.Query.AllowedQueryOptions;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace IkeMtz.NRSRx.Competencies.OData.ODataControllers.V1
{
  [ApiVersion(VersionDefinitions.V1_0)]
  [Authorize]
  [ODataRoutePrefix(nameof(Competencies))]
  public class CompetenciesController : ODataController
  {
    private readonly ICompetenciesContext _ctx;

    public CompetenciesController(ICompetenciesContext ctx)
    {
      _ctx = ctx;
    }

    [Produces("application/json")]
    [ODataRoute]
    [ProducesResponseType(typeof(ODataEnvelope<Competency>), Status200OK)]
    [EnableQuery(MaxTop = 100, AllowedQueryOptions = All)]
    public IActionResult Get()
    {
      return Ok(_ctx.Competencies.Where(t => t.IsEnabled));
    }
  }
}
