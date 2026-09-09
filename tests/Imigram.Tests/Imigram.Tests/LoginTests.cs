using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Imigram.Tests
{
    public class LoginTests
    {
        [Fact]
        public void LoginWithValidCredentials()
        {
            using IWebDriver driver = new ChromeDriver();
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

            driver.Navigate().GoToUrl("http://localhost:4200/login");

            driver.FindElement(By.Id("username")).SendKeys("test1");

            driver.FindElement(By.Id("password")).SendKeys("123456");

            driver.FindElement(By.Id("login-button")).Click();

            wait.Until(d => d.Url.Contains("/home"));

            Assert.Contains("/home", driver.Url);
        }
        [Fact]
        public void LoginWithInvalidUsername()
        {
            using IWebDriver driver = new ChromeDriver();
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

            driver.Navigate().GoToUrl("http://localhost:4200/login");

            driver.FindElement(By.Id("username")).SendKeys("test1qqqq");

            driver.FindElement(By.Id("password")).SendKeys("123456");

            driver.FindElement(By.Id("login-button")).Click();


            var error_msg = wait.Until(d => d.FindElement(By.ClassName("error-message")));

            Assert.True(error_msg.Displayed);
            
        }
        [Fact]
        public void LoginWithInvalidPassword()
        {
            using IWebDriver driver = new ChromeDriver();
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

            driver.Navigate().GoToUrl("http://localhost:4200/login");

            driver.FindElement(By.Id("username")).SendKeys("test");

            driver.FindElement(By.Id("password")).SendKeys("1234567890");

            driver.FindElement(By.Id("login-button")).Click();


            var error_msg = wait.Until(d => d.FindElement(By.ClassName("error-message")));

            Assert.True(error_msg.Displayed);
        }
        [Fact]
        public void LoginWithInvalidUsernameAndPassword()
        {
            using IWebDriver driver = new ChromeDriver();
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

            driver.Navigate().GoToUrl("http://localhost:4200/login");

            driver.FindElement(By.Id("username")).SendKeys("tesqweqt1qqqq");

            driver.FindElement(By.Id("password")).SendKeys("qwewq36579009-9-0=0989087098");

            driver.FindElement(By.Id("login-button")).Click();

            var error_msg = wait.Until(d => d.FindElement(By.ClassName("error-message")));

            Assert.True(error_msg.Displayed);
        }
    }
}
