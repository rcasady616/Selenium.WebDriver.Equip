using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Selenium.WebDriver.Equip.Extensions
{
    public static class SelectElementExtension
    {
        public static void SelectByTextClick(this SelectElement selectElement, string text)
        {
            foreach (IWebElement item in selectElement.Options)
            {
                if (item.Text == text)
                    item.Click();
            }
        }

        public static List<string> OptionsText(this SelectElement selectElement)
        {
            return selectElement.Options.Select(item => item.Text).ToList();
        }

        public static List<string> OptionsValue(this SelectElement selectElement)
        {
            return selectElement.Options.Select(item => item.Value()).ToList();
        }

        public static bool IsOptionPresent(this SelectElement selectElement, string optionValue)
        {
            bool found = false;
            IEnumerator allOptions = selectElement.Options.GetEnumerator();

            while (allOptions.MoveNext())
            {
                var currentOption = (IWebElement)allOptions.Current;
                if (String.Equals(currentOption.Text, optionValue))
                {
                    found = true;
                    break;
                }
            }
            return found;
        }

    }
}
