using System;
using System.Threading;
using HospitalTest.End2EndCommon;
using OpenQA.Selenium;
using Xunit;

namespace HospitalTest.End2EndTests
{
    public class LoginE2ETest
    {
        private IWebDriver _webDriver;
        private readonly LoginPage _loginPage;

        public LoginE2ETest()
        {
            var browserOptions = new BrowserOptions();
            _webDriver = browserOptions.CreateChromeDriver();
            _loginPage = new LoginPage(_webDriver);

        }

        [Fact]
        public void Doctor_success_login()
        {
            _loginPage.Navigate();
            _loginPage.InsertUsername("Ilija");
            Thread.Sleep(2000);
            _loginPage.InsertPassword("123");
            Thread.Sleep(2000);
            _loginPage.SubmitForm();
            _loginPage.WaitForFormSubmitDoctor();
            Thread.Sleep(2000);
            _webDriver.Dispose();
        }
    }
}