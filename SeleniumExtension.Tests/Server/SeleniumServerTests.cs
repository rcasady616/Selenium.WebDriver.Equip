using System.Threading;
using NUnit.Framework;
using SeleniumExtension.Server;
using SeleniumExtension.Settings;
using WebDriverProxy.Proxies;

namespace SeleniumExtension.Tests
{
    [TestFixture]
    [Category("SeleniumServer")]
    public class SeleniumServerTests
    {
        public ISeleniumServer SeleniumServer;
        public SeleniumServerSettings settings;

        [SetUp]
        public void Setup()
        {
            settings = new SeleniumServerSettings { HostName = "localhost", Port = "4444", StandAlonePath = @"C:\Users\rcasady\Downloads\selenium-server-standalone-2.42.2.jar" };
        }

        [TearDown]
        public void TearDown()
        {
            if (SeleniumServer.IsSeleniumServerRunning())
            {
                SeleniumServer.Stop();
                Assert.AreEqual(true, SeleniumServer.WaitUntilStopped());
            }
        }

        #region single grid

        [Test]
        public void SingleGridStartStopWait()
        {
            SeleniumServer = new SeleniumServerProxy(settings);
            SeleniumServer.Start();
            Assert.AreEqual(true, SeleniumServer.WaitUntilRunning());

            SeleniumServer.Stop();
            Assert.AreEqual(true, SeleniumServer.WaitUntilStopped());
        }

        #endregion

        #region Hub

        [Test]
        public void HubStartStopWait()
        {
            SeleniumServer = new SeleniumServerHubProxy(settings);
            SeleniumServer.Start();
            Assert.AreEqual(true, SeleniumServer.WaitUntilRunning());
            SeleniumServer.Stop();
            Assert.AreEqual(true, SeleniumServer.WaitUntilStopped());
        }

        [Test]
        public void RegisterNodeToHub()
        {
            var hubSettings = new SeleniumServerSettings { HostName = "localhost", Port = "5555", StandAlonePath = @"C:\Users\rcasady\Downloads\selenium-server-standalone-2.42.2.jar" };
            
            SeleniumServer = new SeleniumServerHubProxy(hubSettings);
            SeleniumServer.Start();
            Assert.That(SeleniumServer.WaitUntilRunning());

            var node = new SeleniumServerProxy(settings);
            node.Start("-role node -hub http://localhost:5555/grid/register");
            Assert.That(node.WaitUntilRunning());

            //node.

            node.Stop();
            Assert.That(node.WaitUntilStopped());
        }

        #endregion
    }
}
