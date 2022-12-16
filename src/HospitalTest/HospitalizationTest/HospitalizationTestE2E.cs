using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;
using Xunit;

namespace HospitalTest.HospitalizationTest
{
    public class HospitalizationTestE2E
    {
        public readonly IWebDriver driver;
        private Pages.DoctorLoginPage loginPage;
        private Pages.DashboardPage dashboardPagePage;
        private Pages.PatientHospitalizationPage hospitalizationPage;
        private Pages.HospitalisedPatientPage hospitalisedPatientPage;

        public HospitalizationTestE2E()
        {

            ChromeOptions options = new ChromeOptions();
            options.AddArguments("start-maximized");            // open Browser in maximized mode
            options.AddArguments("disable-infobars");           // disabling infobars
            options.AddArguments("--disable-extensions");       // disabling extensions
            options.AddArguments("--disable-gpu");              // applicable to windows os only
            options.AddArguments("--disable-dev-shm-usage");    // overcome limited resource problems
            options.AddArguments("--no-sandbox");               // Bypass OS security model
            options.AddArguments("--disable-notifications");    // disable notifications
            

            driver = new ChromeDriver(options);

            loginPage = new Pages.DoctorLoginPage(driver);
            loginPage.Navigate();
            Assert.True(loginPage.LoginButtonDisplayed());
            Assert.True(loginPage.UsernameInputDisplayed());
            Assert.True(loginPage.PasswordInputDisplayed());

            loginPage.InsertUsername("Ilija");
            loginPage.InsertPassword("123");
            loginPage.SubmitForm();
            loginPage.WaitForFormSubmit();

            dashboardPagePage = new Pages.DashboardPage(driver);
            Assert.True(dashboardPagePage.hospitalizationNavDisplayed());
            dashboardPagePage.hospitalizationNavClick();
            dashboardPagePage.WaitForFormSubmit();

            hospitalizationPage = new Pages.PatientHospitalizationPage(driver);
            Assert.True(hospitalizationPage.PatientInputDisplayed());
            Assert.True(hospitalizationPage.ReasonInputDisplayed());
            Assert.True(hospitalizationPage.SubmitButtonDisplayed());
            
        }

        [Fact]
        public void patient_is_successfuly_hospitalized()
        {
            hospitalizationPage.InsertPatient("mat-select-0","mat-option-0");
            hospitalizationPage.InsertReason("probaa");
            hospitalizationPage.SubmitForm();
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 20));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.UrlToBe("http://localhost:4200/hospitalizes-patients"));
            var p = driver.Url;
            Assert.Equal(driver.Url,Pages.HospitalisedPatientPage.URI);
        }
        
        [Fact]
        public void patient_is_not_successfuly_hospitalized()
        {
            hospitalizationPage.InsertReason("probaa");
            hospitalizationPage.SubmitForm();
            Assert.NotEqual(driver.Url,Pages.HospitalisedPatientPage.URI);
        }
        
        [Fact]
        public void reason_is_not_entered()
        {
            hospitalizationPage.InsertPatient("mat-select-0","mat-option-0");
            hospitalizationPage.SubmitForm();
            Assert.NotEqual(driver.Url,Pages.HospitalisedPatientPage.URI);
        }
    }
}