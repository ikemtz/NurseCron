using IkeMtz.NRSRx.Core.Unigration;
using IkeMtz.NRSRx.Employees.Models;
using IkeMtz.NRSRx.Employees.WebApi;
using IkeMtz.NRSRx.Employees.WebApi.Data;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace IkeMtz.NRSRx.Employees.Tests.Integration.Api
{
    [TestClass]
    public class EmployeesTests : BaseUnigrationTests
    {
        [TestMethod]
        [TestCategory("Integration")]
        public async Task GetEmployeeTest()
        {
            using (var srv = new TestServer(TestHostBuilder<Startup, IntegrationTestStartup>()))
            {
                var client = srv.CreateClient();
                GenerateAuthHeader(client, GenerateTestToken());
                //Get 
                var resp = await client.GetAsync($"api/v1/{nameof(Employees)}.json");

                Assert.AreEqual(HttpStatusCode.OK, resp.StatusCode);
                var result = await resp.Content.ReadAsStringAsync();

                var obj = JsonConvert.DeserializeObject<dynamic>(result);
                Assert.AreEqual("NRSRx Employee API Microservice Controller", obj.name.ToString());
            }
        }

        [TestMethod]
        [TestCategory("Integration")]
        public async Task SaveEmployeeTest()
        {
            var objA = new EmployeeInsertRequest
            {
                FirstName = Guid.NewGuid().ToString(),
                LastName = Guid.NewGuid().ToString(),
                Email = $"{Guid.NewGuid()}@email.com",
                Certifications = new EmployeeCertification[] {
                    new EmployeeCertification()
                    {
                        CertificationId = Guid.NewGuid(),
                        CertificationName  = Guid.NewGuid().ToString(),
                    },
                     new EmployeeCertification()
                    {
                        CertificationId = Guid.NewGuid(),
                        CertificationName  = Guid.NewGuid().ToString(),
                        ExpiresOnUtc = DateTime.UtcNow.AddDays(14)
                    },
                }
            };
            using (var srv = new TestServer(TestHostBuilder<Startup, IntegrationTestStartup>()))
            {
                var client = srv.CreateClient();
                GenerateAuthHeader(client, await GenerateTokenAsync());

                var resp = await client.PutAsJsonAsync($"api/v1/{nameof(Employees)}.json", objA);
                resp.EnsureSuccessStatusCode();
                var result = await DeserializeResponseAsync<Employee>(resp);
                Assert.AreEqual(objA.FirstName, result.FirstName);
                Assert.IsTrue(result.IsEnabled);
                Assert.IsNotNull(result.CreatedBy);

                var ctx = srv.GetDbContext<EmployeesContext>();
                var emp = await ctx.Employees.Include(t => t.Certifications).FirstOrDefaultAsync(t => t.FirstName == result.FirstName);

                Assert.IsNotNull(emp);
                Assert.AreEqual(result.CreatedOnUtc.ToString(), emp.CreatedOnUtc.ToString());
                Assert.AreEqual(2, emp.Certifications.Count);

            }
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
                Certifications = new EmployeeCertification[] {
                    new EmployeeCertification()
                    {
                        CertificationId = Guid.NewGuid(),
                        CertificationName  = Guid.NewGuid().ToString(),
                    }
                },
                IsEnabled = true,
            };
            using (var srv = new TestServer(TestHostBuilder<Startup, IntegrationTestStartup>()))
            {
                var client = srv.CreateClient();
                GenerateAuthHeader(client, await GenerateTokenAsync());

                var resp = await client.PutAsJsonAsync($"api/v1/{nameof(Employees)}.json?id={objA.Id}", new EmployeeInsertRequest(objA));
                resp.EnsureSuccessStatusCode();
                objA = await DeserializeResponseAsync<Employee>(resp);

                //Update
                objA.FirstName = Guid.NewGuid().ToString();
                objA.Certifications = new EmployeeCertification[] {
                    new EmployeeCertification()
                    {
                        CertificationId = Guid.NewGuid(),
                        CertificationName  = Guid.NewGuid().ToString(),
                    }
                };
                resp = await client.PostAsJsonAsync($"api/v1/{nameof(Employees)}.json?id={objA.Id}", new EmployeeUpdateRequest(objA));
                resp.EnsureSuccessStatusCode();
                var result = await DeserializeResponseAsync<Employee>(resp);

                Assert.IsNotNull(result.UpdatedBy);
                Assert.AreEqual(objA.FirstName, result.FirstName);

                var ctx = srv.GetDbContext<EmployeesContext>();
                var emp = await ctx.Employees.FirstOrDefaultAsync(t => t.FirstName == result.FirstName);

                Assert.IsNotNull(emp);
                Assert.AreEqual(result.UpdatedOnUtc.ToString(), emp.UpdatedOnUtc.ToString());
                Assert.IsNotNull(emp.UpdatedBy);
                Assert.AreEqual(1, result.Certifications.Count);
            }
        }

        [TestMethod]
        [TestCategory("Integration")]
        public async Task UpdateEmployeeNotFoundTest()
        {
            var objA = new EmployeeUpdateRequest
            {
                FirstName = Guid.NewGuid().ToString(),
                Id = Guid.NewGuid(),
                HireDate = DateTime.UtcNow.AddMonths(-23),
            };
            using (var srv = new TestServer(TestHostBuilder<Startup, IntegrationTestStartup>()))
            {
                var client = srv.CreateClient();
                GenerateAuthHeader(client, await GenerateTokenAsync());

                var resp = await client.PostAsJsonAsync($"api/v1/{nameof(Employees)}.json?id={objA.Id}", objA);
                Assert.AreEqual(HttpStatusCode.NotFound, resp.StatusCode);
                Assert.AreEqual($"\"{nameof(Employee)} Not Found\"", await resp.Content.ReadAsStringAsync());
            }
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
            using (var srv = new TestServer(TestHostBuilder<Startup, IntegrationTestStartup>()))
            {
                var client = srv.CreateClient();
                GenerateAuthHeader(client, await GenerateTokenAsync());

                var resp = await client.PutAsJsonAsync($"api/v1/{nameof(Employees)}.json?id={objA.Id}", new EmployeeInsertRequest(objA));
                resp.EnsureSuccessStatusCode();
                objA = await DeserializeResponseAsync<Employee>(resp);
                //Delete 
                resp = await client.DeleteAsync($"api/v1/{nameof(Employees)}.json?id={objA.Id}");
                resp.EnsureSuccessStatusCode();
                Assert.AreEqual(HttpStatusCode.OK, resp.StatusCode);
                var result = await DeserializeResponseAsync<Employee>(resp);

                Assert.IsNotNull(result.UpdatedBy);
                Assert.IsFalse(result.IsEnabled);
            }
        }

        [TestMethod]
        [TestCategory("Integration")]
        public async Task DeleteEmployeeNotFoundTest()
        {
            using (var srv = new TestServer(TestHostBuilder<Startup, IntegrationTestStartup>()))
            {
                var client = srv.CreateClient();
                GenerateAuthHeader(client, await GenerateTokenAsync());
                //Delete
                var resp = await client.DeleteAsync($"api/v1/{nameof(Employees)}.json?id={Guid.NewGuid()}");
                Assert.AreEqual(HttpStatusCode.NotFound, resp.StatusCode);
                Assert.AreEqual($"\"{nameof(Employee)} Not Found\"", await resp.Content.ReadAsStringAsync());
            }
        }
    }
}
