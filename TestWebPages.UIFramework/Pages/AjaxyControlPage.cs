using System;
using OpenQA.Selenium;
using SeleniumExtention;

namespace TestWebPages.UIFramework.Pages
{
    public class AjaxyControlPage : BasePage , IPage
    {
        #region Static By Selectors

        #endregion

        #region properties

        #endregion

        #region constructors

        public AjaxyControlPage(IWebDriver driver)
            : base(driver)
        {

        }

        public AjaxyControlPage()
        {
        }

        #endregion

        #region public methods

        public bool IsPageLoaded()
        {
            throw new NotImplementedException("");
        }

        #endregion

        #region private methods

        #endregion
    }
}
