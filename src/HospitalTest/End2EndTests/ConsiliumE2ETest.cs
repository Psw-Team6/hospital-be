using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;
using HospitalTest.End2EndCommon;
using HospitalTest.HospitalizationTest.Pages;
using Xunit;


namespace HospitalTest.End2EndTests
{
    public class ConsiliumE2ETest
    {
        public readonly IWebDriver driver;
        private readonly ScheduleConsiliumPage _scheduleConsiliumPage;

        public ConsiliumE2ETest()
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

            var loginPage = new DoctorLoginPage(driver);
            loginPage.Navigate();
            Assert.True(loginPage.LoginButtonDisplayed());
            Assert.True(loginPage.UsernameInputDisplayed());
            Assert.True(loginPage.PasswordInputDisplayed());

            loginPage.InsertUsername("Ilija");
            loginPage.InsertPassword("123");
            loginPage.SubmitForm();
            loginPage.WaitForFormSubmit();

            var dashboardPagePage = new DashboardPage(driver);
            Assert.True(dashboardPagePage.consiliumNavDisplayed());
            dashboardPagePage.consiliumNavClick();
            dashboardPagePage.WaitForConsiliumNavigate();

            var consiliumPage = new ConsiliumPage(driver);
            Assert.True(consiliumPage.ScheduleConsiliumDisplayed());
            consiliumPage.schecduleConsiliumBtnClick();
            consiliumPage.WaitForBtnScheduleConsilium();
            
            _scheduleConsiliumPage = new ScheduleConsiliumPage(driver);
            Assert.True(_scheduleConsiliumPage.ThemeInputDisplayed());
            Assert.True(_scheduleConsiliumPage.DurationInputDisplayed());
            Assert.True(_scheduleConsiliumPage.StartDateInputDisplayed());
            Assert.True(_scheduleConsiliumPage.EndDateInputDisplayed());
        }

        
        [Fact]
        public void schedule_consilium_doctors_successfuly()
        {
            _scheduleConsiliumPage.InsertTheme("Test");
            _scheduleConsiliumPage.InsertDuration("45");
            _scheduleConsiliumPage.InsertStartDate("12/17/2022");
            _scheduleConsiliumPage.InsertEndDate("12/17/2022");
            _scheduleConsiliumPage.DoctorSubmit();
            Thread.Sleep(1000);
            Assert.True(_scheduleConsiliumPage.IsMultiVisible());
            _scheduleConsiliumPage.MultiSelectDocClick();
            _scheduleConsiliumPage.Select();
            Thread.Sleep(1000);
            _scheduleConsiliumPage.Esc();
            _scheduleConsiliumPage.SubmitForm();
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 20));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.UrlToBe(ScheduleConsiliumPage.UriDashboard));
            Assert.Equal(ScheduleConsiliumPage.UriDashboard,driver.Url);
            driver.Close();
        }
        [Fact]
        public void schedule_consilium_doctors_not_valid_range()
        {
            _scheduleConsiliumPage.InsertTheme("Test");
            _scheduleConsiliumPage.InsertDuration("45");
            _scheduleConsiliumPage.InsertStartDate("12/15/2022");
            _scheduleConsiliumPage.InsertEndDate("12/15/2022");
            _scheduleConsiliumPage.DoctorSubmit();
            Thread.Sleep(1000);
            Assert.True(_scheduleConsiliumPage.IsMultiVisible());
            _scheduleConsiliumPage.MultiSelectDocClick();
            _scheduleConsiliumPage.Select();
            Thread.Sleep(1000);
            _scheduleConsiliumPage.Esc();
            _scheduleConsiliumPage.SubmitForm();
            Assert.NotEqual(ScheduleConsiliumPage.UriDashboard,driver.Url);
            driver.Close();
        }
        [Fact]
        public void schedule_consilium_doctors_not_valid_start_date()
        {
            _scheduleConsiliumPage.InsertTheme("Test");
            _scheduleConsiliumPage.InsertDuration("45");
            _scheduleConsiliumPage.InsertStartDate("12/15/2022");
            _scheduleConsiliumPage.InsertEndDate("12/15/2022");
            _scheduleConsiliumPage.DoctorSubmit();
            Thread.Sleep(2000);
            Assert.True(_scheduleConsiliumPage.IsMultiVisible());
            _scheduleConsiliumPage.MultiSelectDocClick();
            _scheduleConsiliumPage.Select();
            Thread.Sleep(1000);
            _scheduleConsiliumPage.Esc();
            _scheduleConsiliumPage.SubmitForm();
            Assert.NotEqual(ScheduleConsiliumPage.UriDashboard,driver.Url);
            driver.Close();
        }
        [Fact]
        public void schedule_consilium_doctors_not_valid_end_date()
        {
            _scheduleConsiliumPage.InsertTheme("Test");
            _scheduleConsiliumPage.InsertDuration("45");
            _scheduleConsiliumPage.InsertStartDate("12/17/2022");
            _scheduleConsiliumPage.InsertEndDate("12/13/2022");
            _scheduleConsiliumPage.DoctorSubmit();
            Thread.Sleep(1000);
            Assert.True(_scheduleConsiliumPage.IsMultiVisible());
            _scheduleConsiliumPage.MultiSelectDocClick();
            _scheduleConsiliumPage.Select();
            Thread.Sleep(1000);
            _scheduleConsiliumPage.Esc();
            _scheduleConsiliumPage.SubmitForm();
            Assert.NotEqual(ScheduleConsiliumPage.UriDashboard,driver.Url);
            driver.Close();
        }
        [Fact]
        public void schedule_consilium_specialization_success()
        {
            _scheduleConsiliumPage.InsertTheme("Test");
            _scheduleConsiliumPage.InsertDuration("45");
            _scheduleConsiliumPage.InsertStartDate("12/20/2022");
            _scheduleConsiliumPage.InsertEndDate("12/22/2022");
            _scheduleConsiliumPage.SpecializationSubmit();
            Thread.Sleep(1000);
            Assert.True(_scheduleConsiliumPage.IsMultiVisibleSpec());
            _scheduleConsiliumPage.MultiSelectSpecClick();
            _scheduleConsiliumPage.Select();
            Thread.Sleep(1000);
            _scheduleConsiliumPage.EscSpec();
            _scheduleConsiliumPage.SubmitForm();
            Thread.Sleep(1000);
            Assert.Equal(ScheduleConsiliumPage.UriDashboard,driver.Url);
            driver.Close();
        }
        [Fact]
        public void schedule_consilium_specialization_not_valid()
        {
            _scheduleConsiliumPage.InsertTheme("Test");
            _scheduleConsiliumPage.InsertDuration("45");
            _scheduleConsiliumPage.InsertStartDate("12/13/2022");
            _scheduleConsiliumPage.InsertEndDate("12/13/2022");
            _scheduleConsiliumPage.SpecializationSubmit();
            Thread.Sleep(1000);
            Assert.True(_scheduleConsiliumPage.IsMultiVisibleSpec());
            _scheduleConsiliumPage.MultiSelectSpecClick();
            _scheduleConsiliumPage.Select();
            Thread.Sleep(1000);
            _scheduleConsiliumPage.EscSpec();
            _scheduleConsiliumPage.SubmitForm();
            Assert.NotEqual(ScheduleConsiliumPage.UriDashboard,driver.Url);
            driver.Close();
        }
        
    }
}