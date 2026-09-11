using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Imigram.Tests
{
    public class RegisterTests
    {

        [Fact]
        public void ValidRegistration()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://localhost:4200/register");
           

            string uniqueId = DateTime.Now.ToString("yyyyMMddHHmmssfff");

            driver.FindElement(By.Id("username"))
                .SendKeys($"testuser_{uniqueId}");

            driver.FindElement(By.Id("email"))
                .SendKeys($"test_{uniqueId}@gmail.com");

            driver.FindElement(By.Id("password"))
                .SendKeys("Test123456");

            driver.FindElement(By.Id("confirm-password"))
                .SendKeys("Test123456");

            driver.FindElement(By.Id("firstName"))
                .SendKeys("Test");

            driver.FindElement(By.Id("lastName"))
                .SendKeys("User");

            driver.FindElement(By.Id("register-button")).Click();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(d => d.Url.Contains("/login"));

            Assert.True(driver.Url.Contains("/login"));
            driver.Quit();
            driver.Dispose();

        }
        [Fact]
        public void InvalidUsernameRegistration()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://localhost:4200/register");

            string uniqueId = DateTime.Now.ToString("yyyyMMddHHmmssfff");

            driver.FindElement(By.Id("username"))
                .SendKeys($"Z+");

            driver.FindElement(By.Id("email"))
                .SendKeys($"test_{uniqueId}@gmail.com");

            driver.FindElement(By.Id("password"))
                .SendKeys("Test123456");

            driver.FindElement(By.Id("confirm-password"))
                .SendKeys("Test123456");

            driver.FindElement(By.Id("firstName"))
                .SendKeys("Test");

            driver.FindElement(By.Id("lastName"))
                .SendKeys("User");

            driver.FindElement(By.Id("register-button")).Click();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

            var error_msg = wait.Until(d => d.FindElement(By.ClassName("error-message")));

            Assert.True(error_msg.Displayed);
            Assert.Equal("Korisničko ime mora da ima barem 4 karaktera i sme da sadrži samo slova, brojeve i _.", error_msg.Text);
            driver.Quit();
            driver.Dispose();
        }
        [Fact]
        public void InvalidEmailRegistration()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://localhost:4200/register");


            string uniqueId = DateTime.Now.ToString("yyyyMMddHHmmssfff");

            driver.FindElement(By.Id("username"))
                .SendKeys($"testuser_{uniqueId}");

            driver.FindElement(By.Id("email"))
                .SendKeys($"erw dfsrew");

            driver.FindElement(By.Id("password"))
                .SendKeys("Test123456");

            driver.FindElement(By.Id("confirm-password"))
                .SendKeys("Test123456");

            driver.FindElement(By.Id("firstName"))
                .SendKeys("Test");

            driver.FindElement(By.Id("lastName"))
                .SendKeys("User");

            driver.FindElement(By.Id("register-button")).Click();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

            var error_msg = wait.Until(d => d.FindElement(By.ClassName("error-message")));

            Assert.True(error_msg.Displayed);
            Assert.Equal("Email nije validnog formata.", error_msg.Text);

            driver.Quit();
            driver.Dispose();
        }
        [Fact]
        public void PasswordDontMatchRegistration()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://localhost:4200/register");


            string uniqueId = DateTime.Now.ToString("yyyyMMddHHmmssfff");

            driver.FindElement(By.Id("username"))
                .SendKeys($"testuser_{uniqueId}");

            driver.FindElement(By.Id("email"))
                .SendKeys($"test_{uniqueId}@gmail.com");

            driver.FindElement(By.Id("password"))
                .SendKeys("Test123456");

            driver.FindElement(By.Id("confirm-password"))
                .SendKeys("Test12346");

            driver.FindElement(By.Id("firstName"))
                .SendKeys("Test");

            driver.FindElement(By.Id("lastName"))
                .SendKeys("User");

            driver.FindElement(By.Id("register-button")).Click();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

            var error_msg = wait.Until(d => d.FindElement(By.ClassName("error-message")));

            Assert.True(error_msg.Displayed);
            Assert.Equal("Šifre moraju da se poklapaju!", error_msg.Text);

            driver.Quit();
            driver.Dispose();
        }
        [Fact]
        public void UsernameTakenRegistration()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://localhost:4200/register");


            string uniqueId = DateTime.Now.ToString("yyyyMMddHHmmssfff");

            driver.FindElement(By.Id("username"))
                .SendKeys($"test1");

            driver.FindElement(By.Id("email"))
                .SendKeys($"test_{uniqueId}@gmail.com");

            driver.FindElement(By.Id("password"))
                .SendKeys("Test123456");

            driver.FindElement(By.Id("confirm-password"))
                .SendKeys("Test123456");

            driver.FindElement(By.Id("firstName"))
                .SendKeys("Test");

            driver.FindElement(By.Id("lastName"))
                .SendKeys("User");

            driver.FindElement(By.Id("register-button")).Click();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

            var error_msg = wait.Until(d => d.FindElement(By.ClassName("error-message")));

            Assert.True(error_msg.Displayed);
            Assert.Equal("Korisničko ime je zauzeto.", error_msg.Text);

            driver.Quit();
            driver.Dispose();
        }
        [Fact]
        public void EmailTakenRegistration()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://localhost:4200/register");


            string uniqueId = DateTime.Now.ToString("yyyyMMddHHmmssfff");

            driver.FindElement(By.Id("username"))
                .SendKeys($"testuser_{uniqueId}");

            driver.FindElement(By.Id("email"))
                .SendKeys($"test6@gmail.com");

            driver.FindElement(By.Id("password"))
                .SendKeys("Test123456");

            driver.FindElement(By.Id("confirm-password"))
                .SendKeys("Test123456");

            driver.FindElement(By.Id("firstName"))
                .SendKeys("Test");

            driver.FindElement(By.Id("lastName"))
                .SendKeys("User");

            driver.FindElement(By.Id("register-button")).Click();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

            var error_msg = wait.Until(d => d.FindElement(By.ClassName("error-message")));

            Assert.True(error_msg.Displayed);
            Assert.Equal("Email je zauzet.", error_msg.Text);
            driver.Quit();
            driver.Dispose();
        }
    }
}
