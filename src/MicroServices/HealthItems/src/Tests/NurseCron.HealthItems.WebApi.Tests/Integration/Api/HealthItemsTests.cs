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
using NurseCron.HealthItems.Models;
using NurseCron.HealthItems.WebApi;
using NurseCron.HealthItems.WebApi.Data;

namespace NurseCron.HealthItems.Tests.Integration.WebApi
{
  [TestClass]
  public class HealthItemsTests : BaseUnigrationTests
  {
    [TestMethod]
    [TestCategory("Integration")]
    public async Task GetHealthItemTest()
    {
      using var srv = new TestServer(TestHostBuilder<Startup, IntegrationTestStartup>());
      var client = srv.CreateClient();
      GenerateAuthHeader(client, GenerateTestToken());
      //Get 
      var resp = await client.GetAsync($"api/v1/{nameof(HealthItem)}s.json");

      Assert.AreEqual(HttpStatusCode.OK, resp.StatusCode);
      var result = await resp.Content.ReadAsStringAsync();

      var obj = JsonConvert.DeserializeObject<PingResult>(result);
      Assert.AreEqual($"NurseCRON {nameof(HealthItem)} API Microservice Controller", obj.Name);
    }

    [TestMethod]
    [TestCategory("Integration")]
    public async Task SaveHealthItemTest()
    {
      var objA = new HealthItemInsertDto
      {
        Name = Guid.NewGuid().ToString()
      };
      using var srv = new TestServer(TestHostBuilder<Startup, IntegrationTestStartup>());
      var client = srv.CreateClient();
      GenerateAuthHeader(client, GenerateTestToken());

      var resp = await client.PostAsJsonAsync($"api/v1/{nameof(HealthItem)}s.json", objA);
      _ = resp.EnsureSuccessStatusCode();
      var result = await DeserializeResponseAsync<HealthItem>(resp);
      Assert.AreEqual(objA.Name, result.Name);
      Assert.IsTrue(result.IsEnabled);
      Assert.IsNotNull(result.CreatedBy);

      var ctx = srv.GetDbContext<DatabaseContext>();
      var item = await ctx.HealthItems.FirstOrDefaultAsync(t => t.Name == result.Name);

      Assert.IsNotNull(item);
      Assert.AreEqual(result.CreatedOnUtc.ToString(), item.CreatedOnUtc.ToString());
    }

    [TestMethod]
    [TestCategory("Integration")]
    public async Task UpdateHealthItemTest()
    {
      var objA = new HealthItem
      {
        Id = Guid.NewGuid(),
        Name = Guid.NewGuid().ToString(),
        IsEnabled = true,
      };
      using var srv = new TestServer(TestHostBuilder<Startup, IntegrationTestStartup>());
      var client = srv.CreateClient();
      GenerateAuthHeader(client, GenerateTestToken());

      var resp = await client.PostAsJsonAsync($"api/v1/{nameof(HealthItem)}s.json?id={objA.Id}", objA);
      _ = resp.EnsureSuccessStatusCode();
      objA = await DeserializeResponseAsync<HealthItem>(resp);

      //Update
      objA.Name = Guid.NewGuid().ToString();
      resp = await client.PutAsJsonAsync($"api/v1/{nameof(HealthItem)}s.json?id={objA.Id}", objA);
      _ = resp.EnsureSuccessStatusCode();
      var result = await DeserializeResponseAsync<HealthItem>(resp);

      Assert.IsNotNull(result.UpdatedBy);
      Assert.AreEqual(objA.Name, result.Name);

      var ctx = srv.GetDbContext<DatabaseContext>();
      var item = await ctx.HealthItems.FirstOrDefaultAsync(t => t.Name == result.Name);

      Assert.IsNotNull(item);
      Assert.AreEqual(result.UpdatedOnUtc.ToString(), item.UpdatedOnUtc.ToString());
      Assert.IsNotNull(item.UpdatedBy);
    }

    [TestMethod]
    [TestCategory("Integration")]
    public async Task UpdateHealthItemNotFoundTest()
    {
      var objA = new HealthItemUpdateDto
      {
        Name = Guid.NewGuid().ToString(),
        Id = Guid.NewGuid(),
      };
      using var srv = new TestServer(TestHostBuilder<Startup, IntegrationTestStartup>());
      var client = srv.CreateClient();
      GenerateAuthHeader(client, GenerateTestToken());

      var resp = await client.PutAsJsonAsync($"api/v1/{nameof(HealthItem)}s.json?id={objA.Id}", objA);
      Assert.AreEqual(HttpStatusCode.NotFound, resp.StatusCode);
      Assert.AreEqual($"\"{nameof(HealthItem)} Not Found\"", await resp.Content.ReadAsStringAsync());
    }

    [TestMethod]
    [TestCategory("Integration")]
    public async Task DeleteHealthItemTest()
    {
      var objA = new HealthItem
      {
        Id = Guid.NewGuid(),
        Name = Guid.NewGuid().ToString(),
        IsEnabled = true,
      };
      using var srv = new TestServer(TestHostBuilder<Startup, IntegrationTestStartup>());
      var client = srv.CreateClient();
      GenerateAuthHeader(client, GenerateTestToken());

      var resp = await client.PostAsJsonAsync($"api/v1/{nameof(HealthItem)}s.json?id={objA.Id}", objA);
      _ = resp.EnsureSuccessStatusCode();
      objA = await DeserializeResponseAsync<HealthItem>(resp);
      //Delete 
      resp = await client.DeleteAsync($"api/v1/{nameof(HealthItem)}s.json?id={objA.Id}");
      _ = resp.EnsureSuccessStatusCode();
      Assert.AreEqual(HttpStatusCode.OK, resp.StatusCode);
      var result = await DeserializeResponseAsync<HealthItem>(resp);

      Assert.IsNotNull(result.UpdatedBy);
      Assert.IsFalse(result.IsEnabled);
    }

    [TestMethod]
    [TestCategory("Integration")]
    public async Task DeleteHealthItemNotFoundTest()
    {
      using var srv = new TestServer(TestHostBuilder<Startup, IntegrationTestStartup>());
      var client = srv.CreateClient();
      GenerateAuthHeader(client, GenerateTestToken());
      //Delete
      var resp = await client.DeleteAsync($"api/v1/{nameof(HealthItem)}s.json?id={Guid.NewGuid()}");
      Assert.AreEqual(HttpStatusCode.NotFound, resp.StatusCode);
      Assert.AreEqual($"\"{nameof(HealthItem)} Not Found\"", await resp.Content.ReadAsStringAsync());
    }
  }
}
