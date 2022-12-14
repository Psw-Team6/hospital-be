﻿using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTest.Pages
{
    public class RoomsPage
    {
        private readonly IWebDriver driver;
        public const string URI = "http://localhost:4200/rooms";

        IWebElement bloodSupplyNav => driver.FindElement(By.XPath("/html/body/app-root/app-sidenav/div/ul/li[8]/a"));
        IWebElement bloodBankNav => driver.FindElement(By.XPath("/html/body/app-root/app-sidenav/div/ul/li[9]/a"));

        public RoomsPage(IWebDriver driver)
        {
            this.driver = driver;
        }
        public bool bloodSupplyNavDisplayed()
        {
            return bloodSupplyNav.Displayed;
        }

        public bool bloodBankNavDisplayed()
        {
            return bloodBankNav.Displayed;
        }
        public void bloodSupplyNavClick()
        {
            bloodSupplyNav.Click();
        }
        public void bloodBankNavClick()
        {
            bloodBankNav.Click();
        }
        public void WaitForFormSubmit()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 20));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.UrlToBe("http://localhost:4200/bloodBank"));
        }
        public void WaitForFormSubmitBloodBank()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 20));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.UrlToBe("http://localhost:4200/bloodBank/add"));
        }
    }
}
