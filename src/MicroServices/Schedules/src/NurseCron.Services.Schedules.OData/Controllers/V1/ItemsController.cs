using System;
using System.Collections.Generic;
using IkeMtz.NRSRx.Core.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NurseCron.Services.Schedules.Abstraction;
using NurseCron.Services.Schedules.OData.Data; 
using static Microsoft.AspNet.OData.Query.AllowedQueryOptions;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace NurseCron.Services.Schedules.OData.Controllers.V1
{
  [ApiVersion(VersionDefinitions.v1_0)]
  [Authorize]
  [ODataRoutePrefix("Schedules")]
  [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 6000)]
  public class ItemsController : ODataController
  {
    private readonly IDatabaseContext _databaseContext;

    public ItemsController(IDatabaseContext databaseContext)
    {
      _databaseContext = databaseContext;
    }

    [ODataRoute]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ODataEnvelope<Schedule, Guid>), Status200OK)]
    [EnableQuery(MaxTop = 100, AllowedQueryOptions = All)]
    public IEnumerable<Schedule> Get()
    {
      return _databaseContext.Schedules
        .AsNoTracking();
    }
  }
}
