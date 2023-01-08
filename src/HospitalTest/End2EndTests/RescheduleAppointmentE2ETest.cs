using System.Threading;
using HospitalTest.End2EndCommon;
using HospitalTest.End2EndPages;
using OpenQA.Selenium;
using Xunit;

namespace HospitalTest.End2EndTests
{
    public class RescheduleAppointmentE2ETest
    {
        private readonly IWebDriver _webDriver;
        private readonly LoginPage _loginPage;
        private readonly RescheduleAppointmentPage _createSchedulePage;
        private readonly string _appId = "c0576733-b7fa-4974-b60c-d3d7e8c9f216";

        public RescheduleAppointmentE2ETest()
        {
            var browserOptions = new BrowserOptions();
            _webDriver = browserOptions.CreateChromeDriver();
            _loginPage = new LoginPage(_webDriver);
            _createSchedulePage = new RescheduleAppointmentPage(_webDriver);
        }

        private void Login()
        {
            _loginPage.Navigate();
            _loginPage.InsertUsername("Ilija");
            _loginPage.InsertPassword("123");
            _loginPage.SubmitForm();
            _loginPage.WaitForFormSubmitDoctor();
        }
        [Fact]
        public void Create_schedule_success()
        {
            Login();
            _createSchedulePage.Navigate(_appId);
            _createSchedulePage.EnterDate("1/25/2023");
            _createSchedulePage.EnterStartTime("09:00 AM");
            _createSchedulePage.EnterFinishTime("09:30 AM");
            _createSchedulePage.Submit();
            Thread.Sleep(2000);
            //assert
            Assert.Equal(_webDriver.Url,CreateSchedulePage.UriDashboard);
            _webDriver.Dispose();
        }
        [Fact]
        public void Create_schedule_invalid_date()
        {
            Login();
            _createSchedulePage.Navigate(_appId);
            _createSchedulePage.EnterDate("12/30/2021");
            _createSchedulePage.EnterStartTime("11:00 AM");
            _createSchedulePage.EnterFinishTime("11:30 AM");
            _createSchedulePage.Submit();
            Thread.Sleep(2000);
            //assert
            Assert.NotEqual(_webDriver.Url,CreateSchedulePage.UriDashboard);
            _webDriver.Dispose();
        }
        [Fact]
        public void Create_schedule_invalid_range()
        {
            Login();
            _createSchedulePage.Navigate(_appId);
            _createSchedulePage.EnterDate("12/30/2021");
            _createSchedulePage.EnterStartTime("11:00 AM");
            _createSchedulePage.EnterFinishTime("10:30 AM");
            _createSchedulePage.Submit();
            Thread.Sleep(2000);
            //assert
            Assert.NotEqual(_webDriver.Url,CreateSchedulePage.UriDashboard);
            _webDriver.Dispose();
        }
        [Fact]
        public void Create_schedule_already_scheduled()
        {
            Login();
            _createSchedulePage.Navigate(_appId);
            _createSchedulePage.EnterDate("12/30/2021");
            _createSchedulePage.EnterStartTime("11:00 AM");
            _createSchedulePage.EnterFinishTime("11:30 AM");
            _createSchedulePage.Submit();
            Thread.Sleep(2000);
            //assert
            Assert.NotEqual(_webDriver.Url,CreateSchedulePage.UriDashboard);
            _webDriver.Dispose();
        }
    }
}