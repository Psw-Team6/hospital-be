using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
namespace HospitalTest.HospitalizationTest.Pages
{
    public class DashboardPage
    {
        private readonly IWebDriver driver;
        public const string URI = "http://localhost:4200/dashboard";

        IWebElement hospitalizationNav => driver.FindElement(By.XPath("/html/body/app-root/app-sidenav/div/ul/li[6]/a"));
        IWebElement consiliumNav => driver.FindElement(By.XPath("/html/body/app-root/app-sidenav/div/ul/li[3]/a"));
        public DashboardPage(IWebDriver driver)
        {
            this.driver = driver;
        }
        public bool hospitalizationNavDisplayed()
        {
            return hospitalizationNav.Displayed;
        }
        public bool consiliumNavDisplayed()
        {
            return consiliumNav.Displayed;
        }
        public void hospitalizationNavClick()
        {
            hospitalizationNav.Click();
        }
        public void consiliumNavClick()
        {
            consiliumNav.Click();
        }
        public void WaitForFormSubmit()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 20));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.UrlToBe("http://localhost:4200/patients/hospitalization"));
        }
        
        public void WaitForConsiliumNavigate()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 20));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.UrlToBe("http://localhost:4200/consiliums"));
        }
        
    }
}