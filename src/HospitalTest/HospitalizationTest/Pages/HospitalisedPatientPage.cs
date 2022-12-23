using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
namespace HospitalTest.HospitalizationTest.Pages
{
    public class HospitalisedPatientPage
    {
        private readonly IWebDriver driver;
        public const string URI = "http://localhost:4200/hospitalizes-patients";
        IWebElement hospitalizatiedTitle => driver.FindElement(By.XPath("/html/body/app-root/app-body/div/app-hospitalized-patients/div/h1"));
            
        public HospitalisedPatientPage(IWebDriver driver)
        {
            this.driver = driver;
        }
        
        public bool hospitalizatedTitleNavDisplayed()
        {
            return hospitalizatiedTitle.Displayed;
        }
        
        public void WaitForFormSubmit()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 20));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.UrlToBe( "http://localhost:4200/hospitalizes-patients"));
        }
        
    }
}