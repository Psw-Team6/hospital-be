using IntegrationTest.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTest.E2E
{
    public class CreateTenderTest
    {
        public readonly IWebDriver driver;
        private Pages.LoginPage loginPage;
        private Pages.CreateTenderPage createTenderPage;
        public CreateTenderTest()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("start-maximized");            // open Browser in maximized mode
            options.AddArguments("disable-infobars");           // disabling infobars
            options.AddArguments("--disable-extensions");       // disabling extensions
            options.AddArguments("--disable-gpu");              // applicable to windows os only
            options.AddArguments("--disable-dev-shm-usage");    // overcome limited resource problems
            options.AddArguments("--no-sandbox");               // Bypass OS security model
            options.AddArguments("--disable-notifications");    // disable notifications


            driver = new ChromeDriver(options);

            loginPage = new Pages.LoginPage(driver);
            loginPage.Navigate();
            Assert.True(loginPage.LoginButtonDisplayed());
            Assert.True(loginPage.UsernameInputDisplayed());
            Assert.True(loginPage.PasswordInputDisplayed());

            loginPage.InsertUsername("ManagerBB");
            loginPage.InsertPassword("123");
            loginPage.SubmitForm();
            loginPage.WaitForFormSubmitRequest();
            createTenderPage = new CreateTenderPage(driver);
            Assert.True(createTenderPage.BloodAmountInputDisplayed());
            Assert.True(createTenderPage.createTenderDisplayed());
            Assert.True(createTenderPage.hasDeadlineDisplayed());
            createTenderPage.SelectDeadline("mat-input-8", "hasno");
            createTenderPage.InsertBloodAmount("5");
        }

        [Fact]
        public void create_tender()
        {
  
            createTenderPage.CreateTender();
            Thread.Sleep(2000);
        }
    }
}
