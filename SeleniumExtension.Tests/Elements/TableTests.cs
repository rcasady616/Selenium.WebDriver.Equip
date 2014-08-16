using System;
using System.ComponentModel;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using OpenQA.Selenium;
using SeleniumExtension.Nunit;
using TestWebPages.UIFramework.Pages;

namespace SeleniumExtension.Tests.Elements
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
    public class TableTests : BaseTest
    {
        public TablesPage Page;

        [SetUp]
        public void SetupTest()
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
        [TestCase(1, 2, "1")]
        [TestCase(1, 3, "2")]
        [TestCase(1, 4, "3")]
        [TestCase(1, 5, "4")]
        [TestCase(1, 6, "5")]
        [TestCase(2, 1, "Subject ID")]
        [TestCase(2, 2, "1001")]
        [TestCase(2, 3, "1003")]
        [TestCase(2, 4, "1002")]
        [TestCase(2, 5, "1008")]
        [TestCase(2, 6, "1007")]
        public void GetCellByIndexWithHeaderRow(int column, int row, string cell)
        {
            Assume.That(Driver.ElementExists(TablesPage.ByTableOne));
            var table = new TableElement(Driver.FindElement(TablesPage.ByTableOne));
            Assert.AreEqual(cell, table.GetCell(column, row).Text);
        }

        [TestCase(1, "ID", "ID", 1)]
        [TestCase(1, "1", "1", 1)]
        [TestCase(1, "2", "2", 1)]
        [TestCase(1, "3", "3", 1)]
        [TestCase(1, "4", "4", 1)]
        [TestCase(1, "5", "5", 1)]
        [TestCase(2, "ID", "Subject ID", 2)]
        [TestCase(2, "1", "1001", 2)]
        [TestCase(2, "2", "1003", 2)]
        [TestCase(2, "3", "1002", 2)]
        [TestCase(2, "4", "1008", 2)]
        [TestCase(2, "5", "1007", 2)]
        public void GetCellColumnIndexRowlabelWithHeaderRow(int column, string row, string cell, int rowIndex)
        {
            Assume.That(Driver.ElementExists(TablesPage.ByTableOne));
            var table = new TableElement(Driver.FindElement(TablesPage.ByTableOne), 1, rowIndex);
            Assert.AreEqual(cell, table.GetCell(column, row).Text);
        }

        [TestCase("ID", 1, "ID")]
        [TestCase("ID", 2, "1")]
        [TestCase("ID", 3, "2")]
        [TestCase("ID", 4, "3")]
        [TestCase("ID", 5, "4")]
        [TestCase("ID", 6, "5")]
        [TestCase("Subject ID", 1, "Subject ID")]
        [TestCase("Subject ID", 2, "1001")]
        [TestCase("Subject ID", 3, "1003")]
        [TestCase("Subject ID", 4, "1002")]
        [TestCase("Subject ID", 5, "1008")]
        [TestCase("Subject ID", 6, "1007")]
        public void GetCellColumnLabelRowIndexWithheaderRow(string column, int row, string cell, int columnIndex = 1)
        {
            Assume.That(Driver.ElementExists(TablesPage.ByTableOne));
            var table = new TableElement(Driver.FindElement(TablesPage.ByTableOne));
            Assert.AreEqual(cell, table.GetCell(column, row).Text);
        }

        [TestCase("ID", "ID", "ID")]
        [TestCase("ID", "1", "1")]
        [TestCase("ID", "2", "2")]
        [TestCase("ID", "3", "3")]
        [TestCase("ID", "4", "4")]
        [TestCase("ID", "5", "5")]
        [TestCase("Subject ID", "Subject ID", "Subject ID")]
        [TestCase("Subject ID", "1001", "1001")]
        [TestCase("Subject ID", "1003", "1003")]
        [TestCase("Subject ID", "1002", "1002")]
        [TestCase("Subject ID", "1008", "1008")]
        [TestCase("Subject ID", "1007", "1007")]
        public void GetCellColumnLabelRowLabelWithHeaderRow(string column, string row, string cell)
        {
            Assume.That(Driver.ElementExists(TablesPage.ByTableOne));
            var table = new TableElement(Driver.FindElement(TablesPage.ByTableOne));
            Assert.AreEqual(cell, table.GetCell(column, row).Text);
        }

        #endregion

        #region table without header row

        [TestCase(1, 1, "ID")]
        [TestCase(1, 2, "1")]
        [TestCase(1, 3, "2")]
        [TestCase(1, 4, "3")]
        [TestCase(1, 5, "4")]
        [TestCase(1, 6, "5")]
        [TestCase(2, 1, "Subject ID")]
        [TestCase(2, 2, "1001")]
        [TestCase(2, 3, "1003")]
        [TestCase(2, 4, "1002")]
        [TestCase(2, 5, "1008")]
        [TestCase(2, 6, "1007")]
        public void GetCellByIndexWithOutHeaderRow(int column, int row, string cell)
        {
            Assume.That(Driver.ElementExists(TablesPage.ByTableTwo));
            var table = new TableElement(Driver.FindElement(TablesPage.ByTableTwo));
            Assert.AreEqual(cell, table.GetCell(column, row).Text);
        }

        [TestCase(1, "ID", "ID")]
        [TestCase(1, "1", "1")]
        [TestCase(1, "2", "2")]
        [TestCase(1, "3", "3")]
        [TestCase(1, "4", "4")]
        [TestCase(1, "5", "5")]
        [TestCase(2, "ID", "Subject ID")]
        [TestCase(2, "1", "1001")]
        [TestCase(2, "2", "1003")]
        [TestCase(2, "3", "1002")]
        [TestCase(2, "4", "1008")]
        [TestCase(2, "5", "1007")]
        public void GetCellColumnIndexRowlabelWithOutheaderRow(int column, string row, string cell)
        {
            Assume.That(Driver.ElementExists(TablesPage.ByTableTwo));
            var table = new TableElement(Driver.FindElement(TablesPage.ByTableTwo));
            Assert.AreEqual(cell, table.GetCell(column, row).Text);
        }

        [TestCase("ID", 1, "ID")]
        [TestCase("ID", 2, "1")]
        [TestCase("ID", 3, "2")]
        [TestCase("ID", 4, "3")]
        [TestCase("ID", 5, "4")]
        [TestCase("ID", 6, "5")]
        [TestCase("Subject ID", 1, "Subject ID")]
        [TestCase("Subject ID", 2, "1001")]
        [TestCase("Subject ID", 3, "1003")]
        [TestCase("Subject ID", 4, "1002")]
        [TestCase("Subject ID", 5, "1008")]
        [TestCase("Subject ID", 6, "1007")]
        public void GetCellColumnLabelRowIndexWithOutheaderRow(string column, int row, string cell)
        {
            Assume.That(Driver.ElementExists(TablesPage.ByTableTwo));
            var table = new TableElement(Driver.FindElement(TablesPage.ByTableTwo));
            Assert.AreEqual(cell, table.GetCell(column, row).Text);
        }

        [TestCase("ID", "ID", "ID")]
        [TestCase("ID", "1", "1")]
        [TestCase("ID", "2", "2")]
        [TestCase("ID", "3", "3")]
        [TestCase("ID", "4", "4")]
        [TestCase("ID", "5", "5")]
        [TestCase("Subject ID", "Subject ID", "Subject ID")]
        [TestCase("Subject ID", "1001", "1001")]
        [TestCase("Subject ID", "1003", "1003")]
        [TestCase("Subject ID", "1002", "1002")]
        [TestCase("Subject ID", "1008", "1008")]
        [TestCase("Subject ID", "1007", "1007")]
        public void GetCellColumnLabelRowLabelWithOutHeaderRow(string column, string row, string cell)
        {
            Assume.That(Driver.ElementExists(TablesPage.ByTableTwo));
            var table = new TableElement(Driver.FindElement(TablesPage.ByTableTwo));
            Assert.AreEqual(cell, table.GetCell(column, row).Text);
        }
        #endregion
    }
}
