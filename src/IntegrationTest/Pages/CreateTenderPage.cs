using IntegrationLibrary.Tender.Model;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTest.Pages
{
    public class CreateTenderPage
    {
        private readonly IWebDriver driver;
        public const string URI = "http://localhost:4200/tender/add";
        private IWebElement createTender => driver.FindElement(By.XPath("/html/body/app-root/app-body/div/app-create-tender/div/div/div[12]/button/span[1]"));
        private IWebElement bloodAmountInput => driver.FindElement(By.Id("mat-input-0"));
        private IWebElement toastMessage => driver.FindElement(By.XPath("/html/body/app-root/lib-ng-toast/div/div[2]/p[2]"));
        private IWebElement hasDeadline => driver.FindElement(By.XPath("//*[@id=\"mat-input-8\"]"));
        public CreateTenderPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public bool createTenderDisplayed()
        {
            return createTender.Displayed;
        }
        public bool BloodAmountInputDisplayed()
        {
            return bloodAmountInput.Displayed;
        }
        public bool toastMessageDisplayed()
        {
            return toastMessage.Displayed;
        }
        public bool hasDeadlineDisplayed()
        {
            return hasDeadline.Displayed;
        }
        public void InsertBloodAmount(string amount)
        {
            bloodAmountInput.SendKeys(amount);
        }

        public void SelectDeadline(string id, string value)
        {
            driver.FindElement(By.Id(id)).Click();
            driver.FindElement(By.Id(value)).Click();
        }

        public void CreateTender()
        {
            createTender.Click();
        }
        public string getToastText()
        {
            return toastMessage.Text;
        }
    }
}
