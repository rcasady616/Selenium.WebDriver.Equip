using NUnit.Framework;
using SeleniumExtension.Server;

namespace SeleniumExtension.Tests.Server
{
    [TestFixture]
    public class SeleniumServerManagerTests
    {
        [Test]
        [Category("SeleniumServer")]
        public void TestStart()
        {
            using (var seleniumServerManager = new SeleniumServerManager("localhost",4444,"firefox","http://cnn.com"))
            {
                seleniumServerManager.Start();
                Assert.AreEqual(true, SeleniumServer.IsSeleniumServerRunning());
                seleniumServerManager.Stop();
                Assert.AreEqual(true, SeleniumServer.IsSeleniumServerRunning());
            }
        }
    }
}