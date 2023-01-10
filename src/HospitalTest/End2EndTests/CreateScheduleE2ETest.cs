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
            _loginPage.InsertPassword("123");
            _loginPage.SubmitForm();
            _loginPage.WaitForFormSubmitDoctor();
        }
        [Fact]
        public void Create_schedule_success()
        {
            Login();
            _createSchedulePage.Navigate();
            _createSchedulePage.EnterDate("6/20/2023");
            _createSchedulePage.EnterStartTime("10:00 AM");
            _createSchedulePage.EnterFinishTime("10:30 AM");
            _createSchedulePage.MultiSelectClick();
            _createSchedulePage.SelectPatient();
            _createSchedulePage.Submit();
            _createSchedulePage.WaitForFormSubmit();
            Thread.Sleep(2000);
            //assert
            Assert.Equal(_webDriver.Url,CreateSchedulePage.UriDashboard);
            _webDriver.Dispose();
        }
        [Fact]
        public void Create_schedule_already_scheduled()
        {
            Login();
            _createSchedulePage.Navigate();
            _createSchedulePage.EnterDate("12/30/2022");
            _createSchedulePage.EnterStartTime("11:00 AM");
            _createSchedulePage.EnterFinishTime("11:30 AM");
            _createSchedulePage.MultiSelectClick();
            _createSchedulePage.SelectPatient();
            _createSchedulePage.Submit();
            Thread.Sleep(2000);
            //assert
            Assert.NotEqual(_webDriver.Url,CreateSchedulePage.UriDashboard);
            _webDriver.Dispose();
        }
        [Fact]
        public void Create_schedule_patient_not_selected()
        {
            Login();
            _createSchedulePage.Navigate();
            _createSchedulePage.EnterDate("12/24/2022");
            _createSchedulePage.EnterStartTime("11:00 AM");
            _createSchedulePage.EnterFinishTime("11:30 AM");
            _createSchedulePage.Submit();
            Thread.Sleep(2000);
            //assert
            Assert.NotEqual(_webDriver.Url,CreateSchedulePage.UriDashboard);
            _webDriver.Dispose();
        }
        [Fact]
        public void Create_schedule_not_valid_date()
        {
            Login();
            _createSchedulePage.Navigate();
            _createSchedulePage.EnterDate("12/12/2022");
            _createSchedulePage.EnterStartTime("11:00 AM");
            _createSchedulePage.EnterFinishTime("11:30 AM");
            _createSchedulePage.MultiSelectClick();
            _createSchedulePage.SelectPatient();
            _createSchedulePage.Submit();
            Thread.Sleep(2000);
            //assert
            Assert.NotEqual(_webDriver.Url,CreateSchedulePage.UriDashboard);
            _webDriver.Dispose();
        }
        [Fact]
        public void Create_schedule_not_valid_range()
        {
            Login();
            _createSchedulePage.Navigate();
            _createSchedulePage.EnterDate("12/12/2022");
            _createSchedulePage.EnterStartTime("11:00 AM");
            _createSchedulePage.EnterFinishTime("10:30 AM");
            _createSchedulePage.MultiSelectClick();
            _createSchedulePage.SelectPatient();
            _createSchedulePage.Submit();
            Thread.Sleep(2000);
            //assert
            Assert.NotEqual(_webDriver.Url,CreateSchedulePage.UriDashboard);
            _webDriver.Dispose();
        }
    }
}