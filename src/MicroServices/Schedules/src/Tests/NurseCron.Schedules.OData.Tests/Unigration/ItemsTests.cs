using System;
using System.Linq;
using System.Threading.Tasks;
using IkeMtz.NRSRx.Core.Models;
using IkeMtz.NRSRx.Core.Unigration;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using NurseCron.Services.Schedules.Abstraction;
using NurseCron.Services.Schedules.OData.Data;
using static IkeMtz.NRSRx.Core.Unigration.TestDataFactory;

namespace NurseCron.Services.Schedules.OData.Tests.Unigration
{
  [TestClass]
  public partial class SchedulesTests : BaseUnigrationTests
  {
    [TestMethod]
    [TestCategory("Unigration")]
    public async Task GetSchedulesTest()
    {
      var objA = CreateIdentifiable(CreateAuditable<Schedule>());
      objA.EmployeeName = $"Test- {StringGenerator(30, true)}";
      objA.UnitName = StringGenerator(5);
      using var srv = new TestServer(TestHostBuilder<Startup, UnigrationODataTestStartup>()
          .ConfigureTestServices(x =>
          {
            ExecuteOnContext<DatabaseContext>(x, db =>
            {
              _ = db.Schedules.Add(objA);
            });
          })
       );
      var client = srv.CreateClient();
      GenerateAuthHeader(client, GenerateTestToken());

      var resp = await client.GetStringAsync($"odata/v1/{nameof(Schedule)}s?$count=true");

      //Validate OData Result
      TestContext.WriteLine($"Server Reponse: {resp}");
      var envelope = JsonConvert.DeserializeObject<ODataEnvelope<Schedule>>(resp);
      Assert.AreEqual(objA.EmployeeName, envelope.Value.First().EmployeeName);
    }
  }
}
