﻿using IkeMtz.NRSRx.Core.Web;
using System.Collections.Generic;

namespace IkeMtz.NRSRx.HealthItems.OData
{
    public class VersionDefinitions : IApiVersionDefinitions
    {
        public const string v1_0 = "1.0";

        public IEnumerable<string> Versions => new[] { v1_0 };
    }
}
