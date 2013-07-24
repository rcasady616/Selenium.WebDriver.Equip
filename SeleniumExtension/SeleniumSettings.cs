using System.Configuration;

namespace SeleniumExtension
{
    sealed public class SeleniumSettings : ApplicationSettingsBase
    {
        [ApplicationScopedSettingAttribute()]
        [DefaultSettingValueAttribute("false")]
        public bool RemoteDriver
        {
            get { return (bool)this["RemoteDriver"]; }
            set { this["RemoteDriver"] = value; }
        }

        [ApplicationScopedSettingAttribute()]
        [DefaultSettingValueAttribute("firefox")]
        public string BrowserName
        {
            get { return (string)this["BrowserName"]; }
            set { this["BrowserName"] = value; }
        }

        [ApplicationScopedSettingAttribute()]
        public string BrowserVersion
        {
            get { return (string)this["BrowserVersion"]; }
            set { this["BrowserVersion"] = value; }
        }

        [ApplicationScopedSettingAttribute()]
        [DefaultSettingValueAttribute("http://127.0.0.1:4444/wd/hub")]
        public string SeleniumServerAddress
        {
            get { return (string)this["SeleniumServerAddress"]; }
            set { this["SeleniumServerAddress"] = value; }
        }

        [ApplicationScopedSettingAttribute()]
        [DefaultSettingValueAttribute("localhost")]
        public string SeleniumHost
        {
            get { return (string)this["SeleniumHost"]; }
            set { this["SeleniumHost"] = value; }
        }

        [ApplicationScopedSettingAttribute()]
        [DefaultSettingValueAttribute("4444")]
        public int SeleniumPort
        {
            get { return (int)this["SeleniumPort"]; }
            set { this["SeleniumPort"] = value; }
        }

        [ApplicationScopedSettingAttribute()]
        public string BrowserUrl
        {
            get { return (string)this["BrowserUrl"]; }
            set { this["BrowserUrl"] = value; }
        }

        [ApplicationScopedSettingAttribute()]
        [DefaultSettingValueAttribute("10")]
        public int ImplicitlyWaitSeconds
        {
            get { return (int)this["ImplicitlyWaitSeconds"]; }
            set { this["ImplicitlyWaitSeconds"] = value; }
        }

        [ApplicationScopedSettingAttribute()]
        [DefaultSettingValueAttribute("10")]
        public int ExplicitWaitSeconds
        {
            get { return (int)this["ExplicitWaitSeconds"]; }
            set { this["ExplicitWaitSeconds"] = value; }
        }

        [ApplicationScopedSettingAttribute()]
        [DefaultSettingValueAttribute("false")]
        public bool EnableNativeEvents
        {
            get { return (bool)this["EnableNativeEvents"]; }
            set { this["EnableNativeEvents"] = value; }
        }

        [ApplicationScopedSettingAttribute()]
        public string SeleniumServerStandAlonePath
        {
            get { return (string)this["SeleniumServerStandAlonePath"]; }
            set { this["SeleniumServerStandAlonePath"] = value; }
        }

        [ApplicationScopedSettingAttribute()]
        [DefaultSettingValueAttribute("true")]
        public bool IsJavaScriptEnabled
        {
            get { return (bool)this["IsJavaScriptEnabled"]; }
            set { this["IsJavaScriptEnabled"] = value; }
        }

        [ApplicationScopedSettingAttribute()]
        [DefaultSettingValueAttribute("30")]
        public int PageLoadTimeout
        {
            get { return (int)this["PageLoadTimeout"]; }
            set { this["PageLoadTimeout"] = value; }
        }
        
        [ApplicationScopedSettingAttribute()]
        [DefaultSettingValueAttribute("15")]
        public int ScriptTimeout
        {
            get { return (int)this["ScriptTimeout"]; }
            set { this["ScriptTimeout"] = value; }
        }
    }
}
