﻿using IkeMtz.NRSRx.Certifications.Abstraction.Models;
using IkeMtz.NRSRx.Certifications.OData.Data;
using IkeMtz.NRSRx.Core.Authorization;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using static Microsoft.AspNet.OData.Query.AllowedQueryOptions;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace IkeMtz.NRSRx.Certifications.OData.Controllers.V1
{
    [ApiVersion(VersionDefinitions.v1_0)]
    [Authorize]
    [ODataRoutePrefix(nameof(Certifications))]
    public class CertificationsController : ODataController
    {
        private readonly ICertificationsContext _ctx;

        public CertificationsController(ICertificationsContext ctx)
        {
            _ctx = ctx;
        }
        [PermissionsFilter(new[] { "cert:read" })]
        [ODataRoute]
        [ProducesResponseType(typeof(ODataValue<IEnumerable<Certification>>), Status200OK)]
        [EnableQuery(MaxTop = 100, AllowedQueryOptions = All)]
        public IActionResult Get()
        {
            return Ok(_ctx.Certifications.Where(t => t.IsEnabled));
        }
    }
}
