using System.Linq;
using System.Threading.Tasks;
using IkeMtz.NRSRx.Core.Models;
using IkeMtz.NRSRx.Core.Unigration;
using NurseCron.Employees.Models;
using NurseCron.Employees.OData;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace NurseCron.Employees.Tests.Integration.OData
{
  [TestClass]
  public partial class EmployeesTests : BaseUnigrationTests
  {
    [TestMethod]
    [TestCategory("Integration")]
    public async Task GetEnabledEmployeesTest()
    {
      using var srv = new TestServer(TestHostBuilder<Startup, IntegrationTestStartup>());
      var client = srv.CreateClient();
      GenerateAuthHeader(client, GenerateTestToken());

      var resp = await client.GetStringAsync($"odata/v1/{nameof(Employees)}?$count=true");
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
    public async Task GetEmployeesCertificationsTest()
    {
      using var srv = new TestServer(TestHostBuilder<Startup, IntegrationTestStartup>());
      var client = srv.CreateClient();
      GenerateAuthHeader(client, GenerateTestToken());

      var resp = await client.GetStringAsync($"odata/v1/{nameof(Employees)}?$count=true&$expand=Certifications");
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
    public async Task GetEmployeesCompetenciesTest()
    {
      using var srv = new TestServer(TestHostBuilder<Startup, IntegrationTestStartup>());
      var client = srv.CreateClient();
      GenerateAuthHeader(client, GenerateTestToken());

      var resp = await client.GetStringAsync($"odata/v1/{nameof(Employees)}?$count=true&$expand=Competencies");
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
    public async Task GetEmployeesHealthItemsTest()
    {
      using var srv = new TestServer(TestHostBuilder<Startup, IntegrationTestStartup>());
      var client = srv.CreateClient();
      GenerateAuthHeader(client, GenerateTestToken());

      var resp = await client.GetStringAsync($"odata/v1/{nameof(Employees)}?$count=true&$expand=HealthItems");
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
