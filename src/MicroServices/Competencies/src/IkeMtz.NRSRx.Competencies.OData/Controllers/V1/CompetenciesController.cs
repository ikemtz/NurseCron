using IkeMtz.NRSRx.Competencies.Abstraction.Models;
using IkeMtz.NRSRx.Competencies.OData.Data;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using static Microsoft.AspNet.OData.Query.AllowedQueryOptions;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace IkeMtz.NRSRx.Competencies.OData.ODataControllers.V1
{
    [ApiVersion(VersionDefinitions.v1_0)]
    [Authorize]
    [ODataRoutePrefix(nameof(Competencies))]
    public class CompetenciesController : ODataController
    {
        private readonly ICompetenciesContext _ctx;

        public CompetenciesController(ICompetenciesContext ctx)
        {
            _ctx = ctx;
        }

        [ODataRoute]
        [ProducesResponseType(typeof(ODataValue<IEnumerable<Competency>>), Status200OK)]
        [EnableQuery(MaxTop = 100, AllowedQueryOptions = All)]
        public IActionResult Get()
        {
            return Ok(_ctx.Competencies.Where(t => t.IsEnabled));
        }
    }
}
