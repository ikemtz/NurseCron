using System.Linq;
using System.Threading.Tasks;
using IkeMtz.NRSRx.Core.Models;
using IkeMtz.NRSRx.Core.Unigration;
using NurseCron.Employees.Models;
using NurseCron.Employees.OData.Data;
using NurseCron.Employees.Tests.Integration;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace NurseCron.Employees.OData.Tests.Integration.OData
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
        .Include(t => t.EmployeeCompetencies)
        .Include(t => t.EmployeeCertifications)
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
        .Include(t => t.EmployeeCompetencies)
        .Include(t => t.EmployeeHealthItems)
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
        .Include(t => t.EmployeeCertifications)
        .Include(t => t.EmployeeCompetencies)
        .Include(t => t.EmployeeHealthItems)
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

    [TestMethod]
    [TestCategory("Integration")]
    public async Task GetEmployees_CompsAndCertsAndHltItems_Test()
    {
      using var srv = new TestServer(TestHostBuilder<Startup, IntegrationTestStartup>());
      var client = srv.CreateClient();
      GenerateAuthHeader(client, GenerateTestToken());

      //Equivalent EfQuery -- THIS WORKS
      var dbContext = srv.GetDbContext<EmployeesContext>();
      var dbItems = await dbContext.Employees
        .Include(t => t.EmployeeCompetencies)
        .Include(t => t.EmployeeCertifications)
        .Include(t => t.EmployeeHealthItems)
        .Take(5)
        .ToListAsync();
      Assert.IsNotNull(dbItems);

      //Equivalent OData Query -- This doesn't
      var resp = await client.GetStringAsync($"odata/v1/{nameof(Employees)}?$top=5&$count=true&$expand=Competencies,Certifications,HealthItems");
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
