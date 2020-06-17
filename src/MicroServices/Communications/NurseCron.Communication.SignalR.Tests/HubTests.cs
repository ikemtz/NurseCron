using System.Threading;
using System.Threading.Tasks;
using IkeMtz.NRSRx.Core.Unigration; 
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NurseCron.Communications.SignalR;

namespace NurseCron.Communication.SignalR.Tests
{
  [TestClass]
  public class HubTests : BaseUnigrationTests
  {
    [TestMethod]
    [TestCategory("Unigration")]
    public async Task NotificationHubTest()
    {
      using var srv = new TestServer(TestHostBuilder<Startup, UnigrationTestStartup>());

      var connection = srv.BuildSignalrConnection("notificationHub", GenerateTestToken());
      var message = "Hello World";
      var returnMessageFired = false;

      _ = connection.On<string>("OnMessageRecieved", msg =>
      {
        Assert.AreEqual($"IntegrationTester@email.com - {message}", msg);
        returnMessageFired = true;
      });

      await connection.StartAsync().ConfigureAwait(false);
      await connection.InvokeAsync("SendMessage", message).ConfigureAwait(true);
      for (int i = 0; i < 10; i++)
      {
        if (returnMessageFired)
        {
          i += 10;
        }
        else
        {
          Thread.Sleep(1000);
        }
      }
      Assert.IsTrue(returnMessageFired);
    }
  }
}
