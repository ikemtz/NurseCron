using NurseCron.Certifications.Abstraction.Models;
using NurseCron.Certifications.OData;
using NurseCron.Certifications.OData.Data;
using IkeMtz.NRSRx.Core.Models;
using IkeMtz.NRSRx.Core.OData;
using IkeMtz.NRSRx.Core.Unigration;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NurseCron.Certifications.Tests.Unigration.OData
{
  [TestClass]
  public partial class CertificationsTests : BaseUnigrationTests
  {
    [TestMethod]
    [TestCategory("Unigration")]
    public async Task GetEnabledCertificationTest()
    {
      var objA = new Certification()
      {
        Id = Guid.NewGuid(),
        Name = "Test",
        IsEnabled = true,
        CreatedOnUtc = DateTime.UtcNow,
      };
      using var srv = new TestServer(TestHostBuilder<Startup, UnigrationODataTestStartup>()
          .ConfigureTestServices(x =>
            {
              ExecuteOnContext<CertificationsContext>(x, db =>
                    {
                      db.Certifications.Add(objA);
                    });
            })
       );
      var client = srv.CreateClient();
      GenerateAuthHeader(client, GenerateTestToken(new[] { new Claim("permissions", "cert:read") }));

      var resp = await client.GetStringAsync("odata/v1/certifications?$count=true");

      TestContext.WriteLine($"Server Reponse: {resp}");
      Assert.IsFalse(resp.ToLower().Contains("updatedby"), $"Validation of {nameof(NrsrxODataSerializer)} failed.");

      var envelope = JsonConvert.DeserializeObject<ODataEnvelope<Certification>>(resp);
      Assert.AreEqual(objA.CreatedOnUtc, envelope.Value.First().CreatedOnUtc.ToUniversalTime());
    }

    [TestMethod]
    [TestCategory("Unigration")]
    public async Task HideDisabledCertificationTest()
    {
      var objA = new Certification()
      {
        Id = Guid.NewGuid(),
        Name = "Test",
        IsEnabled = false
      };
      using var srv = new TestServer(TestHostBuilder<Startup, UnigrationODataTestStartup>()
          .ConfigureTestServices(x =>
          {
            ExecuteOnContext<CertificationsContext>(x, db =>
                  {
                    db.Certifications.Add(objA);
                  });
          })
       );
      var client = srv.CreateClient();
      GenerateAuthHeader(client, GenerateTestToken(new[] { new Claim("scope", "cert:read") }));

      var resp = await client.GetAsync("odata/v1/certifications?$count=true");

      var objB = await DeserializeResponseAsync<ODataEnvelope<Certification>>(resp);

      Assert.AreEqual(0, objB.Value.Count());
    }

    [TestMethod]
    [TestCategory("Unigration")]
    public async Task CertificationSelectTest()
    {
      var objA = new Certification()
      {
        Id = Guid.NewGuid(),
        Name = "Test",
        IsEnabled = true
      };
      using var srv = new TestServer(TestHostBuilder<Startup, UnigrationODataTestStartup>()
          .ConfigureTestServices(x =>
          {
            ExecuteOnContext<CertificationsContext>(x, db =>
                  {
                    db.Certifications.Add(objA);
                  });
          })
       );
      var client = srv.CreateClient();
      GenerateAuthHeader(client, GenerateTestToken(new[] { new Claim("permissions", "cert:read") }));

      var resp = await client.GetAsync("odata/v1/certifications?$select=id&$count=true");

      var objB = await DeserializeResponseAsync<ODataEnvelope<Certification>>(resp);

      Assert.AreEqual(1, objB.Value.Count());
      Assert.IsNull(objB.Value.First().Name);
    }

    [TestMethod]
    [TestCategory("Unigration")]
    public async Task CertificationDateFilterTest()
    {
      var objA = new Certification()
      {
        Id = Guid.NewGuid(),
        Name = "Test",
        IsEnabled = true
      };
      using var srv = new TestServer(TestHostBuilder<Startup, UnigrationODataTestStartup>()
          .ConfigureTestServices(x =>
          {
            ExecuteOnContext<CertificationsContext>(x, db =>
                  {
                    db.Certifications.Add(objA);
                  });
          })
       );
      var client = srv.CreateClient();
      GenerateAuthHeader(client, GenerateTestToken(new[] { new Claim("permissions", "cert:read") }));

      var resp = await client.GetAsync("odata/v1/certifications?filter=createdOnUtc%20gt%202019-01-02T00%3A00%3A00Z&$count=true");

      var objB = await DeserializeResponseAsync<ODataEnvelope<Certification>>(resp);

      Assert.AreEqual(1, objB.Value.Count());

      //TODO: Research bug in EF In Memory Context Or OData
      Assert.AreEqual(1, objB.Value.First().CreatedOnUtc.Year);
    }
  }
}
