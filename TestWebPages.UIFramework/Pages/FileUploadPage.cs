using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using Selenium.WebDriver.Equip;

namespace TestWebPages.UIFramework.Pages
{
    public class FileUploadPage : BasePage, IPage
    {
        public static string Url = "/TestWebPages/FileUpload.html";

        #region Static By Selectors

        public static By ByFile = By.Id("fname");
        public static By ByDisplayFilePath = By.Id("display");

        #endregion

        #region properties

        #endregion

        #region constructors

        public FileUploadPage(IWebDriver driver)
            : base(driver)
        {

        }

        public FileUploadPage()
        {
        }

        #endregion

        #region public methods

        public void UploadFile(string filePath)
        {
            Driver.FindElement(ByFile).SendKeys(filePath);
            Driver.FindElement(ByDisplayFilePath).Click();
        }

        public bool IsPageLoaded()
        {
            return Driver.WaitUntilVisible(new List<By>() { ByFile, ByDisplayFilePath });
        }

        #endregion

        #region private methods

        #endregion
    }
}
