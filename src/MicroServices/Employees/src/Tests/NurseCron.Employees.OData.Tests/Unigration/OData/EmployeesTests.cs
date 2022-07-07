using System;
using System.Linq;
using System.Threading.Tasks;
using IkeMtz.NRSRx.Core.Models;
using IkeMtz.NRSRx.Core.Unigration;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NurseCron.Employees.Models;
using NurseCron.Employees.OData;
using NurseCron.Employees.OData.Data;
using static IkeMtz.NRSRx.Core.Unigration.TestDataFactory;

namespace NurseCron.Employees.Tests.Unigration.OData
{
  [TestClass]
  public partial class EmployeesTests : BaseUnigrationTests
  {
    [TestMethod]
    [TestCategory("Unigration")]
    public async Task GetEnabledEmployeesTest()
    {
      var objA = CreateIdentifiable(CreateAuditable<Employee>());

      objA.FirstName = "Test";
      objA.IsEnabled = true;
      objA.BirthDate = DateTime.Today.AddYears(-20);
      objA.Email = "me@me.com";
      objA.LastName = StringGenerator(20);

      using var srv = new TestServer(TestHostBuilder<Startup, UnigrationODataTestStartup>()
          .ConfigureTestServices(x =>
            {
              ExecuteOnContext<DatabaseContext>(x, db =>
                    {
                      _ = db.Employees.Add(objA);
                    });
            })
       );
      var client = srv.CreateClient();
      GenerateAuthHeader(client, GenerateTestToken());

      var resp = await client.GetAsync($"odata/v1/{nameof(Employee)}s?$count=true");
       
      var envelope = await DeserializeResponseAsync<Employee[]>(resp);
      Assert.AreEqual(objA.CreatedOnUtc, envelope.First().CreatedOnUtc.ToUniversalTime());
    }

    [TestMethod]
    [TestCategory("Unigration")]
    public async Task HideDisabledEmployeesTest()
    {
      var objA = new Employee()
      {
        Id = Guid.NewGuid(),
        FirstName = "Test",
        IsEnabled = false,
        Email = "me@me.com",
        LastName = StringGenerator(20),
      };
      using var srv = new TestServer(TestHostBuilder<Startup, UnigrationODataTestStartup>()
          .ConfigureTestServices(x =>
          {
            ExecuteOnContext<DatabaseContext>(x, db =>
                  {
                    _ = db.Employees.Add(objA);
                  });
          })
       );
      var client = srv.CreateClient();
      GenerateAuthHeader(client, GenerateTestToken());

      var resp = await client.GetAsync($"odata/v1/{nameof(Employee)}s?$count=true");

      var objB = await DeserializeResponseAsync<Employee[]>(resp);

      Assert.AreEqual(0, objB.Length);
    }

  }
}
