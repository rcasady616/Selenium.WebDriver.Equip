using System.ComponentModel;

namespace Selenium.WebDriver.Equip.WebDriver
{
    public class Browser
    {
        internal string Name { get; set; }
        internal string Path { get; set; }
        internal string IconPath { get; set; }
        internal string Version { get; set; }
    }

    /// <summary>
    /// Enumeration of all possiable browsers
    /// </summary>
    public enum DriverType
    {
        [Description("All")]
        All,
        [Description("HtmlUnit")]
        HtmlUnit,
        [Description("IE")]
        IE,
        [Description("Firefox")]
        Firefox,
        [Description("Safari")]
        Safari,
        [Description("Chrome")]
        Chrome,
        [Description("Opera")]
        Opera,
        [Description("Remote")]
        Remote,
        [Description("IPhone")]
        IPhone,
        [Description("Android")]
        Android,
        [Description("WindowsPhone")]
        WindowsPhone,
        [Description("PhantomJS")]
        PhantomJS,
        [Description("SauceLabs")]
        SauceLabs
    }

    public enum BrowserName
    {
        HtmlUnit,
        IE,
        Firefox,
        Safari,
        Chrome,
        Opera,
        IPhone,
        Android,
        WindowsPhone,
        PhantomJS,
    }
}