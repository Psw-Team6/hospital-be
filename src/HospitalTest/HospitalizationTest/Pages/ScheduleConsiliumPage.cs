using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace HospitalTest.HospitalizationTest.Pages
{
    public class ScheduleConsiliumPage
    {
        private readonly IWebDriver driver;
        public const string URI = "http://localhost:4200/schedule-consilium";

        private IWebElement themeInput => driver.FindElement(By.Id("mat-input-12"));
        private IWebElement durationInput => driver.FindElement(By.Id("mat-input-15"));
        private IWebElement startDateInput => driver.FindElement(By.Id("mat-input-13"));
        private IWebElement endDateInput => driver.FindElement(By.Id("mat-input-14"));
        private IWebElement doctorButton => driver.FindElement(By.Id("nextDoctors"));
        private IWebElement specializationButton => driver.FindElement(By.Id("nextSpecializations"));
        private IWebElement submitButton => driver.FindElement(By.Id("submit"));
        
        public ScheduleConsiliumPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public bool ThemeInputDisplayed()
        {
            return themeInput.Displayed;
        }
        public bool DurationInputDisplayed()
        {
            return durationInput.Displayed;
        }
        public bool StartDateInputDisplayed()
        {
            return startDateInput.Displayed;
        }
        public bool EndDateInputDisplayed()
        {
            return endDateInput.Displayed;
        }
 
        
        public void InsertTheme(string amount)
        {
            themeInput.SendKeys(amount);
        }
        public void InsertDuration(string duration)
        {
            durationInput.SendKeys(duration);
        }
        
        public void InsertStartDate(string duration)
        {
            startDateInput.SendKeys(duration);
        }
        
        public void InsertEndDate(string duration)
        {
            endDateInput.SendKeys(duration);
        }

        
        public void DoctorSubmit()
        {
            doctorButton.Click();
        }
        public void SpecializationSubmit()
        {
            specializationButton.Click();
        }
        public void SubmitForm()
        {
            submitButton.Click();
        }
        
        
        public void WaitForConsiliumNavigate()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 20));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.UrlToBe("http://localhost:4200/consiliums"));
        }
    }
}