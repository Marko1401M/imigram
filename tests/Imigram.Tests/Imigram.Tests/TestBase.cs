using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Imigram.Tests
{
    internal class TestBase
    {
        protected IWebDriver Driver;
        protected WebDriverWait Wait;

        public TestBase()
        {
            Driver = new ChromeDriver();
            Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
        }

        private void Login()
        {
            Driver.Navigate().GoToUrl("http://localhost:4200/login");
            Driver.FindElement(By.Id("username")).SendKeys("test1");
            Driver.FindElement(By.Id("password")).SendKeys("123456");
            Driver.FindElement(By.Id("login-button")).Click();

            Wait.Until(d => d.Url.Contains("/home"));
        }
    }
}
