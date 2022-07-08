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

namespace NurseCron.Employees.Tests.Integration.Api
{
  [TestClass]
  public class EmployeesTests : BaseUnigrationTests
  {
    [TestMethod]
    [TestCategory("Integration")]
    public async Task GetEmployeeTest()
    {
      using var srv = new TestServer(TestHostBuilder<Startup, IntegrationTestStartup>());
      var client = srv.CreateClient();
      GenerateAuthHeader(client, GenerateTestToken());
      //Get 
      var resp = await client.GetAsync($"api/v1/{nameof(Employees)}.json");

      Assert.AreEqual(HttpStatusCode.OK, resp.StatusCode);
      var result = await resp.Content.ReadAsStringAsync();

      var obj = JsonConvert.DeserializeObject<PingResult>(result);
      Assert.AreEqual("NRSRx Employee API Microservice Controller", obj.Name);
    }

    [TestMethod]
    [TestCategory("Integration")]
    public async Task SaveEmployeeTest()
    {
      var objA = new EmployeeInsertDto
      {
        FirstName = Guid.NewGuid().ToString(),
        LastName = Guid.NewGuid().ToString(),
        Email = $"{Guid.NewGuid()}@email.com",


      };
      objA.EmployeeCertifications.Add(
         new EmployeeCertification
         {
           CertificationId = Guid.NewGuid(),
           CertificationName = Guid.NewGuid().ToString(),
         });
      objA.EmployeeCertifications.Add(
                     new EmployeeCertification
                     {
                       CertificationId = Guid.NewGuid(),
                       CertificationName = Guid.NewGuid().ToString(),
                       ExpiresOnUtc = DateTime.UtcNow.AddDays(new Random().Next(1, 20)),
                     });
      objA.EmployeeCompetencies.Add(
          new EmployeeCompetency
          {
            CompetencyId = Guid.NewGuid(),
            CompetencyName = Guid.NewGuid().ToString(),
          });
      objA.EmployeeCompetencies.Add(
                     new EmployeeCompetency
                     {
                       CompetencyId = Guid.NewGuid(),
                       CompetencyName = Guid.NewGuid().ToString(),
                       ExpiresOnUtc = DateTime.UtcNow.AddDays(new Random().Next(1, 20)),
                       IsEnabled = true
                     });
      objA.EmployeeHealthItems.Add(new EmployeeHealthItem
      {
        HealthItemId = Guid.NewGuid(),
        HealthItemName = Guid.NewGuid().ToString(),
      });
      objA.EmployeeHealthItems.Add(
                     new EmployeeHealthItem
                     {
                       HealthItemId = Guid.NewGuid(),
                       HealthItemName = Guid.NewGuid().ToString(),
                       ExpiresOnUtc = DateTime.UtcNow.AddDays(new Random().Next(1, 20)),
                       IsEnabled = true
                     });
      using var srv = new TestServer(TestHostBuilder<Startup, IntegrationTestStartup>());
      var client = srv.CreateClient();
      GenerateAuthHeader(client, GenerateTestToken());

      var resp = await client.PostAsJsonAsync($"api/v1/{nameof(Employees)}.json", objA);
      _ = resp.EnsureSuccessStatusCode();
      var result = await DeserializeResponseAsync<Employee>(resp);
      Assert.AreEqual(objA.FirstName, result.FirstName);
      Assert.IsTrue(result.IsEnabled);
      Assert.IsNotNull(result.CreatedBy);

      var ctx = srv.GetDbContext<DatabaseContext>();
      var emp = await ctx.Employees.Include(t => t.EmployeeCertifications).FirstOrDefaultAsync(t => t.FirstName == result.FirstName);

      Assert.IsNotNull(emp);
      Assert.AreEqual(result.CreatedOnUtc.ToString(), emp.CreatedOnUtc.ToString());
      Assert.AreEqual(2, emp.EmployeeCertifications.Count);
    }

    [TestMethod]
    [TestCategory("Integration")]
    public async Task UpdateEmployeeTest()
    {
      var objA = new Employee
      {
        Id = Guid.NewGuid(),
        FirstName = Guid.NewGuid().ToString(),
        LastName = Guid.NewGuid().ToString(),
        Email = $"{Guid.NewGuid()}@email.com",
        HireDate = DateTime.UtcNow.AddMonths(-23),
        IsEnabled = true,
      };
      objA.EmployeeCertifications.Add(new EmployeeCertification()
      {
        CertificationId = Guid.NewGuid(),
        CertificationName = Guid.NewGuid().ToString(),
      });
      using var srv = new TestServer(TestHostBuilder<Startup, IntegrationTestStartup>());
      var client = srv.CreateClient();
      GenerateAuthHeader(client, GenerateTestToken());

      var resp = await client.PostAsJsonAsync($"api/v1/{nameof(Employees)}.json?id={objA.Id}", objA);
      _ = resp.EnsureSuccessStatusCode();
      objA = await DeserializeResponseAsync<Employee>(resp);

      //Update
      objA.FirstName = Guid.NewGuid().ToString();
      objA.EmployeeCertifications.Add(new EmployeeCertification()
      {
        CertificationId = Guid.NewGuid(),
        CertificationName = Guid.NewGuid().ToString(),
      });
      resp = await client.PutAsJsonAsync($"api/v1/{nameof(Employees)}.json?id={objA.Id}", objA);
      _ = resp.EnsureSuccessStatusCode();
      var result = await DeserializeResponseAsync<Employee>(resp);

      Assert.IsNotNull(result.UpdatedBy);
      Assert.AreEqual(objA.FirstName, result.FirstName);

      var ctx = srv.GetDbContext<DatabaseContext>();
      var emp = await ctx.Employees.FirstOrDefaultAsync(t => t.FirstName == result.FirstName);

      Assert.IsNotNull(emp);
      Assert.AreEqual(result.UpdatedOnUtc.ToString(), emp.UpdatedOnUtc.ToString());
      Assert.IsNotNull(emp.UpdatedBy);
      Assert.AreEqual(2, result.EmployeeCertifications.Count);
    }

    [TestMethod]
    [TestCategory("Integration")]
    public async Task UpdateEmployeeNotFoundTest()
    {
      var objA = new EmployeeUpdateDto
      {
        FirstName = Guid.NewGuid().ToString(),
        Id = Guid.NewGuid(),
        HireDate = DateTime.UtcNow.AddMonths(-23),
      };
      using var srv = new TestServer(TestHostBuilder<Startup, IntegrationTestStartup>());
      var client = srv.CreateClient();
      GenerateAuthHeader(client, GenerateTestToken());

      var resp = await client.PutAsJsonAsync($"api/v1/{nameof(Employees)}.json?id={objA.Id}", objA);
      Assert.AreEqual(HttpStatusCode.NotFound, resp.StatusCode);
      Assert.AreEqual($"\"{nameof(Employee)} Not Found\"", await resp.Content.ReadAsStringAsync());
    }

    [TestMethod]
    [TestCategory("Integration")]
    public async Task DeleteEmployeeTest()
    {
      var objA = new Employee
      {
        Id = Guid.NewGuid(),
        FirstName = Guid.NewGuid().ToString(),
        LastName = Guid.NewGuid().ToString(),
        Email = $"{Guid.NewGuid()}@email.com",
        IsEnabled = true,
      };
      using var srv = new TestServer(TestHostBuilder<Startup, IntegrationTestStartup>());
      var client = srv.CreateClient();
      GenerateAuthHeader(client, GenerateTestToken());

      var resp = await client.PostAsJsonAsync($"api/v1/{nameof(Employees)}.json?id={objA.Id}", objA);
      _ = resp.EnsureSuccessStatusCode();
      objA = await DeserializeResponseAsync<Employee>(resp);
      //Delete 
      resp = await client.DeleteAsync($"api/v1/{nameof(Employees)}.json?id={objA.Id}");
      _ = resp.EnsureSuccessStatusCode();
      Assert.AreEqual(HttpStatusCode.OK, resp.StatusCode);
      var result = await DeserializeResponseAsync<Employee>(resp);

      Assert.IsNotNull(result.UpdatedBy);
      Assert.IsFalse(result.IsEnabled);
    }

    [TestMethod]
    [TestCategory("Integration")]
    public async Task DeleteEmployeeNotFoundTest()
    {
      using var srv = new TestServer(TestHostBuilder<Startup, IntegrationTestStartup>());
      var client = srv.CreateClient();
      GenerateAuthHeader(client, GenerateTestToken());
      //Delete
      var resp = await client.DeleteAsync($"api/v1/{nameof(Employees)}.json?id={Guid.NewGuid()}");
      Assert.AreEqual(HttpStatusCode.NotFound, resp.StatusCode);
      Assert.AreEqual($"\"{nameof(Employee)} Not Found\"", await resp.Content.ReadAsStringAsync());
    }
  }
}
