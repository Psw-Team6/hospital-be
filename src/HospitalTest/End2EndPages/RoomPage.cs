using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
namespace HospitalTest.End2EndPages
{
    public class RoomPage
    {
        private readonly IWebDriver driver;
        public const string URI = "http://localhost:4200/rooms";

        IWebElement NavMalicious => driver.FindElement(By.XPath("/html/body/app-root/app-sidenav/div/ul/li[4]/a/i"));
     
        public void Navigate() => driver.Navigate().GoToUrl(URI);
        public RoomPage(IWebDriver driver)
        {
            this.driver = driver;
        }
        public bool NavMaliciousDisplayed()
        {
            return NavMalicious.Displayed;
        }
        
        public void NavMaliciousClick()
        {
            NavMalicious.Click();
        }
        

        public void WaitForNextPage()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 20));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.UrlToBe("http://localhost:4200/malicious-patients"));
        }
        
        
        
    }
}