using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTest.Pages
{
    public class LoginPage
    {
        private readonly IWebDriver driver;
        public const string URI = "http://localhost:4200";

        private IWebElement usernameInput => driver.FindElement(By.XPath("//*[@id=\"container\"]/div[2]/form/input[1]"));
        private IWebElement passwordInput => driver.FindElement(By.XPath("//*[@id=\"container\"]/div[2]/form/input[2]"));
        private IWebElement logInButton => driver.FindElement(By.XPath("//*[@id=\"container\"]/div[2]/form/button"));

        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void Navigate() => driver.Navigate().GoToUrl(URI);
        public void EnsurePageIsDisplayed()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 20));
            wait.Until(condition =>
            {
                try
                {
                    return usernameInput != null && passwordInput != null;
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
            return usernameInput.Displayed;
        }
        public bool PasswordInputDisplayed()
        {
            return passwordInput.Displayed;
        }
        public bool LoginButtonDisplayed()
        {
            return logInButton.Displayed;
        }
        public void InsertUsername(string username)
        {
            usernameInput.SendKeys(username);
        }
        public void InsertPassword(string password)
        {
            passwordInput.SendKeys(password);
        }
        public void SubmitForm()
        {
            logInButton.Click();
        }
        public void WaitForFormSubmit()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 20));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.UrlToBe("http://localhost:4200/rooms"));
        }
        public void WaitForFormSubmitRequest()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 20));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.UrlToBe("http://localhost:4200/tender/add"));
        }
    }
}
