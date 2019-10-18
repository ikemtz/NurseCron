﻿using IkeMtz.NRSRx.Core.Unigration.WebApi;
using IkeMtz.NRSRx.Employees.WebApi;
using Microsoft.Extensions.Configuration;

namespace IkeMtz.NRSRx.Employees.Tests.Integration
{
    public class IntegrationTestStartup : CoreWebApiIntegrationTestStartup<Startup>
    {
        public IntegrationTestStartup(IConfiguration configuration)
            : base(new Startup(configuration))
        {
        }
    }
}
