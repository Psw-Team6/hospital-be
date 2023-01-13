using System.Threading;
using HospitalTest.End2EndCommon;
using HospitalTest.End2EndPages;
using OpenQA.Selenium;
using Xunit;

namespace HospitalTest.End2EndTests
{
    public class UnblockPatientE2ETest
    {
        private readonly IWebDriver _webDriver;
        private readonly LoginPage _loginPage;
        private readonly RoomPage _roomPage;
        private readonly UnblockPatientPage _unblockPage;


        public UnblockPatientE2ETest()
        {
            var browserOptions = new BrowserOptions();
            _webDriver = browserOptions.CreateChromeDriver();
            _loginPage = new LoginPage(_webDriver);
            _roomPage = new RoomPage(_webDriver);
            _unblockPage = new UnblockPatientPage(_webDriver);
        }

        private void Login()
        {
            _loginPage.Navigate();
            _loginPage.InsertUsername("Manager");
            _loginPage.InsertPassword("123");
            _loginPage.SubmitForm();
            Thread.Sleep(1000);
        }
        [Fact]
        public void Unblock_success()
        {
            Login();
            Assert.True(_roomPage.NavMaliciousDisplayed());
            _roomPage.NavMaliciousClick();
            _roomPage.WaitForNextPage();
            _unblockPage.BlockButtonDisplayed();
            _unblockPage.UnblockButtonDisplayed();
            _unblockPage.BlockButtonClicked();
            _unblockPage.WaitForAlertDialog();
            Assert.Equal("Success", _unblockPage.GetDialogMessage());
            
            
            _webDriver.Dispose();
            


        }
        
        [Fact]
        public void Block_success()
        {
            Login();
            Assert.True(_roomPage.NavMaliciousDisplayed());
            _roomPage.NavMaliciousClick();
            _roomPage.WaitForNextPage();
            _unblockPage.BlockButtonDisplayed();
            _unblockPage.UnblockButtonDisplayed();
            _unblockPage.UnblockButtonClicked();
            _unblockPage.WaitForAlertDialog();
            Assert.Equal("Success", _unblockPage.GetDialogMessage());
            
            
            _webDriver.Dispose();
            


        }
       
    }
}