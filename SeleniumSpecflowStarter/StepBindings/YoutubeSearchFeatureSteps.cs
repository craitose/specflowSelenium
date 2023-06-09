using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using TechTalk.SpecFlow;

namespace SeleniumSpecflowStarter.StepBindings
{
    [Binding]
    public class YoutubeSearchFeatureSteps : IDisposable
    {
        private String searchKeyword;

        private ChromeDriver chromeDriver;

        public YoutubeSearchFeatureSteps()
        {
            chromeDriver = new ChromeDriver();
        }

        [When(@"I press the search button")]
        public void WhenIPressTheSearchButton()
        {
            var searchButton = chromeDriver.FindElementByCssSelector("button#search-icon-legacy");
            searchButton.Click();
        }


        [Given(@"I have navigated to the youtube website")]
        public void GivenIHaveNavigatedToTheYoutubeWebsite()
        {
            chromeDriver.Navigate().GoToUrl("https://www.youtube.com");
            Assert.IsTrue(chromeDriver.Title.ToLower().Contains("youtube"));
            System.Threading.Thread.Sleep(3000);
            var RejectCookie = chromeDriver.FindElementByXPath("//*[text()='Reject all']");
            IJavaScriptExecutor executor = (IJavaScriptExecutor)chromeDriver;
            executor.ExecuteScript("arguments[0].click();", RejectCookie);
            
            
            System.Threading.Thread.Sleep(4000);
            
        }

        [Given(@"I have entered (.*) as search keyword")]
        public void GivenIHaveEnteredLondonAsSearchKeyword(String searchString)
        {
            this.searchKeyword = searchString.ToLower();
            var searchInputBox = chromeDriver.FindElementByXPath("//*[@name='search_query']");
           
            var wait = new WebDriverWait(chromeDriver, TimeSpan.FromSeconds(2));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@name='search_query']")));
            
            searchInputBox.SendKeys(searchKeyword);
            
        }


        [Then(@"I should be redirected to search results")]
        public void ThenIShouldBeRedirectedToSearchResults()
        {
            System.Threading.Thread.Sleep(2000);
            // After search is complete the keyword should be present in the url as well as page title
            Assert.IsTrue(chromeDriver.Url.ToLower().Contains(searchKeyword));
            Assert.IsTrue(chromeDriver.Title.ToLower().Contains(searchKeyword));
            
        }

        public void Dispose()
        {

            if (chromeDriver != null)
            {
                
                chromeDriver.Quit();
                chromeDriver = null;
            }
        }

        
    }
}
