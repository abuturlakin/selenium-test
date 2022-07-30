using System;

using NUnit.Framework;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Selenium.Tests
{
    public class GooglePageObjectModel : IDisposable
    {
        #region Infrastructure
        private readonly IWebDriver _driver;

        public GooglePageObjectModel(IWebDriver driver)
        {
            _driver = driver;
        }
        
        public void Navigate(string url)
        {
            _driver.Navigate(url);
        }          
        
        public void Dispose()
        {
            _driver.Dispose();
        }
        #endregion

        private IWebElement SearchBox => _driver.FindByXPath("//input[@title='Search']");

        private IWebElement Results => _driver.FindById("result-stats");

        public void FillSearch(string value)
        {
            SearchBox.SendKeys(value);
        }

        public void ProcessSearch()
        {
            SearchBox.SendKeys(Environment.NewLine);
        }

        public string GetResultsText()
        {
            return Results.Text;
        }
    }

    public class GoogleApplicationFactory
    {
        private readonly IWebDriver _driver;

        public static Lazy<GoogleApplicationFactory> Instance => new Lazy<GoogleApplicationFactory>(() => new GoogleApplicationFactory());

        private GoogleApplicationFactory()
        {
            _driver = new ChromeDriver();
        }
        
        public GooglePageObjectModel GetGooglePageObjectModel()
        {
            return new GooglePageObjectModel(_driver);
        }
    }

    [TestFixture]
    public class GooglePageTestSeleniumWithPom
    {
        #region Infrastructure
        private GooglePageObjectModel _pageObjectModel;

        [SetUp]
        public void Setup()
        {
            _pageObjectModel = GoogleApplicationFactory
                .Instance
                .Value
                .GetGooglePageObjectModel();
        }

        [TearDown]
        public void TearDown()
        {
            _pageObjectModel.Dispose();
        }  
        #endregion        

        [Test]
        public void Test()
        {
            _pageObjectModel.Navigate("http://www.google.com");
            _pageObjectModel.FillSearch("nikon d750");
            _pageObjectModel.ProcessSearch();

            var results = _pageObjectModel.GetResultsText();
            Assert.IsNotNull(results);
            Assert.IsTrue(results.Contains("About"));
        }
    }
}