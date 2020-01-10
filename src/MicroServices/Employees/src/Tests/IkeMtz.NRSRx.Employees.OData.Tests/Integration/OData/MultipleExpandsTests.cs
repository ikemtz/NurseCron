using System.Linq;
using System.Threading.Tasks;
using IkeMtz.NRSRx.Core.Models;
using IkeMtz.NRSRx.Core.Unigration;
using IkeMtz.NRSRx.Employees.Models;
using IkeMtz.NRSRx.Employees.OData.Data;
using IkeMtz.NRSRx.Employees.Tests.Integration;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace IkeMtz.NRSRx.Employees.OData.Tests.Integration.OData
{
  [TestClass]
  public class MultipleExpandsTests : BaseUnigrationTests
  {
    [TestMethod]
    [TestCategory("Integration")]
    public async Task GetEmployees_CompAndCerts_Test()
    {
      using var srv = new TestServer(TestHostBuilder<Startup, IntegrationTestStartup>());
      var client = srv.CreateClient();
      GenerateAuthHeader(client, GenerateTestToken());

      //Equivalent EfQuery -- THIS WORKS
      var dbContext = srv.GetDbContext<EmployeesContext>();
      var dbItems = await dbContext.Employees
        .Include(t => t.Competencies)
        .Include(t=> t.Certifications)
        .Take(5)
        .ToListAsync();
      Assert.IsNotNull(dbItems);

      //Equivalent OData Query -- This doesn't
      var resp = await client.GetStringAsync($"odata/v1/{nameof(Employees)}?$top=5&$count=true&$expand=Competencies,Certifications");
      TestContext.WriteLine($"Server Reponse: {resp}");
      var envelope = JsonConvert.DeserializeObject<ODataEnvelope<Employee>>(resp);
      Assert.AreEqual(envelope.Count, envelope.Value.Count());
      envelope.Value.ToList().ForEach(t =>
      {
        Assert.IsNotNull(t.CreatedBy);
        Assert.IsNotNull(t.CreatedOnUtc);
        Assert.IsTrue(t.IsEnabled);
      });
    }

    [TestMethod]
    [TestCategory("Integration")]
    public async Task GetEmployees_CompAndHltItems_Test()
    {
      using var srv = new TestServer(TestHostBuilder<Startup, IntegrationTestStartup>());
      var client = srv.CreateClient();
      GenerateAuthHeader(client, GenerateTestToken());

      //Equivalent EfQuery -- THIS WORKS
      var dbContext = srv.GetDbContext<EmployeesContext>();
      var dbItems = await dbContext.Employees
        .Include(t => t.Competencies)
        .Include(t => t.HealthItems)
        .Take(5)
        .ToListAsync();
      Assert.IsNotNull(dbItems);

      //Equivalent OData Query -- This doesn't
      var resp = await client.GetStringAsync($"odata/v1/{nameof(Employees)}?$top=5&$count=true&$expand=Competencies,HealthItems");
      TestContext.WriteLine($"Server Reponse: {resp}");
      var envelope = JsonConvert.DeserializeObject<ODataEnvelope<Employee>>(resp);
      Assert.AreEqual(envelope.Count, envelope.Value.Count());
      envelope.Value.ToList().ForEach(t =>
      {
        Assert.IsNotNull(t.CreatedBy);
        Assert.IsNotNull(t.CreatedOnUtc);
        Assert.IsTrue(t.IsEnabled);
      });
    }

    [TestMethod]
    [TestCategory("Integration")]
    public async Task GetEmployees_CertsAndHltItems_Test()
    {
      using var srv = new TestServer(TestHostBuilder<Startup, IntegrationTestStartup>());
      var client = srv.CreateClient();
      GenerateAuthHeader(client, GenerateTestToken());

      //Equivalent EfQuery -- THIS WORKS
      var dbContext = srv.GetDbContext<EmployeesContext>();
      var dbItems = await dbContext.Employees
        .Include(t => t.Certifications)
        .Include(t => t.HealthItems)
        .Take(5)
        .ToListAsync();
      Assert.IsNotNull(dbItems);

      //Equivalent OData Query -- This doesn't
      var resp = await client.GetStringAsync($"odata/v1/{nameof(Employees)}?$top=5&$count=true&$expand=Certifications,HealthItems");
      TestContext.WriteLine($"Server Reponse: {resp}");
      var envelope = JsonConvert.DeserializeObject<ODataEnvelope<Employee>>(resp);
      Assert.AreEqual(envelope.Count, envelope.Value.Count());
      envelope.Value.ToList().ForEach(t =>
      {
        Assert.IsNotNull(t.CreatedBy);
        Assert.IsNotNull(t.CreatedOnUtc);
        Assert.IsTrue(t.IsEnabled);
      });
    }
  }
}
