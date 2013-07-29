using System.Threading;
using NUnit.Framework;
using SeleniumExtension.Server;

namespace SeleniumExtension.Tests
{
    [TestFixture]
    public class SeleniumServerTests
    {
        [SetUp]
        public void Setup()
        {
            if (SeleniumServer.IsSeleniumServerRunning())
            {
                Assert.AreEqual(true, SeleniumServer.Stop());
                Thread.Sleep(10000);
            }
        }

        [TearDown]
        public void TearDown()
        {
            if (SeleniumServer.IsSeleniumServerRunning())
                Assert.AreEqual(true, SeleniumServer.Stop());
        }

        [Test]
        [Category("SeleniumServer")]
        public void TestStart()
        {
            SeleniumServer.Start();
            Assert.AreEqual(true, SeleniumServer.IsSeleniumServerRunning());
        }

        [Test]
        [Category("SeleniumServer")]
        public void TestStop()
        {
            SeleniumServer.Start();
            Thread.Sleep(10000);
            Assert.AreEqual(true, SeleniumServer.WaitUntilSeleniumServerRunning());
            Assert.AreEqual(true, SeleniumServer.Stop());
        }

        [TestCase(true)]
        [TestCase(false)]
        [Category("SeleniumServer")]
        public void TestWaitUntilSeleniumServerRunning(bool running)
        {
            if (running)
                SeleniumServer.Start();
            Assert.AreEqual(running, SeleniumServer.WaitUntilSeleniumServerRunning());
        }

        [TestCase(true)]
        [TestCase(false)]
        [Category("SeleniumServer")]
        public void TestIsSeleniumServerRunning(bool running)
        {
            if (running)
                SeleniumServer.Start();
            Assert.AreEqual(running, SeleniumServer.IsSeleniumServerRunning());
        }
    }
}
