﻿using System.Threading;
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
            if (SeleniumServer.isSeleniumServerRunning())
                Assert.AreEqual(true, SeleniumServer.Stop());
        }

        [TearDown]
        public void TearDown()
        {
            if (SeleniumServer.isSeleniumServerRunning())
                Assert.AreEqual(true, SeleniumServer.Stop());
        }

        [Test]
        [Category("SeleniumServer")]
        public void TestStart()
        {
            SeleniumServer.Start();
            Assert.AreEqual(true, SeleniumServer.isSeleniumServerRunning());
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
        public void TestIsSeleniumServerRunning(bool running)
        {
            if (running)
                SeleniumServer.Start();
            Assert.AreEqual(running, SeleniumServer.isSeleniumServerRunning());
        }
    }
}