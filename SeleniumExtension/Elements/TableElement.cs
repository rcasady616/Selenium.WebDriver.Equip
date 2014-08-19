using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SeleniumExtension.Elements
{
    public class TableElement
    {
        public IWebElement WrappedElement { get; set; }
        public int RowLabelIndex { get; set; }
        public int ColumnLabelIndex { get; set; }
        public bool HeaderRow { get { return WrappedElement.ElementExists(By.CssSelector("th")); } }

        public TableElement(IWebElement iWebElement, int columnLabelIndex, int rowLabelIndex)
        {
            InitTable(iWebElement, columnLabelIndex, rowLabelIndex);
        }

        public TableElement(IWebElement iWebElement)
        {
            InitTable(iWebElement, 1, 1);
        }

        private void InitTable(IWebElement iWebElement, int columnLabelIndex, int rowLabelIndex)
        {
            if (iWebElement == null)
                throw new ArgumentNullException("iWebElement", "iWebElement cannot be null");
            if (string.IsNullOrEmpty(iWebElement.TagName) ||
                string.Compare(iWebElement.TagName, "table", StringComparison.OrdinalIgnoreCase) != 0)
                throw new UnexpectedTagNameException("table", iWebElement.TagName);
            RowLabelIndex = columnLabelIndex;
            ColumnLabelIndex = rowLabelIndex;
            WrappedElement = iWebElement;
        }

        public List<IWebElement> GetHeadings()
        {
            return WrappedElement.FindElements(By.XPath(".//th")).ToList();
        }

        public List<List<IWebElement>> GetRows()
        {
            var rows = new List<List<IWebElement>>();
            var rowElements = WrappedElement.FindElements(By.XPath(".//tr")).ToList();
            foreach (IWebElement rowElement in rowElements)
            {
                rows.Add(rowElement.FindElements(By.XPath(".//td")).ToList());
            }
            return rows;
        }

        public List<IWebElement> GetColumn(int columnNumber)
        {
            return WrappedElement.FindElements(By.XPath(string.Format(".//td[{0}]", columnNumber))).ToList();
        }

        public List<IWebElement> GetRow(int rowNumber)
        {
            if (rowNumber == 1)
                if (WrappedElement.ElementExists(By.CssSelector("th")))
                    return WrappedElement.FindElement(By.CssSelector(string.Format("tr:nth-child({0})", rowNumber))).FindElements(By.CssSelector("th")).ToList();
            return WrappedElement.FindElement(By.CssSelector(string.Format("tr:nth-child({0})", rowNumber))).FindElements(By.CssSelector("td")).ToList();
        }

        public int GetRowNumber(string rowLabel)
        {
            int addToIndex = 1;
            if (WrappedElement.ElementExists(By.CssSelector("th")))
            {
                if (RowLabelIndex == 1)
                    return 1;
                addToIndex++;
            }
            var rows = GetColumn(RowLabelIndex);
            for (int i = 0; i < rows.Count; i++)
                if (rows[i].Text == rowLabel)
                    return i + addToIndex;
            throw new NotFoundException(string.Format("Row label not found, label name: {0}", rowLabel));
        }

        public int GetColumnNumber(string columnLabel)
        {
            int addToIndex = 1;
            var cols = GetRow(ColumnLabelIndex);
            for (int i = 0; i <= cols.Count; i++)
                if (cols[i].Text == columnLabel)
                    return i + addToIndex;
            throw new NotFoundException(string.Format("Column label not found, label name: {0}", columnLabel));
        }

        public IWebElement GetCell(int columnNumber, int rowNumber)
        {
            if (rowNumber == 1)
                if (WrappedElement.ElementExists(By.CssSelector("th")))
                    return WrappedElement.FindElement(By.CssSelector(string.Format("tr:nth-child({0}) th:nth-child({1})", rowNumber, columnNumber)));

            return WrappedElement.FindElement(By.CssSelector(string.Format("tr:nth-child({0}) td:nth-child({1})", rowNumber, columnNumber)));
        }

        public IWebElement GetCell(int columnNumber, string rowLabel)
        {
            return GetCell(columnNumber, GetRowNumber(rowLabel));
        }

        public IWebElement GetCell(string columnLabel, string rowLabel)
        {
            return GetCell(GetColumnNumber(columnLabel), GetRowNumber(rowLabel));
        }

        public IWebElement GetCell(string columnLabel, int rowNumber)
        {
            return GetCell(GetColumnNumber(columnLabel), rowNumber);
        }
    }
}
