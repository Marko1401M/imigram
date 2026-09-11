using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Imigram.Tests
{
    public class TestBase
    {
        protected IWebDriver Driver;
        protected WebDriverWait Wait;

        public TestBase()
        {
            ChromeOptions options = new ChromeOptions();

            options.AddUserProfilePreference("credentials_enable_service", false);
            options.AddUserProfilePreference("profile.password_manager_leak_detection", false);

            Driver = new ChromeDriver(options);
            Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
        }

        protected void Login()
        {
            Driver.Navigate().GoToUrl("http://localhost:4200/login");
            Driver.FindElement(By.Id("username")).SendKeys("test1");
            Driver.FindElement(By.Id("password")).SendKeys("123456");
            Driver.FindElement(By.Id("login-button")).Click();

            Wait.Until(d => d.Url.Contains("/home"));
        }
        private void Dispose()
        {
            this.Driver.Quit();
            this.Driver.Dispose();
        }
    }
}
