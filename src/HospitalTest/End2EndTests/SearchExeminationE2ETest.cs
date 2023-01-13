using HospitalTest.End2EndCommon;
using HospitalTest.End2EndPages;
using OpenQA.Selenium;
using Xunit;

namespace HospitalTest.End2EndTests
{
    public class SearchExeminationE2ETest
    {
        private readonly IWebDriver _webDriver;
        private readonly LoginPage _loginPage;
        private readonly SearchExaminationPage _searchExaminationPage;

        public SearchExeminationE2ETest()
        {
            var browserOptions = new BrowserOptions();
            _webDriver = browserOptions.CreateChromeDriver();
            _loginPage = new LoginPage(_webDriver);
            _searchExaminationPage = new SearchExaminationPage(_webDriver);
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
        public void Search_exemination_sucsess()
        {
            Login();
        }
    }
}