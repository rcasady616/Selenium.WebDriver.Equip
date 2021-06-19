using NUnit.Framework;
using OpenQA.Selenium;
using Selenium.WebDriver.Equip.Elements;

namespace Selenium.WebDriver.Equip.Tests.Elements
{
    public class HtmlCheckBoxTests
    {
    }

    //[TestFixture]
    //[Category(TestCategories.HeadLess)]
    //[Category(TestCategories.Elements)]
    //public class HeadLessHtmlCheckBoxTests
    //{
    //    private Mockery mocks;
    //    private IWebElement iWebElement;
    //    private HtmlCheckBox checkBox;

    //    [SetUp]
    //    public void SetUp()
    //    {
    //        mocks = new Mockery();
    //        iWebElement = mocks.NewMock<IWebElement>();
    //        checkBox = new HtmlCheckBox(iWebElement);
    //    }

    //    [TestCase(true)]
    //    [TestCase(false)]
    //    public void CheckBoxSelected(bool selected)
    //    {
    //        Stub.On(iWebElement).GetProperty("Selected").Will(Return.Value(selected));
    //        Assert.AreEqual(selected, checkBox.Selected);
    //    }

    //    [Test]
    //    public void CheckBoxUnChecked()
    //    {
    //        Expect.Once.On(iWebElement).GetProperty("Selected").Will(Return.Value(true));
    //        Assume.That(true == checkBox.Selected);
    //        Expect.Once.On(iWebElement).GetProperty("Selected").Will(Return.Value(true));
    //        Expect.Once.On(iWebElement).Method("Click");
    //        checkBox.UnCheck();
    //        Expect.Once.On(iWebElement).GetProperty("Selected").Will(Return.Value(false));
    //        Assert.AreEqual(false, checkBox.Selected);
    //        mocks.VerifyAllExpectationsHaveBeenMet();
    //    }

    //    [Test]
    //    public void CheckBoxChecked()
    //    {
    //        Expect.Once.On(iWebElement).GetProperty("Selected").Will(Return.Value(false));
    //        Assume.That(false == checkBox.Selected);
    //        Expect.Once.On(iWebElement).GetProperty("Selected").Will(Return.Value(false));
    //        Expect.Once.On(iWebElement).Method("Click");
    //        checkBox.Check();
    //        Expect.Once.On(iWebElement).GetProperty("Selected").Will(Return.Value(true));
    //        Assert.AreEqual(true, checkBox.Selected);
    //        mocks.VerifyAllExpectationsHaveBeenMet();
    //    }
    //}
}
