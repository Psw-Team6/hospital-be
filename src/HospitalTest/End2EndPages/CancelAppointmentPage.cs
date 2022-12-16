using System;
using HospitalTest.End2EndCommon;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace HospitalTest.End2EndPages
{
    public class CancelAppointmentPage
    {
        private readonly IWebDriver _driver;
        private const string Uri = "http://localhost:4200/my-appointments";
        public IWebElement CancelButton => _driver.FindElement(By.Id("cancelButton"));
        private IWebElement ChangePageButton => _driver.FindElement(By.Id("mat-tab-label-0-2"));
        private IWebElement ChangePageButtonBad => _driver.FindElement(By.Id("mat-tab-label-0-4"));
        public CancelAppointmentPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public void Navigate() => _driver.Navigate().GoToUrl(Uri);

        public void Cancel()
        {
            CancelButton.Click();
        }
        
        public void WaitForFormSubmit()
        {
            var wait = new WebDriverWait(_driver, new TimeSpan(0, 0, 1));
        }

        public void ChangeDay()
        {
            ChangePageButton.Click();
        }
        
        public void BadChangeDay()
        {
            ChangePageButtonBad.Click();
        }
        
        public void WaitForAlertDialog()
        {
            var wait = new WebDriverWait(_driver, new TimeSpan(0, 0, 10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.AlertIsPresent());
        }
        
        public string GetDialogMessage()
        {
            return _driver.SwitchTo().Alert().Text;
        }
    }
    
    
}