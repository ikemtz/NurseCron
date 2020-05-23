using System;
using System.Collections.Generic;
using IkeMtz.NRSRx.Core.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NurseCron.Units.OData.Data; 
using static Microsoft.AspNet.OData.Query.AllowedQueryOptions;
using static Microsoft.AspNetCore.Http.StatusCodes;
using NurseCron.Units.Abstraction;

namespace NurseCron.Units.OData.Controllers.V1
{
  [ApiVersion(VersionDefinitions.v1_0)]
  [Authorize]
  [ODataRoutePrefix("Units")]
  [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 6000)]
  public class UnitsController : ODataController
  {
    private readonly IDatabaseContext _databaseContext;

    public UnitsController(IDatabaseContext databaseContext)
    {
      _databaseContext = databaseContext;
    }

    [ODataRoute]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ODataEnvelope<Unit, Guid>), Status200OK)]
    [EnableQuery(MaxTop = 100)]
    public IEnumerable<Unit> Get()
    {
      return _databaseContext.Units
        .AsNoTracking();
    }
  }
}
