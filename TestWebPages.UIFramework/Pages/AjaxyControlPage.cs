using System.Collections.ObjectModel;
using OpenQA.Selenium;
using SeleniumExtention;

namespace TestWebPages.UIFramework.Pages
{
    public class AjaxyControlPage : BasePage , IPage
    {
        public static string Url = "/TestWebPages/AjaxyControl.html";

        #region Static By Selectors

        public static By ByNewLableText = By.Name("typer");
        public static By ByRedRadio = By.Id("red");
        public static By ByGreenRadio = By.Id("green");
        public static By BySubmitButton = By.Name("submit");
        public static By ByLabelsDiv = By.ClassName("label");

        #endregion

        #region IWebElement properties

        public IWebElement NewLabelText
        { get { return Driver.FindElement(ByNewLableText); } }

        public IWebElement RedRadio
        { get { return Driver.FindElement(ByRedRadio); } }

        public IWebElement GreenRadio
        { get { return Driver.FindElement(ByGreenRadio); } }

        public IWebElement SubmitButton
        { get { return Driver.FindElement(BySubmitButton); } }

        public ReadOnlyCollection<IWebElement> Labels
        { get { return Driver.FindElements(ByLabelsDiv); } }

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

        public void AddGreenLabel(string label)
        {
            GreenRadio.Click();
            NewLabelText.SendKeys(label);
            SubmitButton.Click();
        }

        public void AddRedLabel(string label)
        {
            RedRadio.Click();
            NewLabelText.SendKeys(label);
            SubmitButton.Click();
        }

        public bool IsPageLoaded()
        {
            return Driver.WaitUntilExists(BySubmitButton);
        }

        #endregion

        #region private methods

        #endregion
    }
}
