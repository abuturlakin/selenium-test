using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Selenium.Tests
{
    [TestFixture]
    public class GooglePageTestPureSelenium
    {
        private IWebDriver _driver;

        [SetUp]
        public void Setup()
        {
            _driver = new ChromeDriver();
        }

        [Test]
        public void Test()
        {
            _driver.Navigate().GoToUrl("http://www.google.com");
            var textInput = _driver.FindElement(By.XPath("//input[@title='Search']"));
            textInput.SendKeys("nikon d750");
            textInput.SendKeys(Environment.NewLine);

            var results = _driver.FindElement(By.Id("result-stats"));
            Assert.IsNotNull(results);
            Assert.IsTrue(results.Text.Contains("About"));
        }

        [TearDown]
        public void TearDown()
        {
            _driver.Dispose();
        }
    }
}