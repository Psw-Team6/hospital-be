using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using HospitalTest.End2EndCommon;
using HospitalTest.HospitalizationTest.Pages;
using Xunit;


namespace HospitalTest.End2EndTests
{
    public class ConsiliumE2ETest
    {
        public readonly IWebDriver driver;
        private DashboardPage dashboardPagePage;
        private DoctorLoginPage loginPage;
        private ConsiliumPage _consiliumPage;
        private ScheduleConsiliumPage scheduleConsiliumPage;

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

            loginPage = new DoctorLoginPage(driver);
            loginPage.Navigate();
            Assert.True(loginPage.LoginButtonDisplayed());
            Assert.True(loginPage.UsernameInputDisplayed());
            Assert.True(loginPage.PasswordInputDisplayed());

            loginPage.InsertUsername("Ilija");
            loginPage.InsertPassword("123");
            loginPage.SubmitForm();
            loginPage.WaitForFormSubmit();

            dashboardPagePage = new DashboardPage(driver);
            Assert.True(dashboardPagePage.consiliumNavDisplayed());
            dashboardPagePage.consiliumNavClick();
            dashboardPagePage.WaitForConsiliumNavigate();

            _consiliumPage = new ConsiliumPage(driver);
            Assert.True(_consiliumPage.ScheduleConsiliumDisplayed());
            _consiliumPage.schecduleConsiliumBtnClick();
            _consiliumPage.WaitForBtnScheduleConsilium();
            
            scheduleConsiliumPage = new ScheduleConsiliumPage(driver);
            Assert.True(scheduleConsiliumPage.ThemeInputDisplayed());
            Assert.True(scheduleConsiliumPage.DurationInputDisplayed());
            Assert.True(scheduleConsiliumPage.StartDateInputDisplayed());
            Assert.True(scheduleConsiliumPage.EndDateInputDisplayed());
        }

        
        [Fact]
        public void schedule_consilium_doctors_successfuly()
        {
            scheduleConsiliumPage.InsertTheme("Proooba");
            scheduleConsiliumPage.InsertDuration("45");
            scheduleConsiliumPage.InsertStartDate("12/17/2022");
            scheduleConsiliumPage.InsertEndDate("12/17/2022");
            scheduleConsiliumPage.DoctorSubmit();
            
            //promeniti id polja, selektovati doktora i kliknuti submit
            
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 20));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.UrlToBe("http://localhost:4200/consiliums"));
            Assert.NotEqual(driver.Url,"http://localhost:4200/consiliums");
        }
        
    }
}