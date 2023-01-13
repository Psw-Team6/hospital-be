using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
namespace HospitalTest.End2EndPages
{
    public class SearchExaminationPage
    {
        private readonly IWebDriver _driver;
        public const string Uri = "http://localhost:4200/examination-search";
        public const string UriDashboard = "http://localhost:4200/dashboard";
        
        private IWebElement SearchInput => _driver.FindElement(By.Id("searchInput"));
        private IWebElement SearchButton => _driver.FindElement(By.Id("searchButton"));

        
        public SearchExaminationPage(IWebDriver driver)
        {
            _driver = driver;
        }
        public void Navigate() => _driver.Navigate().GoToUrl(Uri);
        
        public void Search()
        {
            SearchButton.Click();
        }
        public void EnterQuery(string query)
        {
            SearchInput.SendKeys(query);
        }
        public void WaitForFormSubmit()
        {
            var wait = new WebDriverWait(_driver, new TimeSpan(0, 0, 10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.UrlToBe(UriDashboard));
        }
    }
}