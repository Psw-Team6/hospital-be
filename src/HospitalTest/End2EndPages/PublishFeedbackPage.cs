using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace HospitalTest.End2EndPages
{
    public class PublishFeedbackPage
    {
        private readonly IWebDriver _driver;
        private const string Uri = "http://localhost:4200/feedback";
        private IWebElement PublishButton => _driver.FindElement(By.Id("publish"));

        public PublishFeedbackPage(IWebDriver driver)
        {
            _driver = driver;
        }
        
        public void Navigate() => _driver.Navigate().GoToUrl(Uri);
        
        public void Publish()
        {
            PublishButton.Click();
        }
        
        public void WaitForFormSubmit()
        {
            var wait = new WebDriverWait(_driver, new TimeSpan(0, 0, 1));
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