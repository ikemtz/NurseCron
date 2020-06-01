using System;
using System.Net;
using System.Threading.Tasks;
using NurseCron.Certifications.Abstraction.Models;
using NurseCron.Certifications.WebApi;
using NurseCron.Certifications.WebApi.Data;
using IkeMtz.NRSRx.Core.Unigration;
using IkeMtz.NRSRx.Core.WebApi;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace NurseCron.Certifications.Tests.Unigration.Api
{
  [TestClass]
  public class CertificationsTests : BaseUnigrationTests
  {
    [TestMethod]
    [TestCategory("Unigration")]
    public async Task GetCertificationTest()
    {
      using var srv = new TestServer(TestHostBuilder<Startup, UnigrationWebApiTestStartup>());
      var client = srv.CreateClient();
      GenerateAuthHeader(client, GenerateTestToken());
      //Get 
      var resp = await client.GetAsync($"api/v1/{nameof(Certification)}s.json");

      Assert.AreEqual(HttpStatusCode.OK, resp.StatusCode);
      var result = await resp.Content.ReadAsStringAsync();

      var obj = JsonConvert.DeserializeObject<PingResult>(result);
      Assert.AreEqual("NRSRx Certification API Microservice Controller", obj.Name);
    }

    [TestMethod]
    [TestCategory("Unigration")]
    public async Task SaveCertificationTest()
    {
      var objA = new CertificationInsertDto
      {
        Name = Guid.NewGuid().ToString()
      };
      using var apiSrv = new TestServer(TestHostBuilder<Startup, UnigrationWebApiTestStartup>());
      var apiClient = apiSrv.CreateClient();
      GenerateAuthHeader(apiClient, GenerateTestToken());

      var resp = await apiClient.PostAsJsonAsync($"api/v1/{nameof(Certification)}s.json", objA);

      var result = await DeserializeResponseAsync<Certification>(resp);
      Assert.AreEqual(objA.Name, result.Name);
      Assert.AreEqual("Integration Tester", result.CreatedBy);

      var dbContext = apiSrv.GetDbContext<CertificationsContext>();
      var dbCert = await dbContext.Certifications.FirstOrDefaultAsync(f => f.Name == objA.Name);

      Assert.IsNotNull(dbCert);
      Assert.AreEqual(result.CreatedOnUtc, dbCert.CreatedOnUtc);
    }

    [TestMethod]
    [TestCategory("Unigration")]
    public async Task UpdateCertificationTest()
    {
      var objA = new Certification
      {
        Id = Guid.NewGuid(),
        Name = Guid.NewGuid().ToString(),
        IsEnabled = true,
      };
      using var srv = new TestServer(TestHostBuilder<Startup, UnigrationWebApiTestStartup>()
          .ConfigureTestServices(x =>
          {
            ExecuteOnContext<CertificationsContext>(x, db =>
            {
              _ = db.Certifications.Add(objA);
            });
          })
       );
      var client = srv.CreateClient();
      GenerateAuthHeader(client, GenerateTestToken());
      //Update
      objA.Name = Guid.NewGuid().ToString();
      var resp = await client.PutAsJsonAsync($"api/v1/{nameof(Certification)}s.json?id={objA.Id}", objA);
      var result = await DeserializeResponseAsync<Certification>(resp);

      Assert.AreEqual("Integration Tester", result.UpdatedBy);
      Assert.AreEqual(objA.Name, result.Name);
      var ctx = srv.GetDbContext<CertificationsContext>();
      var cert = await ctx.Certifications.FirstOrDefaultAsync(t => t.Name == result.Name);

      Assert.IsNotNull(cert);
      Assert.AreEqual(result.UpdatedOnUtc, cert.UpdatedOnUtc);
      Assert.AreEqual("Integration Tester", cert.UpdatedBy);
    }

    [TestMethod]
    [TestCategory("Unigration")]
    public async Task UpdateCertificationNotFoundTest()
    {
      var objA = new CertificationUpdateDto
      {
        Name = Guid.NewGuid().ToString(),
        Id = Guid.NewGuid(),
      };
      using var srv = new TestServer(TestHostBuilder<Startup, UnigrationWebApiTestStartup>());
      var client = srv.CreateClient();
      GenerateAuthHeader(client, GenerateTestToken());

      var resp = await client.PutAsJsonAsync($"api/v1/{nameof(Certification)}s.json?id={objA.Id}", objA);
      Assert.AreEqual(HttpStatusCode.NotFound, resp.StatusCode);
      Assert.AreEqual("\"Certification Not Found\"", await resp.Content.ReadAsStringAsync());
    }

    [TestMethod]
    [TestCategory("Unigration")]
    public async Task DeleteCertificationNotFoundTest()
    {
      using var srv = new TestServer(TestHostBuilder<Startup, UnigrationWebApiTestStartup>());
      var client = srv.CreateClient();
      GenerateAuthHeader(client, GenerateTestToken());
      //Delete
      var resp = await client.DeleteAsync($"api/v1/{nameof(Certification)}s.json?id={Guid.NewGuid()}");
      Assert.AreEqual(HttpStatusCode.NotFound, resp.StatusCode);
      Assert.AreEqual("\"Certification Not Found\"", await resp.Content.ReadAsStringAsync());
    }


    [TestMethod]
    [TestCategory("Unigration")]
    public async Task DeleteCertificationTest()
    {
      var objA = new Certification
      {
        Id = Guid.NewGuid(),
        Name = Guid.NewGuid().ToString(),
        IsEnabled = true,
      };
      using var srv = new TestServer(TestHostBuilder<Startup, UnigrationWebApiTestStartup>()
          .ConfigureTestServices(x =>
          {
            ExecuteOnContext<CertificationsContext>(x, db =>
            {
              _ = db.Certifications.Add(objA);
            });
          })
       );
      var client = srv.CreateClient();
      GenerateAuthHeader(client, GenerateTestToken());
      //Update 
      var resp = await client.DeleteAsync($"api/v1/{nameof(Certification)}s.json?id={objA.Id}");

      Assert.AreEqual(HttpStatusCode.OK, resp.StatusCode);
      var result = await DeserializeResponseAsync<Certification>(resp);

      Assert.AreEqual("Integration Tester", result.UpdatedBy);
      Assert.IsFalse(result.IsEnabled);
    }

  }
}
