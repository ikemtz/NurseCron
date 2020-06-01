using NurseCron.Certifications.Abstraction.Models;
using NurseCron.Certifications.OData;
using IkeMtz.NRSRx.Core.Models;
using IkeMtz.NRSRx.Core.Unigration;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Net;
using NurseCron.Seurity;

namespace NurseCron.Certifications.Tests.Integration.OData
{
  [TestClass]
  public partial class CertificationsTests : BaseUnigrationTests
  {
    [TestMethod]
    [TestCategory("Integration")]
    public async Task GetEnabledCertificationsTest()
    {
      using var srv = new TestServer(TestHostBuilder<Startup, IntegrationODataTestStartup>());
      var client = srv.CreateClient();
      GenerateAuthHeader(client, GenerateTestToken(new[] { new Claim("permissions", Permissions.ReadCertifications) }));

      var resp = await client.GetStringAsync("odata/v1/Certifications?$count=true");
      TestContext.WriteLine($"Server Reponse: {resp}");
      var envelope = JsonConvert.DeserializeObject<ODataEnvelope<Certification>>(resp);
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
    public async Task CertificationSelectTest()
    {
      using var srv = new TestServer(TestHostBuilder<Startup, IntegrationODataTestStartup>());
      var client = srv.CreateClient();
      GenerateAuthHeader(client, GenerateTestToken(new[] { new Claim("permissions", Permissions.ReadCertifications) }));

      var resp = await client.GetAsync("odata/v1/certifications?$select=id&top=1&$count=true");

      var objB = await DeserializeResponseAsync<ODataEnvelope<Certification>>(resp);

      Assert.IsNull(objB.Value.FirstOrDefault()?.Name);
    }

    [TestMethod]
    [TestCategory("Integration")]
    public async Task CertificationDateFilterTest()
    {
      using var srv = new TestServer(TestHostBuilder<Startup, IntegrationODataTestStartup>());
      var client = srv.CreateClient();
      GenerateAuthHeader(client, GenerateTestToken(new[] { new Claim("permissions", Permissions.ReadCertifications) }));

      var resp = await client.GetAsync("odata/v1/certifications?filter=createdOnUtc%20gt%202019-01-02T00%3A00%3A00Z&$top=1&$count=true");

      var objB = await DeserializeResponseAsync<ODataEnvelope<Certification>>(resp);

      Assert.AreEqual(HttpStatusCode.OK, resp.StatusCode);
    }
  }
}
