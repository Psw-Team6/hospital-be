using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
namespace HospitalTest.HospitalizationTest.Pages
{
    public class HomePage
    {
        private readonly IWebDriver driver;
        public const string URI = "http://localhost:4200/home";

        IWebElement NavFeedback => driver.FindElement(By.XPath("/html/body/app-root/div/app-toolbar/mat-toolbar/div/button[3]/span[1]/mat-icon"));
        private IWebElement textInput => driver.FindElement(By.Id("textInput"));
        private IWebElement SubmitFeedback => driver.FindElement(By.Id("submitFeedback"));

        public HomePage(IWebDriver driver)
        {
            this.driver = driver;
        }
        public bool NavFeedbackDisplayed()
        {
            return NavFeedback.Displayed;
        }
        
        public bool InputTextDisplayed()
        {
            return textInput.Displayed;
        }
        
        public bool SubmitFeedbackDisplayed()
        {
            return SubmitFeedback.Displayed;
        }
        public void NavFeedbackClick()
        {
            NavFeedback.Click();
        }
        
        public void SubmitButtonClick()
        {
            SubmitFeedback.Click();
        }

        public void InputText(string text)
        {
            textInput.SendKeys(text);
        }

        public void WaitForAlertDialog()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.AlertIsPresent());
        }

        public string GetDialogMessage()
        {
            return driver.SwitchTo().Alert().Text;
        }
    }
}