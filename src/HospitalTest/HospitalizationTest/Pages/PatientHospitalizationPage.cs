using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace HospitalTest.HospitalizationTest.Pages
{
    public class PatientHospitalizationPage
    {
        private readonly IWebDriver driver;
        public const string URI = "http://localhost:4200/patients/hospitalization";

        private IWebElement patientInput => driver.FindElement(By.Id("mat-select-0"));
        private IWebElement reasonInput => driver.FindElement(By.Id("mat-input-0"));
        private IWebElement submitButton => driver.FindElement(By.XPath("/html/body/app-root/app-body/div/app-patient-hospitalization/div/div/div/form/button/span[1]"));
        private IWebElement toast => driver.FindElement(By.XPath("/html/body/app-root/lib-ng-toast/div/div[2]/p[2]"));
        //private IWebElement toastError => driver.FindElement(By.XPath("/html/body/app-root/lib-ng-toast/div/div[2]/p[2]"));


        public PatientHospitalizationPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public bool PatientInputDisplayed()
        {
            return patientInput.Displayed;
        }
        public bool ReasonInputDisplayed()
        {
            return reasonInput.Displayed;
        }
        public bool SubmitButtonDisplayed()
        {
            return submitButton.Displayed;
        }
        public void InsertPatient(string id, string value)
        {
            driver.FindElement(By.Id(id)).Click();
            driver.FindElement(By.Id(value)).Click();
        }
        public void InsertReason(string amount)
        {
            reasonInput.SendKeys(amount);
        }
        public void SubmitForm()
        {
            submitButton.Click();
        }
        public string getResponseText()
        {
            var p = toast.Text;
            return p;
        }
    }
}