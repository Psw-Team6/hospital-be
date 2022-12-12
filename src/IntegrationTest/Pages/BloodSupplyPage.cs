using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTest.Pages
{
    public class BloodSupplyPage
    {
        private readonly IWebDriver driver;
        public const string URI = "http://localhost:4200/bloodSupply";

        private IWebElement bloodTypeInput => driver.FindElement(By.Id("bloodTypeSelect"));
        private IWebElement bloodAmountInput => driver.FindElement(By.Id("bloodAmountInput"));
        private IWebElement submitButton => driver.FindElement(By.Id("submitBloodSupply"));
        private IWebElement responseLabel => driver.FindElement(By.Id("responseStatus"));


        public BloodSupplyPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public bool BloodTypeInputDisplayed()
        {
            return bloodTypeInput.Displayed;
        }
        public bool BloodAmountInputDisplayed()
        {
            return bloodAmountInput.Displayed;
        }
        public bool SubmitButtonDisplayed()
        {
            return submitButton.Displayed;
        }
        public void InsertBloodType(string id, string value)
        {
            driver.FindElement(By.Id(id)).Click();
            driver.FindElement(By.Id(value)).Click();
        }
        public void InsertBloodAmount(string amount)
        {
            bloodAmountInput.SendKeys(amount);
        }
        public void SubmitForm()
        {
            submitButton.Click();
        }
        public string getResponseText()
        {
            return responseLabel.Text;
        }
    }
}
