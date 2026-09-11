using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Imigram.Tests
{
    public class SearchTests : TestBase
    {
        public SearchTests()
        {
            
        }
        [Fact]
        public void ValidSearch()
        {
            Login();

            Driver.Navigate().GoToUrl("http://localhost:4200/search");
            Wait.Until(d => d.Url.Contains("/search"));

            var query = "Marko";
            var searchBtn = Wait.Until(d => d.FindElement(By.Id("search")));
            searchBtn.SendKeys(query);

            var elements = Wait.Until(d => d.FindElements(By.ClassName("user-card")));
            Console.WriteLine(elements.Count);
            foreach(var element in elements)
            {
                var info = element.FindElement(By.ClassName("user-info"));
                var username = info.FindElement(By.TagName("h3"));
                var name = info.FindElement(By.TagName("p"));
                Assert.True(username.Text.Contains(query) || name.Text.Contains(query));
            }

            
        }
    }
}
