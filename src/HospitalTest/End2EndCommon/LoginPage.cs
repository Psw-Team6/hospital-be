using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace HospitalTest.End2EndCommon
{
    public class LoginPage
    {
        private readonly IWebDriver _driver;
        private const string UriBase = "http://localhost:4200";

        private IWebElement UsernameInput => _driver.FindElement(By.XPath("//*[@id=\"container\"]/div[2]/form/input[1]"));
        private IWebElement PasswordInput => _driver.FindElement(By.XPath("//*[@id=\"container\"]/div[2]/form/input[2]"));
        private IWebElement LogInButton => _driver.FindElement(By.XPath("//*[@id=\"container\"]/div[2]/form/button"));

        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public void Navigate() => _driver.Navigate().GoToUrl(UriBase);
        public void EnsurePageIsDisplayed()
        {
            var wait = new WebDriverWait(_driver, new TimeSpan(0, 0, 20));
            wait.Until(condition =>
            {
                try
                {
                    return UsernameInput != null && PasswordInput != null;
                }
                catch (StaleElementReferenceException)
                {
                    return false;
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
            });
        }

        public bool UsernameInputDisplayed()
        {
            return UsernameInput.Displayed;
        }
        public bool PasswordInputDisplayed()
        {
            return PasswordInput.Displayed;
        }
        public bool LoginButtonDisplayed()
        {
            return LogInButton.Displayed;
        }
        public void InsertUsername(string username)
        {
            UsernameInput.SendKeys(username);
        }
        public void InsertPassword(string password)
        {
            PasswordInput.SendKeys(password);
        }
        public void SubmitForm()
        {
            LogInButton.Click();
        }
        public void WaitForFormSubmitDoctor()
        {
            var wait = new WebDriverWait(_driver, new TimeSpan(0, 0, 10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.UrlToBe("http://localhost:4200/dashboard"));
        }
    }
}
