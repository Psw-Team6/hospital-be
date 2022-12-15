using System.Threading;
using HospitalTest.End2EndCommon;
using HospitalTest.End2EndPages;
using OpenQA.Selenium;
using Xunit;

namespace HospitalTest.End2EndTests
{
    public class CreateScheduleE2ETest
    {
        private readonly IWebDriver _webDriver;
        private readonly LoginPage _loginPage;
        private readonly CreateSchedulePage _createSchedulePage;

        public CreateScheduleE2ETest()
        {
            var browserOptions = new BrowserOptions();
            _webDriver = browserOptions.CreateChromeDriver();
            _loginPage = new LoginPage(_webDriver);
            _createSchedulePage = new CreateSchedulePage(_webDriver);
        }

        private void Login()
        {
            _loginPage.Navigate();
            _loginPage.InsertUsername("Ilija");
            Thread.Sleep(1000);
            _loginPage.InsertPassword("123");
            Thread.Sleep(1000);
            _loginPage.SubmitForm();
            //_loginPage.WaitForFormSubmitDoctor();
            Thread.Sleep(1000);
        }
        [Fact]
        public void Create_schedule_success()
        {
            Login();
            Thread.Sleep(1000);
            _createSchedulePage.Navigate();
            Thread.Sleep(1000);
            _createSchedulePage.EnterDate("12/20/2022");
            Thread.Sleep(1000);
            _createSchedulePage.EnterStartTime("11:00 AM");
            Thread.Sleep(1000);
            _createSchedulePage.EnterFinishTime("11:30 AM");
            Thread.Sleep(1000);
            _createSchedulePage.MultiSelectClick();
            Thread.Sleep(1000);
            _createSchedulePage.SelectPatient();
            Thread.Sleep(1000);
            _createSchedulePage.Submit();
            Thread.Sleep(2000);
            _createSchedulePage.WaitForFormSubmit();
            //assert
            _webDriver.Dispose();
        }
    }
}