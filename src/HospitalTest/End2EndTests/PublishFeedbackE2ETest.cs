using System.Threading;
using HospitalTest.End2EndCommon;
using HospitalTest.End2EndPages;
using OpenQA.Selenium;
using Xunit;

namespace HospitalTest.End2EndTests
{
    public class PublishFeedbackE2ETest
    {
        
        private readonly IWebDriver _webDriver;
        private readonly LoginPage _loginPage;
        private readonly PublishFeedbackPage _publishFeedbackPage;

        public PublishFeedbackE2ETest()
        {
            var browserOptions = new BrowserOptions();
            _webDriver = browserOptions.CreateChromeDriver();
            _loginPage = new LoginPage(_webDriver);
            _publishFeedbackPage = new PublishFeedbackPage(_webDriver);
          
        }
        
        private void Login()
        {
            _loginPage.Navigate();
            _loginPage.InsertUsername("Manager");
            Thread.Sleep(1000);
            _loginPage.InsertPassword("123");
            Thread.Sleep(1000);
            _loginPage.SubmitForm();
            //_loginPage.WaitForFormSubmitDoctor();
            Thread.Sleep(1000);
        }
        
        [Fact]
        public void Publish_feedback_success()
        {
            Login();
            Thread.Sleep(1000);
            _publishFeedbackPage.Navigate();
            Thread.Sleep(2000);
            _publishFeedbackPage.Publish();
            _publishFeedbackPage.WaitForFormSubmit();
            _publishFeedbackPage.WaitForAlertDialog();
            Assert.Equal("success", _publishFeedbackPage.GetDialogMessage());
            _webDriver.Dispose();
        }
        
    }
}