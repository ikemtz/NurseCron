using System;
using System.Net;
using System.Threading.Tasks;
using IkeMtz.NRSRx.Core.Unigration;
using IkeMtz.NRSRx.Core.Unigration.Http;
using IkeMtz.NRSRx.Core.WebApi;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using NurseCron.Employees.Models;
using NurseCron.Employees.WebApi;
using NurseCron.Employees.WebApi.Data;

namespace NurseCron.Employees.Tests.Unigration.Api
{
  [TestClass]
  public class EmployeesTests : BaseUnigrationTests
  {
    [TestMethod]
    [TestCategory("Unigration")]
    public async Task GetEmployeesTest()
    {
      using var srv = new TestServer(TestHostBuilder<Startup, UnigrationWebApiTestStartup>());
      var client = srv.CreateClient();
      GenerateAuthHeader(client, GenerateTestToken());
      //Get 
      var resp = await client.GetAsync($"api/v1/Employees.json");

      Assert.AreEqual(HttpStatusCode.OK, resp.StatusCode);
      var result = await resp.Content.ReadAsStringAsync();

      var obj = JsonConvert.DeserializeObject<PingResult>(result);
      Assert.AreEqual("NurseCRON Employee API Microservice Controller", obj.Name);
    }

    [TestMethod]
    [TestCategory("Unigration")]
    public async Task SaveEmployeeTest()
    {
      var objA = new EmployeeInsertDto
      {
        FirstName = Guid.NewGuid().ToString(),
        LastName = Guid.NewGuid().ToString(),
        Email = $"{Guid.NewGuid()}@email.com",
      };
      using var srv = new TestServer(TestHostBuilder<Startup, UnigrationWebApiTestStartup>());
      var client = srv.CreateClient();
      GenerateAuthHeader(client, GenerateTestToken());

      var resp = await client.PostAsJsonAsync("api/v1/Employees.json", objA);
      _ = resp.EnsureSuccessStatusCode();
      var result = await DeserializeResponseAsync<Employee>(resp);
      Assert.AreEqual(objA.FirstName, result.FirstName);
      Assert.AreEqual("IntegrationTester@email.com", result.CreatedBy);

      var ctx = srv.GetDbContext<DatabaseContext>();
      var emp = await ctx.Employees.FirstOrDefaultAsync(t => t.FirstName == result.FirstName);

      Assert.IsNotNull(emp);
      Assert.AreEqual(result.CreatedOnUtc, emp.CreatedOnUtc);
    }

    [TestMethod]
    [TestCategory("Unigration")]
    public async Task UpdateEmployeeTest()
    {
      var objA = new Employee
      {
        Id = Guid.NewGuid(),
        HireDate = DateTime.UtcNow.AddMonths(-6),
        FirstName = Guid.NewGuid().ToString(),
        IsEnabled = true,
        Email = "me@me.com",
        LastName = TestDataFactory.StringGenerator(20),
      };
      using var srv = new TestServer(TestHostBuilder<Startup, UnigrationWebApiTestStartup>()
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
      //Update
      objA.FirstName = Guid.NewGuid().ToString();
      var resp = await client.PutAsJsonAsync($"api/v1/Employees.json?id={objA.Id}", objA);
      var result = await DeserializeResponseAsync<Employee>(resp);

      Assert.AreEqual("IntegrationTester@email.com", result.UpdatedBy);
      Assert.AreEqual(objA.FirstName, result.FirstName);


      var ctx = srv.GetDbContext<DatabaseContext>();
      var emp = await ctx.Employees.FirstOrDefaultAsync(t => t.FirstName == result.FirstName);

      Assert.IsNotNull(emp);
      Assert.AreEqual(result.UpdatedOnUtc, emp.UpdatedOnUtc);
      Assert.AreEqual("IntegrationTester@email.com", emp.UpdatedBy);
    }

    [TestMethod]
    [TestCategory("Unigration")]
    public async Task UpdateEmployeeNotFoundTest()
    {
      var objA = new EmployeeUpdateDto
      {
        FirstName = Guid.NewGuid().ToString(),
        HireDate = DateTime.UtcNow.AddMonths(-6),
        Id = Guid.NewGuid(),
      };
      using var srv = new TestServer(TestHostBuilder<Startup, UnigrationWebApiTestStartup>());
      var client = srv.CreateClient();
      GenerateAuthHeader(client, GenerateTestToken());

      var resp = await client.PutAsJsonAsync($"api/v1/Employees.json?id={objA.Id}", objA);
      Assert.AreEqual(HttpStatusCode.NotFound, resp.StatusCode);
      Assert.AreEqual($"\"{nameof(Employee)} Not Found\"", await resp.Content.ReadAsStringAsync());
    }

    [TestMethod]
    [TestCategory("Unigration")]
    public async Task DeleteEmployeeTest()
    {
      var objA = new Employee
      {
        Id = Guid.NewGuid(),
        FirstName = Guid.NewGuid().ToString(),
        IsEnabled = true,
        Email = "me@me.com",
        LastName = TestDataFactory.StringGenerator(20),
      };
      using var srv = new TestServer(TestHostBuilder<Startup, UnigrationWebApiTestStartup>()
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
      //Update 
      var resp = await client.DeleteAsync($"api/v1/Employees.json?id={objA.Id}");

      Assert.AreEqual(HttpStatusCode.OK, resp.StatusCode);
      var result = await DeserializeResponseAsync<Employee>(resp);

      Assert.AreEqual("IntegrationTester@email.com", result.UpdatedBy);
      Assert.IsFalse(result.IsEnabled);
    }

    [TestMethod]
    [TestCategory("Unigration")]
    public async Task DeleteEmployeeNotFoundTest()
    {
      using var srv = new TestServer(TestHostBuilder<Startup, UnigrationWebApiTestStartup>());
      var client = srv.CreateClient();
      GenerateAuthHeader(client, GenerateTestToken());
      //Delete
      var resp = await client.DeleteAsync($"api/v1/Employees.json?id={Guid.NewGuid()}");
      Assert.AreEqual(HttpStatusCode.NotFound, resp.StatusCode);
      Assert.AreEqual($"\"{nameof(Employee)} Not Found\"", await resp.Content.ReadAsStringAsync());
    }
  }
}
