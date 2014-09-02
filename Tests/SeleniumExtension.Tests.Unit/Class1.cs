using NMock2;
using NUnit.Framework;
using OpenQA.Selenium;

namespace SeleniumExtension.Tests.Unit
{
    [TestFixture]
    public class Class1
    {
        private Mockery mocks;
        private IWebDriver driver;

        [SetUp]
        public void SetUp()
        {
            mocks = new Mockery();
            driver = mocks.NewMock<IWebDriver>();
        }

        [Test]
        public void LearningToNMockForTheSecondTime()
        {
            Expect.Once.On(driver).Method("FindElement").With(By.Id("1")).Will(Throw.Exception(new StaleElementReferenceException()));
            Assert.Throws<StaleElementReferenceException>(() => driver.FindElement(By.Id("1")));
            mocks.VerifyAllExpectationsHaveBeenMet();
        }

        [ExpectedException(typeof(StaleElementReferenceException))]
        [Test]
        public void LearningToNMockForWithException()
        {
            Expect.Once.On(driver).Method("FindElement").With(By.Id("1")).Will(Throw.Exception(new StaleElementReferenceException()));
             driver.FindElement(By.Id("1"));
            mocks.VerifyAllExpectationsHaveBeenMet();
        }

        [Test]
        public void LearningToNMockInterfaceExtensionMethodWithActionParameter()
        {
            // iSearchContext.RetryActionOnStaleElement(() => iSearchContext.FindElement(By.ClassName("tableRow")).Click());

            Expect.Once.On(driver).Method("FindElement").With(By.Id("1")).Will(Throw.Exception(new StaleElementReferenceException()));
            //Expect.Once.On(driver).Method("RetryActionOnStaleElement").With(() => driver.RetryActionOnStaleElement(() => driver.FindElement(By.Id("1")).Click())).Will(Throw.Exception(new StaleElementReferenceException()));
            Assert.Throws<StaleElementReferenceException>(() => driver.FindElement(By.Id("1")));
            
            //Assert.DoesNotThrow(() => driver.RetryActionOnStaleElement(() => driver.FindElement(By.Id("1")).Click()));
            mocks.VerifyAllExpectationsHaveBeenMet();
        }
    }
}
