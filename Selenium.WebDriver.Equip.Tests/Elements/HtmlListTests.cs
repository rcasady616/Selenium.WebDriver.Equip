using NMock2;
using NUnit.Framework;
using OpenQA.Selenium;
using Selenium.WebDriver.Equip.Elements;

namespace Selenium.WebDriver.Equip.Tests.Elements
{
    public class HtmlListTests
    {
    }

    [Parallelizable]
    [TestFixture]
    [Category(TestCategories.HeadLess)]
    [Category(TestCategories.Elements)]
    public class HeadLessHtmlListTests
    {
        private Mockery mocks;
        private IWebElement iWebElement;
        private HtmlList listBox;

        [SetUp]
        public void SetUp()
        {
            mocks = new Mockery();
            iWebElement = mocks.NewMock<IWebElement>();
            listBox = new HtmlList(iWebElement);
        }

        [Test]
        public void CheckBoxUnChecked()
        {
            var itemElement = mocks.NewMock<IWebElement>();
            Expect.Once.On(iWebElement).Method("FindElement").Will(Return.Value(itemElement));
            Assume.That(true == listBox.ListItemPresent("item1"));

            mocks.VerifyAllExpectationsHaveBeenMet();
        }
    }
}
