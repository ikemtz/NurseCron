using System;
using System.Collections.Generic;
using IkeMtz.NRSRx.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using NurseCron.Services.Schedules.Abstraction;
using NurseCron.Services.Schedules.OData.Data;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace NurseCron.Services.Schedules.OData.Controllers.V1
{
  [ApiVersion(VersionDefinitions.v1_0)]
  [Authorize]
  [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 6000)]
  public class SchedulesController : ODataController
  {
    private readonly DatabaseContext _databaseContext;

    public SchedulesController(DatabaseContext databaseContext)
    {
      _databaseContext = databaseContext;
    }


    [Produces("application/json")]
    [ProducesResponseType(typeof(ODataEnvelope<Schedule, Guid>), Status200OK)]
    [EnableQuery(MaxTop = 100, AllowedQueryOptions = AllowedQueryOptions.All)]
    public IEnumerable<Schedule> Get()
    {
      return _databaseContext.Schedules
        .AsNoTracking();
    }
  }
}
