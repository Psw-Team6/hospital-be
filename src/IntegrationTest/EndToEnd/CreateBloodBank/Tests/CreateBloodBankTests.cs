using IntegrationLibrary.SendMail.Services;
using Moq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTest.EndToEnd.CreateBloodBank.Tests
{
    public class CreateBloodBankTests
    {
        private readonly IWebDriver driver;
        private Pages.CreateBloodBankPage createBloodBankPage;

        public CreateBloodBankTests()
        {
            // options for launching Google Chrome
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("start-maximized");            // open Browser in maximized mode
            options.AddArguments("disable-infobars");           // disabling infobars
            options.AddArguments("--disable-extensions");       // disabling extensions
            options.AddArguments("--disable-gpu");              // applicable to windows os only
            options.AddArguments("--disable-dev-shm-usage");    // overcome limited resource problems
            options.AddArguments("--no-sandbox");               // Bypass OS security model
            options.AddArguments("--disable-notifications");    // disable notifications

            driver = new ChromeDriver(options);

            createBloodBankPage = new Pages.CreateBloodBankPage(driver); 

            //Assert.Equal(driver.Url, Pages.CreateBloodBankPage.URI);

            Assert.True(createBloodBankPage.NameElementDisplayed());          // check if form input elements are displayed
            Assert.True(createBloodBankPage.ServerAddressElementDisplayed());
            Assert.True(createBloodBankPage.EmailElementDisplayed());
            Assert.True(createBloodBankPage.SubmitButtonElementDisplayed());
        }
        public void Dispose()
        {
            driver.Quit();
            driver.Dispose();
        }

        [Fact]
        public void TestSuccessfulSubmit()
        {
            createBloodBankPage.InsertName("Banka krvi 1");                     // insert all values except price
            createBloodBankPage.InsertServerAddress("adresa");
            createBloodBankPage.InsertEmail("deki555@hotmail.com");
            createBloodBankPage.SubmitForm();
            createBloodBankPage.WaitForFormSubmit();
        }
    }
}
