using System;
using System.Threading;
using System.Web.Razor.Parser.SyntaxTree;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace HospitalTest.End2EndPages
{
    public class UnblockPatientPage
    {
        private readonly IWebDriver _driver;
        public const string Uri = "http://localhost:4200/malicious-patients";
        
        private IWebElement blockButton => _driver.FindElement(By.Id("blockButton"));
        private IWebElement unblockButton => _driver.FindElement(By.Id("unblockButton"));

        public UnblockPatientPage(IWebDriver driver)
        {
            _driver = driver;
        }
        public void Navigate() => _driver.Navigate().GoToUrl(Uri);
        
        public bool BlockButtonDisplayed()
        {
            return blockButton.Displayed;
        }

        public bool UnblockButtonDisplayed()
        {
            return unblockButton.Displayed;
        }

        public void BlockButtonClicked()
        {
            blockButton.Click();
        }

        public void UnblockButtonClicked()
        {
            unblockButton.Click();
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