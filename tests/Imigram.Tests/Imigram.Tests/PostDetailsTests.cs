using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Xml.Linq;

namespace Imigram.Tests
{
    public class PostDetailsTests : TestBase
    {

        [Fact]
        public void LikePost()
        {
            Login();

            Driver.Navigate().GoToUrl("http://localhost:4200/post-details/6a9ee8b6505279c8b9079d5b");
            Wait.Until(d => d.Url.Contains("/post-details"));

            var likeButton = Wait.Until(d => d.FindElement(By.XPath("/html/body/app-root/div/app-post-details/div/app-post-card/div/div[4]/div[2]/button[1]")));
            Console.WriteLine(likeButton.ToString());
            var likeCount = Driver.FindElement(By.ClassName("like-count"));

            var initialLikeCount = BigInteger.Parse(likeCount.Text);

            likeButton.Click();
            Thread.Sleep(1000);
            var currentLikeText = Driver.FindElement(By.ClassName("like-count"));

            var currentLikeCount = BigInteger.Parse(currentLikeText.Text);

            Assert.Equal(initialLikeCount + 1, currentLikeCount);

            Driver.Quit();
            Driver.Dispose();
        }
        [Fact]
        public void CommentPost()
        {
            Login();
            Driver.Navigate().GoToUrl("http://localhost:4200/post-details/6a9ee8b6505279c8b9079d5b");
            Wait.Until(d => d.Url.Contains("/post-details"));

            var commentInput = Wait.Until(d => d.FindElement(By.TagName("textarea")));

            var newCommentText = "Novi komentar!";

            commentInput.SendKeys(newCommentText);

            var addCommentButton = Driver.FindElement(By.Id("add-comment-button"));
            addCommentButton.Click();
            Thread.Sleep(1000);
            var comments = Wait.Until(d => d.FindElements(By.ClassName("comment")));
            ((IJavaScriptExecutor)Driver).ExecuteScript(
                "arguments[0].scrollIntoView({block: 'center'});",
                comments.Last()
            );

            var latestComment = comments.Last();
            var latestCommentText = latestComment.FindElement(By.TagName("p")).Text;
            Assert.Equal(newCommentText, latestCommentText);

            Driver.Quit();
            Driver.Dispose();
        }
    }
}
