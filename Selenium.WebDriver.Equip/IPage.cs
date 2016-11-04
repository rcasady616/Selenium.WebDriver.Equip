namespace Selenium.WebDriver.Equip
{
    /// <summary>
    /// Defines an interface through which a web page is controlled 
    /// </summary>
    public interface IPage
    {
        /// <summary>
        /// Gets the value indicating whether or not this <see cref="IPage"/> has loaded
        /// </summary>
        /// <returns><see langword="true"/> if the <see cref="IPage"/> is loaded; otherwise, <see langword="false"/></returns>
        bool IsPageLoaded();
    }
}
