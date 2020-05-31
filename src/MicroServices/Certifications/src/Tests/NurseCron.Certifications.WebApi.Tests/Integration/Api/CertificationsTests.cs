using System;
using System.Net;
using System.Threading.Tasks;
using NurseCron.Certifications.Abstraction.Models;
using NurseCron.Certifications.WebApi;
using NurseCron.Certifications.WebApi.Data;
using IkeMtz.NRSRx.Core.Unigration;
using IkeMtz.NRSRx.Events;
using IkeMtz.NRSRx.Events.Publishers.ServiceBus;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Azure.ServiceBus;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace NurseCron.Certifications.Tests.Integration.Api
{
  [TestClass]
  public class CertificationsTests : BaseUnigrationTests
  {
    [TestMethod]
    [TestCategory("Integration")]
    public async Task GetCertificationTest()
    {
      using var srv = new TestServer(TestHostBuilder<Startup, IntegrationWebApiTestStartup>());
      var client = srv.CreateClient();
      GenerateAuthHeader(client, GenerateTestToken());
      //Get 
      var resp = await client.GetAsync($"api/v1/Certifications.json");

      Assert.AreEqual(HttpStatusCode.OK, resp.StatusCode);
      var result = await resp.Content.ReadAsStringAsync();

      var obj = JsonConvert.DeserializeObject<Certification>(result);
      Assert.AreEqual("NRSRx Certification API Microservice Controller", obj.Name);
    }

    [TestMethod]
    [TestCategory("Integration")]
    public async Task SaveCertificationTest()
    {
      var objA = new CertificationInsertRequest
      {
        Name = Guid.NewGuid().ToString()
      };
      using var srv = new TestServer(TestHostBuilder<Startup, IntegrationWebApiTestStartup>());
      var client = srv.CreateClient();
      GenerateAuthHeader(client, GenerateTestToken());

      var resp = await client.PutAsJsonAsync("api/v1/Certifications.json", objA);
      _ = resp.EnsureSuccessStatusCode();
      var result = await DeserializeResponseAsync<Certification>(resp);
      Assert.AreEqual(objA.Name, result.Name);
      Assert.IsTrue(result.IsEnabled);
      Assert.IsNotNull(result.CreatedBy);
      var ctx = srv.GetDbContext<CertificationsContext>();
      var cert = await ctx.Certifications.FirstOrDefaultAsync(t => t.Name == objA.Name);
      Assert.IsNotNull(cert);
      Assert.AreEqual(result.CreatedOnUtc.ToString(), cert.CreatedOnUtc.ToString());

      var publisher = srv.GetTestService<IPublisher<Certification, CreatedEvent, Message>, ServiceBusQueuePublisher<Certification, CreatedEvent>>();
      Assert.IsNotNull(publisher);
    }

    [TestMethod]
    [TestCategory("Integration")]
    public async Task UpdateCertificationTest()
    {
      var objA = new Certification
      {
        Id = Guid.NewGuid(),
        Name = Guid.NewGuid().ToString(),
        IsEnabled = true,
      };
      using var srv = new TestServer(TestHostBuilder<Startup, IntegrationWebApiTestStartup>());
      var client = srv.CreateClient();
      GenerateAuthHeader(client, GenerateTestToken());

      var resp = await client.PutAsJsonAsync($"api/v1/Certifications.json?id={objA.Id}", objA);
      resp.EnsureSuccessStatusCode();
      objA = await DeserializeResponseAsync<Certification>(resp);

      //Update
      objA.Name = Guid.NewGuid().ToString();
      resp = await client.PostAsJsonAsync($"api/v1/Certifications.json?id={objA.Id}", objA);
      resp.EnsureSuccessStatusCode();
      var result = await DeserializeResponseAsync<Certification>(resp);

      Assert.IsNotNull(result.UpdatedBy);
      Assert.AreEqual(objA.Name, result.Name);
      var ctx = srv.GetDbContext<CertificationsContext>();
      var cert = await ctx.Certifications.FirstOrDefaultAsync(t => t.Name == result.Name);

      Assert.IsNotNull(cert);
      Assert.AreEqual(result.UpdatedOnUtc.ToString(), cert.UpdatedOnUtc.ToString());
      Assert.IsNotNull(cert.UpdatedBy);

      var publisher = srv.GetTestService<IPublisher<Certification, UpdatedEvent, Message>, ServiceBusQueuePublisher<Certification, UpdatedEvent>>();
      Assert.IsNotNull(publisher);
    }

    [TestMethod]
    [TestCategory("Integration")]
    public async Task UpdateCertificationNotFoundTest()
    {
      var objA = new CertificationUpdateRequest
      {
        Name = Guid.NewGuid().ToString(),
        Id = Guid.NewGuid(),
      };
      using var srv = new TestServer(TestHostBuilder<Startup, IntegrationWebApiTestStartup>());
      var client = srv.CreateClient();
      GenerateAuthHeader(client, GenerateTestToken());

      var resp = await client.PostAsJsonAsync($"api/v1/Certifications.json?id={objA.Id}", objA);
      Assert.AreEqual(HttpStatusCode.NotFound, resp.StatusCode);
      Assert.AreEqual($"\"{nameof(Certification)} Not Found\"", await resp.Content.ReadAsStringAsync());
    }

    [TestMethod]
    [TestCategory("Integration")]
    public async Task DeleteCertificationTest()
    {
      var objA = new Certification
      {
        Id = Guid.NewGuid(),
        Name = Guid.NewGuid().ToString(),
        IsEnabled = true,
      };
      using var srv = new TestServer(TestHostBuilder<Startup, IntegrationWebApiTestStartup>());
      var client = srv.CreateClient();
      GenerateAuthHeader(client, GenerateTestToken());

      var resp = await client.PutAsJsonAsync($"api/v1/Certifications.json?id={objA.Id}", objA);
      _ = resp.EnsureSuccessStatusCode();
      objA = await DeserializeResponseAsync<Certification>(resp);
      //Delete 
      resp = await client.DeleteAsync($"api/v1/Certifications.json?id={objA.Id}");
      _ = resp.EnsureSuccessStatusCode();
      Assert.AreEqual(HttpStatusCode.OK, resp.StatusCode);
      var result = await DeserializeResponseAsync<Certification>(resp);

      Assert.IsNotNull(result.UpdatedBy);
      Assert.IsFalse(result.IsEnabled);

      var publisher = srv.GetTestService<IPublisher<Certification, DeletedEvent, Message>, ServiceBusQueuePublisher<Certification, DeletedEvent>>();
      Assert.IsNotNull(publisher);
    }

    [TestMethod]
    [TestCategory("Integration")]
    public async Task DeleteCertificationNotFoundTest()
    {
      using var srv = new TestServer(TestHostBuilder<Startup, IntegrationWebApiTestStartup>());
      var client = srv.CreateClient();
      GenerateAuthHeader(client, GenerateTestToken());
      //Delete
      var resp = await client.DeleteAsync($"api/v1/Certifications.json?id={Guid.NewGuid()}");
      Assert.AreEqual(HttpStatusCode.NotFound, resp.StatusCode);
      Assert.AreEqual($"\"{nameof(Certification)} Not Found\"", await resp.Content.ReadAsStringAsync());
    }
  }
}
