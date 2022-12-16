using System.Threading;
using HospitalTest.End2EndCommon;
using HospitalTest.End2EndPages;
using HospitalTest.HospitalizationTest.Pages;
using OpenQA.Selenium;
using Xunit;

namespace HospitalTest.End2EndTests
{
    public class CreateFeedbackE2ETest
    {
        private readonly IWebDriver _webDriver;
        private readonly LoginPage _loginPage;
        private readonly HomePage _homePage;

        public CreateFeedbackE2ETest()
        {
            var browserOptions = new BrowserOptions();
            _webDriver = browserOptions.CreateChromeDriver();
            _loginPage = new LoginPage(_webDriver);
            _homePage = new HomePage(_webDriver);
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
        public void Create_feedback_success()
        {
            Login();
            _homePage.NavFeedbackClick();
            Assert.True(_homePage.NavFeedbackDisplayed());
            Assert.True(_homePage.InputTextDisplayed());
            Assert.True(_homePage.SubmitFeedbackDisplayed());
            _homePage.InputText("Komentar");
            _homePage.SubmitButtonClick();
            _homePage.WaitForAlertDialog();
            Assert.Equal("Feedback sent for review.", _homePage.GetDialogMessage());



            _webDriver.Dispose();
        }
        
        [Fact]
        public void Create_feedback_reason_empty()
        {
            Login();
            _homePage.NavFeedbackClick();
            Assert.True(_homePage.NavFeedbackDisplayed());
            Assert.True(_homePage.InputTextDisplayed());
            Assert.True(_homePage.SubmitFeedbackDisplayed());
            _homePage.InputText("");
            _homePage.SubmitButtonClick();
            _homePage.WaitForAlertDialog();
            Assert.Equal("Feedback cannot be empty.", _homePage.GetDialogMessage());



            _webDriver.Dispose();
        }
        
       
    }
}