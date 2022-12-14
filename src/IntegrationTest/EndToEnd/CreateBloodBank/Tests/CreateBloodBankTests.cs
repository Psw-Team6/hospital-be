using IntegrationLibrary.SendMail.Services;
using IntegrationTest.EndToEnd.CreateBloodBank.Pages;
using IntegrationTest.Pages;
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
        private LoginPage loginPage;
        private RoomsPage roomsPage;

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

            loginPage = new LoginPage(driver);
            loginPage.Navigate();
            Assert.True(loginPage.LoginButtonDisplayed());
            Assert.True(loginPage.UsernameInputDisplayed());
            Assert.True(loginPage.PasswordInputDisplayed());

            loginPage.InsertUsername("Manager");
            loginPage.InsertPassword("123");
            loginPage.SubmitForm();
            loginPage.WaitForFormSubmit();

            roomsPage = new RoomsPage(driver);
            Assert.True(roomsPage.bloodBankNavDisplayed());
            roomsPage.bloodBankNavClick();
            roomsPage.WaitForFormSubmitBloodBank();

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

        [Fact]
        public void TestInvalidEmailForm()
        {
            createBloodBankPage.InsertName("Banka krvi 1");                     // insert all values except price
            createBloodBankPage.InsertServerAddress("adresa");
            createBloodBankPage.InsertEmail("nevalidan unos mejla");
            createBloodBankPage.ClickOnElementsCoordinates(driver, 200, 200);
            createBloodBankPage.WaitForErrorMessage();
            String error = createBloodBankPage.GetErrorMessage();
            Assert.Equal(error, createBloodBankPage.getInvalidEmailMessage());
        }
    }
}
