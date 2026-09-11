using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace Imigram.Tests
{
    public class CreatePostTests: TestBase
    {
        [Fact]
        public void CreatePost()
        {
            string content = "Testiram Kreiranje nove objave.";
            string location = "Test environment";

            Login();
            Driver.Navigate().GoToUrl("http://localhost:4200/create-post");
            Wait.Until(d => d.Url.Contains("/create-post"));

            var contentInput = Driver.FindElement(By.Id("content-input"));

            var locationInput = Driver.FindElement(By.Id("location-input"));

            var createPostButton = Driver.FindElement(By.Id("create-post-button"));

            contentInput.SendKeys(content);
            locationInput.SendKeys(location);

            createPostButton.Click();

            Wait.Until(d => d.Url.Contains("/post-details"));

            Assert.True(Driver.Url.Contains("/post-details"));

            Driver.Quit();
            Driver.Dispose();
        }
    }
}
