using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTest.EndToEnd.CreateBloodBank.Pages
{
    public class CreateBloodBankPage
    {
        private readonly IWebDriver driver;
        public const string URI = "http://localhost:4200/bloodBank/add";
        private IWebElement NameElement => driver.FindElement(By.Id("bloodBankName"));
        private IWebElement ServerAddressElement => driver.FindElement(By.Id("serverAddress"));
        private IWebElement EmailElement => driver.FindElement(By.Id("email"));
        private IWebElement ErrorElement => driver.FindElement(By.Id("error"));
        private IWebElement SubmitButtonElement => driver.FindElement(By.Id("submitButton"));

        public string Title => driver.Title;
        public const string InvalidEmailMessage = "Email format is not valid!";

        public string getInvalidEmailMessage() 
        {
            return InvalidEmailMessage;
        }
       
        public CreateBloodBankPage(IWebDriver driver)
        {
            this.driver = driver;
        }
        public bool NameElementDisplayed()
        {
            return NameElement.Displayed;
        }
        public bool ServerAddressElementDisplayed()
        {
            return ServerAddressElement.Displayed;
        }

        public bool EmailElementDisplayed()
        {
            return EmailElement.Displayed;
        }
        public bool SubmitButtonElementDisplayed()
        {
            return SubmitButtonElement.Displayed;
        }
        public void InsertName(string name)
        {
            NameElement.SendKeys(name);
        }

        public void InsertServerAddress(string color)
        {
            ServerAddressElement.SendKeys(color);
        }

        public void InsertEmail(string price)
        {
            EmailElement.SendKeys(price);
        }

        public void SubmitForm()
        {
            SubmitButtonElement.Click();
        }
        public void WaitForErrorMessage()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("error")));
        }

        public string GetErrorMessage()
        {
            return driver.FindElement(By.Id("error")).Text;
        }
        public void WaitForFormSubmit()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 20));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.UrlToBe(CreateBloodBankPage.URI));
        }

        public void ClickOnElementsCoordinates(IWebDriver driver, int positionX, int positionY)
        {
            OpenQA.Selenium.Interactions.Actions builder = new OpenQA.Selenium.Interactions.Actions(driver);
            builder.MoveByOffset(positionX, positionY).Click().Build().Perform(); 
            builder.Release();
        }
    }
}
