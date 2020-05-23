using System;
using System.Linq;
using System.Threading.Tasks;
using IkeMtz.NRSRx.Core.Models;
using IkeMtz.NRSRx.Core.Unigration;
using NurseCron.Employees.Models;
using NurseCron.Employees.OData;
using NurseCron.Employees.OData.Data;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace NurseCron.Employees.Tests.Unigration.OData
{
  [TestClass]
  public partial class EmployeesTests : BaseUnigrationTests
  {
    [TestMethod]
    [TestCategory("Unigration")]
    public async Task GetEnabledEmployeesTest()
    {
      var objA = new Employee()
      {
        Id = Guid.NewGuid(),
        FirstName = "Test",
        IsEnabled = true,
        BirthDate = DateTime.Today.AddYears(-20),
        CreatedOnUtc = DateTime.UtcNow,
      };
      using var srv = new TestServer(TestHostBuilder<Startup, UnigrationODataTestStartup>()
          .ConfigureTestServices(x =>
            {
              ExecuteOnContext<EmployeesContext>(x, db =>
                    {
                      db.Employees.Add(objA);
                    });
            })
       );
      var client = srv.CreateClient();
      GenerateAuthHeader(client, GenerateTestToken());

      var resp = await client.GetStringAsync("odata/v1/Employees?$count=true");

      TestContext.WriteLine($"Server Reponse: {resp}");
      var envelope = JsonConvert.DeserializeObject<ODataEnvelope<Employee>>(resp);
      Assert.AreEqual(objA.CreatedOnUtc, envelope.Value.First().CreatedOnUtc.ToUniversalTime());
    }
    [TestMethod]
    [TestCategory("Unigration")]
    public async Task HideDisabledEmployeesTest()
    {
      var objA = new Employee()
      {
        Id = Guid.NewGuid(),
        FirstName = "Test",
        IsEnabled = false
      };
      using var srv = new TestServer(TestHostBuilder<Startup, UnigrationODataTestStartup>()
          .ConfigureTestServices(x =>
          {
            ExecuteOnContext<EmployeesContext>(x, db =>
                  {
                    db.Employees.Add(objA);
                  });
          })
       );
      var client = srv.CreateClient();
      GenerateAuthHeader(client, GenerateTestToken());

      var resp = await client.GetAsync("odata/v1/Employees?$count=true");

      var objB = await DeserializeResponseAsync<ODataEnvelope<Employee>>(resp);

      Assert.AreEqual(0, objB.Value.Count());
    }

  }
}
