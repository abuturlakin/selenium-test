using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Selenium.Tests
{
    public static class WebDriverExtensions
    {
        private const int TimeoutInSeconds = 3;

        public static IWebElement FindBy(this IWebDriver driver, By by, int? timeoutInSeconds)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds ?? TimeoutInSeconds));
            return wait.Until(drv => drv.FindElement(by));
        }
        
        public static IWebElement FindByXPath(this IWebDriver driver, string xPath, int? timeoutInSeconds = null)
        {
            return driver.FindBy(By.XPath(xPath), timeoutInSeconds);
        }
        
        public static IWebElement FindById(this IWebDriver driver, string id, int? timeoutInSeconds = null)
        {
            return driver.FindBy(By.Id(id), timeoutInSeconds);
        }

        public static void Navigate(this IWebDriver driver, string url)
        {
            driver.Navigate().GoToUrl(url);
        }
    }
}