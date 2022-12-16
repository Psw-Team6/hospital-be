using OpenQA.Selenium;

namespace HospitalTest.End2EndPages
{
    public class RescheduleAppointmentPage
    {
        private readonly IWebDriver _driver;
        public const string Uri = "http://localhost:4200/reschedule-appointment/";
        public const string UriDashboard = "http://localhost:4200/dashboard";

        private IWebElement DateInput => _driver.FindElement(By.Id("dateSchedule"));
        private IWebElement StartTimeInput => _driver.FindElement(By.Id("startTimeSchedule"));
        private IWebElement FinishTimeInput => _driver.FindElement(By.Id("finishTimeSchedule"));
        private IWebElement SubmitButton => _driver.FindElement(By.Id("submitSchedule"));
        public void Navigate(string app) => _driver.Navigate().GoToUrl($"{Uri+app}");
        public RescheduleAppointmentPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public void Submit()
        {
            SubmitButton.Click();
        }

        public void EnterDate(string date)
        {
            for (var i = 0; i < 10; i++)
            {
                DateInput.SendKeys(Keys.Backspace);
            }
            DateInput.SendKeys(date);
        }
        public void EnterStartTime(string date)
        {
            for (var i = 0; i < 8; i++)
            {
                StartTimeInput.SendKeys(Keys.Backspace);
            }
            StartTimeInput.SendKeys(date);
        }
        public void EnterFinishTime(string date)
        {
            for (var i = 0; i < 8; i++)
            {
                FinishTimeInput.SendKeys(Keys.Backspace);
            }
            FinishTimeInput.SendKeys(date);
        }
        
    }
}