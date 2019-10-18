﻿using IkeMtz.NRSRx.Competencies.Abstraction.Models;
using IkeMtz.NRSRx.Competencies.WebApi;
using IkeMtz.NRSRx.Competencies.WebApi.Data;
using IkeMtz.NRSRx.Core.Unigration;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace IkeMtz.NRSRx.Competencies.Tests.Integration.WebApi
{
    [TestClass]
    public class CompetenciesTests : BaseUnigrationTests
    {
        [TestMethod]
        [TestCategory("Integration")]
        public async Task GetCompetencyTest()
        {
            using (var srv = new TestServer(TestHostBuilder<Startup, IntegrationTestStartup>()))
            {
                var client = srv.CreateClient();
                GenerateAuthHeader(client, GenerateTestToken());
                //Get 
                var resp = await client.GetAsync($"api/v1/Competencies.json");

                Assert.AreEqual(HttpStatusCode.OK, resp.StatusCode);
                var result = await resp.Content.ReadAsStringAsync();

                var obj = JsonConvert.DeserializeObject<dynamic>(result);
                Assert.AreEqual("NRSRx Competency API Microservice Controller", obj.name.ToString());
            }
        }

        [TestMethod]
        [TestCategory("Integration")]
        public async Task SaveCompetencyTest()
        {
            var objA = new CompetencyInsertRequest
            {
                Name = Guid.NewGuid().ToString()
            };
            using (var srv = new TestServer(TestHostBuilder<Startup, IntegrationTestStartup>()))
            {
                var client = srv.CreateClient();
                GenerateAuthHeader(client, await GenerateTokenAsync());

                var resp = await client.PutAsJsonAsync("api/v1/Competencies.json", objA);
                resp.EnsureSuccessStatusCode();
                var result = await DeserializeResponseAsync<Competency>(resp);
                Assert.AreEqual(objA.Name, result.Name);
                Assert.IsTrue(result.IsEnabled);
                Assert.IsNotNull(result.CreatedBy);

                var ctx = srv.GetDbContext<CompetenciesContext>();
                var comp = await ctx.Competencies.FirstOrDefaultAsync(t => t.Name == result.Name);

                Assert.IsNotNull(comp);
                Assert.AreEqual(result.CreatedOnUtc.ToString(), comp.CreatedOnUtc.ToString());
            }
        }

        [TestMethod]
        [TestCategory("Integration")]
        public async Task UpdateCompetencyTest()
        {
            var objA = new Competency
            {
                Id = Guid.NewGuid(),
                Name = Guid.NewGuid().ToString(),
                IsEnabled = true,
            };
            using (var srv = new TestServer(TestHostBuilder<Startup, IntegrationTestStartup>()))
            {
                var client = srv.CreateClient();
                GenerateAuthHeader(client, await GenerateTokenAsync());

                var resp = await client.PutAsJsonAsync($"api/v1/Competencies.json?id={objA.Id}", new CompetencyInsertRequest(objA));
                resp.EnsureSuccessStatusCode();
                objA = await DeserializeResponseAsync<Competency>(resp);

                //Update
                objA.Name = Guid.NewGuid().ToString();
                resp = await client.PostAsJsonAsync($"api/v1/Competencies.json?id={objA.Id}", new CompetencyUpdateRequest(objA));
                resp.EnsureSuccessStatusCode();
                var result = await DeserializeResponseAsync<Competency>(resp);

                Assert.IsNotNull(result.UpdatedBy);
                Assert.AreEqual(objA.Name, result.Name);

                var ctx = srv.GetDbContext<CompetenciesContext>();
                var comp = await ctx.Competencies.FirstOrDefaultAsync(t => t.Name == result.Name);

                Assert.IsNotNull(comp);
                Assert.AreEqual(result.UpdatedOnUtc.ToString(), comp.UpdatedOnUtc.ToString());
                Assert.IsNotNull(comp.UpdatedBy);
            }
        }

        [TestMethod]
        [TestCategory("Integration")]
        public async Task UpdateCompetencyNotFoundTest()
        {
            var objA = new CompetencyUpdateRequest
            {
                Name = Guid.NewGuid().ToString(),
                Id = Guid.NewGuid(),
            };
            using (var srv = new TestServer(TestHostBuilder<Startup, IntegrationTestStartup>()))
            {
                var client = srv.CreateClient();
                GenerateAuthHeader(client, await GenerateTokenAsync());

                var resp = await client.PostAsJsonAsync($"api/v1/Competencies.json?id={objA.Id}", objA);
                Assert.AreEqual(HttpStatusCode.NotFound, resp.StatusCode);
                Assert.AreEqual($"\"{nameof(Competency)} Not Found\"", await resp.Content.ReadAsStringAsync());
            }
        }

        [TestMethod]
        [TestCategory("Integration")]
        public async Task DeleteCompetencyTest()
        {
            var objA = new Competency
            {
                Id = Guid.NewGuid(),
                Name = Guid.NewGuid().ToString(),
                IsEnabled = true,
            };
            using (var srv = new TestServer(TestHostBuilder<Startup, IntegrationTestStartup>()))
            {
                var client = srv.CreateClient();
                GenerateAuthHeader(client, await GenerateTokenAsync());

                var resp = await client.PutAsJsonAsync($"api/v1/Competencies.json?id={objA.Id}", new CompetencyInsertRequest(objA));
                resp.EnsureSuccessStatusCode();
                objA = await DeserializeResponseAsync<Competency>(resp);
                //Delete 
                resp = await client.DeleteAsync($"api/v1/Competencies.json?id={objA.Id}");
                resp.EnsureSuccessStatusCode();
                Assert.AreEqual(HttpStatusCode.OK, resp.StatusCode);
                var result = await DeserializeResponseAsync<Competency>(resp);

                Assert.IsNotNull(result.UpdatedBy);
                Assert.IsFalse(result.IsEnabled);
            }
        }

        [TestMethod]
        [TestCategory("Integration")]
        public async Task DeleteCompetencyNotFoundTest()
        {
            using (var srv = new TestServer(TestHostBuilder<Startup, IntegrationTestStartup>()))
            {
                var client = srv.CreateClient();
                GenerateAuthHeader(client, await GenerateTokenAsync());
                //Delete
                var resp = await client.DeleteAsync($"api/v1/Competencies.json?id={Guid.NewGuid()}");
                Assert.AreEqual(HttpStatusCode.NotFound, resp.StatusCode);
                Assert.AreEqual($"\"{nameof(Competency)} Not Found\"", await resp.Content.ReadAsStringAsync());
            }
        }
    }
}
