using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace HospitalTest.End2EndCommon
{
    public class BrowserOptions:IDisposable
    {
        private  IWebDriver _driver;

        public IWebDriver CreateChromeDriver()
        {
            var options = new ChromeOptions();
            options.AddArguments("start-maximized");            // open Browser in maximized mode
            options.AddArguments("disable-infobars");           // disabling infobars
            options.AddArguments("--disable-extensions");       // disabling extensions
            options.AddArguments("--disable-gpu");              // applicable to windows os only
            options.AddArguments("--disable-dev-shm-usage");    // overcome limited resource problems
            options.AddArguments("--no-sandbox");               // Bypass OS security model
            options.AddArguments("--disable-notifications");    // disable notifications
            _driver = new ChromeDriver(options);
            return _driver;
        }

        public void Dispose()
        {
            _driver.Quit();
            _driver?.Dispose();
        }
    }
}