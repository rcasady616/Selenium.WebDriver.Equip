using NMock2;
using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumExtension.Elements;

namespace SeleniumExtension.Tests.Elements
{
    class CheckBoxTests
    {
    }

    [TestFixture]
    [Category(TestCategories.HeadLess)]
    public class HeadLessCheckBoxTests
    {
        private Mockery mocks;
        private IWebElement iWebElement;
        private CheckBox checkBox;

        [SetUp]
        public void SetUp()
        {
            mocks = new Mockery();
            iWebElement = mocks.NewMock<IWebElement>();
            checkBox = new CheckBox(iWebElement);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void CheckBoxSelected(bool selected)
        {
            Stub.On(iWebElement).GetProperty("Selected").Will(Return.Value(selected));
            Assert.AreEqual(selected, checkBox.Selected);
        }

        [Test]
        public void CheckBoxUnChecked()
        {
            Expect.Once.On(iWebElement).GetProperty("Selected").Will(Return.Value(true));
            Assume.That(true == checkBox.Selected);
            Expect.Once.On(iWebElement).GetProperty("Selected").Will(Return.Value(true));
            Expect.Once.On(iWebElement).Method("Click");
            checkBox.UnCheck();
            Expect.Once.On(iWebElement).GetProperty("Selected").Will(Return.Value(false));
            Assert.AreEqual(false, checkBox.Selected);
            mocks.VerifyAllExpectationsHaveBeenMet();
        }

        [Test]
        public void CheckBoxChecked()
        {
            Expect.Once.On(iWebElement).GetProperty("Selected").Will(Return.Value(false));
            Assume.That(false == checkBox.Selected);
            Expect.Once.On(iWebElement).GetProperty("Selected").Will(Return.Value(false));
            Expect.Once.On(iWebElement).Method("Click");
            checkBox.Check();
            Expect.Once.On(iWebElement).GetProperty("Selected").Will(Return.Value(true));
            Assert.AreEqual(true, checkBox.Selected);
            mocks.VerifyAllExpectationsHaveBeenMet();
        }
    }
}
