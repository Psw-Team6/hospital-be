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
    public class CheckBloodSupplyTest
    {
        public readonly IWebDriver driver;
        private Pages.LoginPage loginPage;
        private Pages.RoomsPage roomsPage;
        private Pages.BloodSupplyPage supplyPage;

        public CheckBloodSupplyTest()
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

            loginPage.InsertUsername("Manager");
            loginPage.InsertPassword("123");
            loginPage.SubmitForm();
            loginPage.WaitForFormSubmit();

            roomsPage = new Pages.RoomsPage(driver);
            Assert.True(roomsPage.bloodSupplyNavDisplayed());
            roomsPage.bloodSupplyNavClick();
            roomsPage.WaitForFormSubmit();

            supplyPage = new Pages.BloodSupplyPage(driver);
            Assert.True(supplyPage.BloodTypeInputDisplayed());
            Assert.True(supplyPage.BloodAmountInputDisplayed());
            Assert.True(supplyPage.SubmitButtonDisplayed());

            supplyPage.InsertBloodType("bloodTypeSelect","state1");
            

        }

        [Fact]
        public void check_blood_supply()
        {
            string beforeClick = supplyPage.getResponseText();
            supplyPage.SubmitForm();
            Thread.Sleep(2000);

            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 20));

            IAlert alert = wait.Until(drv => {
                try
                {
                    return drv.SwitchTo().Alert();
                }
                catch (NoAlertPresentException)
                {
                    return null;
                }
            });
            alert.Accept();
            Assert.False(supplyPage.getResponseText().Equals("Response from blood bank: Connection error!"));
        }
    }
}
