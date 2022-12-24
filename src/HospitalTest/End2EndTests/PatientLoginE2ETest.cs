using System;
using System.Threading;
using HospitalTest.End2EndCommon;
using OpenQA.Selenium;
using Xunit;

namespace HospitalTest.End2EndTests
{
    
    public class PatientLoginE2ETest
    {
        private  IWebDriver _webDriver;
        private  PatientLoginPage _loginPage;

        public PatientLoginE2ETest()
        {
            var browserOptions = new BrowserOptions();
            _webDriver = browserOptions.CreateChromeDriver();
            _loginPage = new PatientLoginPage(_webDriver);

        }

       
        
        [Fact]
        public void Patient_success_login()
        {
            _loginPage.Navigate();
            _loginPage.InsertUsername("Sale");
            _loginPage.InsertPassword("123");
            _loginPage.SubmitForm();
            _loginPage.WaitForFormSubmitDoctor();
            _webDriver.Dispose();
        }
        
        [Fact]
        public void Doctor_bad_credential()
        {
            _loginPage.Navigate();
            _loginPage.InsertUsername("Ilija");
            Thread.Sleep(1000);
            _loginPage.InsertPassword("1234");
            Thread.Sleep(1000);
            _loginPage.SubmitForm();
            try
            {
                _loginPage.WaitForFormSubmitDoctor();
                Thread.Sleep(2000);
            }
            catch (WebDriverTimeoutException e)
            {
                Assert.True(e.Message.Equals("Timed out after 5 seconds"));
                _webDriver.Dispose();
            }
            _webDriver.Dispose();
        }
    }
}