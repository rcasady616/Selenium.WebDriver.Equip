using NUnit.Framework;
using Selenium.WebDriver.Equip.Server;
using Selenium.WebDriver.Equip.Server.Json;
using Selenium.WebDriver.Equip.Settings;

namespace Selenium.WebDriver.Equip.Tests.Server
{
    [TestFixture]
    [Category(TestCategories.SeleniumServer)]
    public class SeleniumServerTests
    {
        public SeleniumServerProxy SeleniumServer;
        public SeleniumServerSettings Settings;

        [SetUp]
        public void Setup()
        {
            Settings = new SeleniumServerSettings { HostName = "localhost", Port = "4444", StandAlonePath = @"C:\Users\Rick\Documents\GitHub2\Selenium.WeDriver.Equip\selenium-server-standalone-3.141.59.jar" };
            SeleniumServer = new SeleniumServerProxy(Settings);
        }

        [TearDown]
        public void TearDown()
        {
            if (SeleniumServer.IsSeleniumServerRunning())
            {
                SeleniumServer.Stop();
                if (!SeleniumServer.WaitUntilStopped())
                    SeleniumServer.ServerProcess.Kill();
            }
        }

        #region single grid

        [Test]
        public void SingleGridStartStopWait()
        {
            SeleniumServer.Start();
            Assert.AreEqual(true, SeleniumServer.WaitUntilRunning());

            SeleniumServer.Stop();
            Assert.AreEqual(true, SeleniumServer.WaitUntilStopped());
        }

        #endregion

        #region node

        //[Test]
        //public void StartNodeByJsonFile()
        //{
        //    // hub
        //    var hubSettings = new SeleniumServerSettings { HostName = "localhost", Port = "5555", StandAlonePath = @"C:\Users\rcasady\Downloads\selenium-server-standalone-2.42.2.jar" };
        //    SeleniumServer.Start();



        //    // node


        //    //SeleniumServer = new ISeleniumServer(hubSettings);
        //    Assert.That(SeleniumServer.WaitUntilRunning());

        //    var node = new SeleniumServerProxy(Settings);
        //    JsonSerializer.Serialize(new NodeContract(), "DefaultConfiguration.json");
        //    string config = string.Format("-role node -hub http://localhost:5555/grid/register -nodeConfig \"{0}\"", "DefaultConfiguration.json");
        //    node.Start(config);
        //    Assert.That(node.WaitUntilRunning());

        //    // todo assert configuration

        //    node.Stop();
        //    Assert.That(node.WaitUntilStopped());
        //}

        #endregion

        #region Hub

        //[Test]
        //public void HubStartStopWait()
        //{
        //    SeleniumServer = new SeleniumServerHubProxy(Settings);
        //    SeleniumServer.Start();
        //    Assert.AreEqual(true, SeleniumServer.WaitUntilRunning());
        //    SeleniumServer.Stop();
        //    Assert.AreEqual(true, SeleniumServer.WaitUntilStopped());
        //}

        //[Test]
        //public void RegisterNodeToHub()
        //{
        //    var hubSettings = new SeleniumServerSettings { HostName = "localhost", Port = "5555", StandAlonePath = @"C:\Users\rcasady\Downloads\selenium-server-standalone-2.42.2.jar" };

        //    SeleniumServer = new SeleniumServerHubProxy(hubSettings);
        //    SeleniumServer.Start();
        //    Assert.That(SeleniumServer.WaitUntilRunning());

        //    var node = new SeleniumServerProxy(Settings);
        //    node.Start("-role node -hub http://localhost:5555/grid/register");
        //    Assert.That(node.WaitUntilRunning());

        //    // todo assert node is registered on hub

        //    node.Stop();
        //    Assert.That(node.WaitUntilStopped());
        //}

        #endregion
    }
}