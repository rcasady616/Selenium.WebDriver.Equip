using NMock2;
using NUnit.Framework;
using OpenQA.Selenium;
using Selenium.WebDriver.Equip.Elements;
using TestWebPages.UIFramework.Pages;

namespace Selenium.WebDriver.Equip.Tests.Elements
{

    //[TestFixture]//(typeof(int), typeof(int), 1, 1)]
    //[NUnit.Framework.Category("Elements")]
    //public class TableTests2<T1, T2> : BaseTest
    //{
    //    public TablesPage Page;

    //    [SetUp]
    //    public void SetupTest()
    //    {
    //        Driver.Navigate().GoToUrl(TablesPage.Url);
    //        Page = new TablesPage(Driver);
    //        Assert.AreEqual(true, Page.IsPageLoaded());
    //    }

    //    //[TestCase(typeof (int), typeof (int), 1, 1, "ID")] //, Category = "Unit", Description = "Index")]
    //    [TestCase(1, 1, ExpectedResult = "ID")]
    //    public void GetCellBy(object column, object row, string cell)
    //    {
    //        //var col = (T1)(object)column.ToString();
    //        //var ro = (T2)(object)row.ToString();
    //        //var col = (T1)column>column;
    //        // var ro = (T2)(object)row.ToString();
    //        Assume.That(Driver.ElementExists(TablesPage.ByTableOne));
    //        var table = new TableElement(Driver.FindElement(TablesPage.ByTableOne));
    //        // Assert.AreEqual(cell, table.GetCell(column as typeof(T1) , <typeof(T2)>row).Text);
    //        // return table.GetCell(col , ro).Text;
    //        var col = Convert<T1>(column);
    //        var ro = Convert<T2>(row);
    //        return table.GetCell(column, row).Text;
    //    }

    //    //public T Convert<T>(object v)
    //    //{
    //    //    return (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFrom(v);
    //    //}
    //    public T Convert<T>(object v)
    //    {
    //        if (typeof(T) == typeof(string))
    //        {
    //            return (T)(object)v;
    //        }
    //        else
    //        {
    //            return (T)v;
    //        }

    //    }

    //    //public T GetType<T>(object v)
    //    //{
    //    //    return v.GetType();

    //    //}
    //}

    [TestFixture]
    [Category(TestCategories.Elements)]
    public class TableTests : BaseTest
    {
        public TablesPage Page;

        [SetUp]
        public void SetupTableTests()
        {
            Driver.Navigate().GoToUrl(TablesPage.Url);
            Page = new TablesPage(Driver);
            Assert.AreEqual(true, Page.IsPageLoaded());
        }

        //[TestCase(typeof(int), typeof(int), 1, 1, "ID")]//, Category = "Unit", Description = "Index")]
        //public void GetCellBy<T1, T2>(T1 column, T2 row, string cell)
        //{

        //    Assume.That(Driver.ElementExists(TablesPage.ByTableOne));
        //    var table = new TableElement(Driver.FindElement(TablesPage.ByTableOne));
        //    Assert.AreEqual(cell, table.GetCell(column, row).Text);
        //}


        #region table with header

        [TestCase(1, 1, "ID")]
        [TestCase(1, 6, "5")]
        [TestCase(2, 1, "Subject ID")]
        [TestCase(2, 6, "1007")]
        public void GetCellByIndexWithHeaderRow(int column, int row, string cell)
        {
            Assume.That(Driver.ElementExists(TablesPage.ByTableOne));
            var table = new Table(Driver.FindElement(TablesPage.ByTableOne));
            Assert.AreEqual(cell, table.GetCell(column, row).Text);
        }
       
        #endregion

        #region table without header row

        [TestCase(1, 1, "ID")]
        [TestCase(1, 6, "5")]
        [TestCase(2, 1, "Subject ID")]
        [TestCase(2, 6, "1007")]
        public void GetCellByIndexWithOutHeaderRow(int column, int row, string cell)
        {
            Assume.That(Driver.ElementExists(TablesPage.ByTableTwo));
            var table = new Table(Driver.FindElement(TablesPage.ByTableTwo));
            Assert.AreEqual(cell, table.GetCell(column, row).Text);
        }
        #endregion
    }

    [TestFixture]
    [Category(TestCategories.HeadLess)]
    [Category(TestCategories.Elements)]
    public class HeadLessTests
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
        [Ignore("work in progress")]
        public void TestClassNameExtention()
        {
            string className = "r2d2";
            Stub.On(driver).Method("FindElement").With("ii").Will(Return.Value(""));
            StringAssert.AreEqualIgnoringCase(className, driver.FindElement(By.Id("")).ClassName());

            //Assume.That(Driver.ElementExists(TablesPage.ByTableOne));
            //var table = new Table(Driver.FindElement(TablesPage.ByTableOne));
            //Assert.AreEqual(cell, table.GetCell(column, row).Text);

        }

    }
}
