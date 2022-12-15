using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTest.Pages
{
    public class GenerateAndSendPage
    {
        private readonly IWebDriver driver;
        public const string URI = "http://localhost:4200/configureSendingReports";

        private IWebElement bloodBankName => driver.FindElement(By.Id("bloodBankName"));
        private IWebElement generatePeriod => driver.FindElement(By.Id("generatePeriod"));
        private IWebElement sendPeriod => driver.FindElement(By.Id("sendPeriod"));
        private IWebElement submitButton => driver.FindElement(By.Id("submitButton"));
        private IWebElement customSendPeriod => driver.FindElement(By.Id("customSendPeriod"));
        private IWebElement customGeneratePeriod => driver.FindElement(By.Id("customGeneratePeriod"));

        public GenerateAndSendPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public bool BloodBankNameDysplay()
        {
            return bloodBankName.Displayed;
        }
        public bool GeneratePeriodDisplay()
        {
            return generatePeriod.Displayed;
        }

        public bool SendPeriodDisplay()
        {
            return sendPeriod.Displayed;
        }

        public bool CustomSendPeriodDisplay()
        {
            return customSendPeriod.Displayed;
        }

        public bool CustomGeneratePeriodDisplay()
        {
            return customGeneratePeriod.Displayed;
        }

        public bool SubmitButtonDisplayed()
        {
            return submitButton.Displayed;
        }

        public void Select(string id, string value)
        {
            driver.FindElement(By.Id(id)).Click();
            driver.FindElement(By.Id(value)).Click();
        }

        public void SubmitForm()
        {
            submitButton.Click();
        }

    }
}
