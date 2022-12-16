using System.Threading;
using HospitalTest.End2EndCommon;
using HospitalTest.End2EndPages;
using OpenQA.Selenium;
using Xunit;

namespace HospitalTest.End2EndTests
{
    public class CancelAppointmentE2ETest
    {
        private readonly IWebDriver _webDriver;
        private readonly LoginPage _loginPage;
        private readonly CancelAppointmentPage _cancelAppointmentPage;
        
        public CancelAppointmentE2ETest()
        {
            var browserOptions = new BrowserOptions();
            _webDriver = browserOptions.CreateChromeDriver();
            _loginPage = new LoginPage(_webDriver);
            _cancelAppointmentPage = new CancelAppointmentPage(_webDriver);
        }

        private void Login()
        {
            _loginPage.Navigate();
            _loginPage.InsertUsername("Sale");
            Thread.Sleep(1000);
            _loginPage.InsertPassword("123");
            Thread.Sleep(1000);
            _loginPage.SubmitForm();
            //_loginPage.WaitForFormSubmitDoctor();
            Thread.Sleep(1000);
        }
        
        [Fact]
        public void Cancel_appointment_success()
        {
            Login();
            Thread.Sleep(1000);
            _cancelAppointmentPage.Navigate();
            _cancelAppointmentPage.ChangeDay();
            _cancelAppointmentPage.Cancel();
            Thread.Sleep(2000);
            _cancelAppointmentPage.WaitForFormSubmit();
            _webDriver.Dispose();
        }
    }
}