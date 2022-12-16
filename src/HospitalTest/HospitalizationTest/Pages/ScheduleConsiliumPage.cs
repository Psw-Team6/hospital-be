using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace HospitalTest.HospitalizationTest.Pages
{
    public class ScheduleConsiliumPage
    {
        private readonly IWebDriver _driver;
        public const string Uri = "http://localhost:4200/schedule-consilium";
        public const string UriDashboard = "http://localhost:4200/consiliums";

        private IWebElement ThemeInput => _driver.FindElement(By.Id("theme"));
        private IWebElement DurationInput => _driver.FindElement(By.Id("duration"));
        private IWebElement StartDateInput => _driver.FindElement(By.Id("startDate"));
        private IWebElement EndDateInput => _driver.FindElement(By.Id("finishDate"));
        private IWebElement DoctorButton => _driver.FindElement(By.Id("nextDoctors"));
        private IWebElement SpecializationButton => _driver.FindElement(By.Id("nextSpecializations"));
        private IWebElement SubmitButton => _driver.FindElement(By.Id("submitConsilium"));
        private IWebElement MultiSelectDoctor => _driver.FindElement(By.CssSelector("mat-select[id='multi-select-doctor']"));
        private IWebElement MultiSelectSpecialization => _driver.FindElement(By.CssSelector("mat-select[id='multi-select-spec']"));

        public ScheduleConsiliumPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public bool IsMultiVisible()
        {
            return MultiSelectDoctor.Displayed;
        }
        public bool IsMultiVisibleSpec()
        {
            return MultiSelectSpecialization.Displayed;
        }
        public void Esc()
        {
            MultiSelectDoctor.SendKeys(Keys.Escape);
        }
        public void EscSpec()
        {
            MultiSelectSpecialization.SendKeys(Keys.Escape);
        }

        public bool ThemeInputDisplayed()
        {
            return ThemeInput.Displayed;
        }
        public bool DurationInputDisplayed()
        {
            return DurationInput.Displayed;
        }
        public bool StartDateInputDisplayed()
        {
            return StartDateInput.Displayed;
        }
        public bool EndDateInputDisplayed()
        {
            return EndDateInput.Displayed;
        }
        
 
        
        public void InsertTheme(string amount)
        {
            ThemeInput.SendKeys(amount);
        }
        public void InsertDuration(string duration)
        {
            DurationInput.SendKeys(duration);
        }
        
        public void InsertStartDate(string duration)
        {
            StartDateInput.SendKeys(duration);
        }
        public void MultiSelectDocClick()
        {
            MultiSelectDoctor.Click();
        }
        public void MultiSelectSpecClick()
        {
            MultiSelectSpecialization.Click();
        }
        public void Select()
        {
            var select  =_driver.FindElements(By.ClassName("mat-option-text"));
            select[0].Click();
        }
        
        public void InsertEndDate(string duration)
        {
            EndDateInput.SendKeys(duration);
        }

        
        public void DoctorSubmit()
        {
            DoctorButton.Click();
        }
        public void SpecializationSubmit()
        {
            SpecializationButton.Click();
        }
        public void SubmitForm()
        {
            SubmitButton.Click();
        }
        
        
        public void WaitForConsiliumNavigate()
        {
            var wait = new WebDriverWait(_driver, new TimeSpan(0, 0, 20));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.UrlToBe("http://localhost:4200/consiliums"));
        }
    }
}