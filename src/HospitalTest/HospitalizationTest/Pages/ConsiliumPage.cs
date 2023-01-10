using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace HospitalTest.HospitalizationTest.Pages
{
    public class ConsiliumPage
    {
        private readonly IWebDriver driver;
        public const string URI = "http://localhost:4200/consiliums";
        
        private IWebElement submitButton => driver.FindElement(By.XPath("/html/body/app-root/app-body/div/app-consilium-dashboard/div/div/div[2]/button/span[1]"));
       
        public ConsiliumPage(IWebDriver driver)
        {
            this.driver = driver;
        }
        
        public bool ScheduleConsiliumDisplayed()
        {
            return submitButton.Displayed;
        }
        
        public void WaitForBtnScheduleConsilium()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 20));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.UrlToBe("http://localhost:4200/schedule-consilium"));
        }

        public void schecduleConsiliumBtnClick()
        {
            submitButton.Click();
        }
    }
}