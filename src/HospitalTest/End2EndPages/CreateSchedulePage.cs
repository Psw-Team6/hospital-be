using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace HospitalTest.End2EndPages
{
    public class CreateSchedulePage
    {
        private readonly IWebDriver _driver;
        public const string Uri = "http://localhost:4200/create-schedule";
        public const string UriDashboard = "http://localhost:4200/dashboard";

        private IWebElement DateInput => _driver.FindElement(By.Id("dateSchedule"));
        private IWebElement StartTimeInput => _driver.FindElement(By.Id("startTimeSchedule"));
        private IWebElement FinishTimeInput => _driver.FindElement(By.Id("finishTimeSchedule"));
        private IWebElement SubmitButton => _driver.FindElement(By.Id("submitSchedule"));
        private IWebElement ToastMessage => _driver.FindElement(By.XPath("/html/body/app-root/lib-ng-toast/div/div[2]/p[2]"));
        private IWebElement MultiSelect => _driver.FindElement(By.CssSelector(".mat-select-trigger"));

        public CreateSchedulePage(IWebDriver driver)
        {
            _driver = driver;
        }
        public void Navigate() => _driver.Navigate().GoToUrl(Uri);

        public void Submit()
        {
            SubmitButton.Click();
        }

        public void EnterDate(string date)
        {
            DateInput.SendKeys(date);
        }
        public void EnterStartTime(string date)
        {
            StartTimeInput.SendKeys(date);
        }
        public void EnterFinishTime(string date)
        {
            FinishTimeInput.SendKeys(date);
        }

        public void MultiSelectClick()
        {
            MultiSelect.Click();
        }

        public void SelectPatient()
        {
            var patientSelect  =_driver.FindElements(By.ClassName("mat-option-text"));
            patientSelect[0].Click();
        }

        public string GetToastMessage()
        {
           return ToastMessage.Text;
        }
        public void WaitForFormSubmit()
        {
            var wait = new WebDriverWait(_driver, new TimeSpan(0, 0, 10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.UrlToBe(UriDashboard));
        }
    }
}