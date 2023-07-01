using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using SpecFlowJiraIntegration.Core.Drivers;
using SpecFlowJiraIntegration.Core.Report;
using SpecFlowJiraIntegration.Helpers;
using SpecFlowJiraIntegration.Steps;
using System;

namespace SpecFlowJiraIntegration.Core.WebElement
{
    public static class WebObjectExtension
    {
        public static int GetWaitTimeoutSeconds()
        {
            return int.Parse(ConfigurationHelper.GetConfigurationByKey(Hooks.Config, "Timeout.Webdriver.Wait.Seconds"));
        }

        //Wait Element 
        public static IWebElement WaitForElementToBeVisible(this WebObject webObject)
        {
            try
            {
                var wait = new WebDriverWait(BrowserFactory.GetWebDriver(), TimeSpan.FromSeconds(GetWaitTimeoutSeconds()));
                return wait.Until(ExpectedConditions.ElementIsVisible(webObject.By));
            }
            catch (WebDriverTimeoutException exception)
            {
                var message = $"Element is not visible as expected. Element information: {webObject.Name}";
                HtmlReporter.Node.Fail(message);
                throw exception;
            }
        }

        public static IWebElement WaitForElementToBeClickable(this WebObject webObject)
        {
            try
            {
                var wait = new WebDriverWait(BrowserFactory.GetWebDriver(), TimeSpan.FromSeconds(GetWaitTimeoutSeconds()));
                return wait.Until(ExpectedConditions.ElementIsVisible(webObject.By));
            }
            catch (WebDriverTimeoutException exception)
            {
                var message = $"Element is not clickable as expected. Element information: {webObject.Name}";
                HtmlReporter.Node.Fail(message);
                throw exception;
            }
        }

        public static void WaitForElementToBeInvisible(this WebObject wobject)
        {
            try
            {
                var wait = new WebDriverWait(BrowserFactory.GetWebDriver(), TimeSpan.FromSeconds(GetWaitTimeoutSeconds()));
                wait.Until(ExpectedConditions.InvisibilityOfElementLocated(wobject.By));
            }
            catch (WebDriverTimeoutException)
            {
                var message = $"Element is still visible. Element information: {wobject.Name}";
                Console.WriteLine(message);
                HtmlReporter.Node.Pass(message);
            }
        }

        public static bool IsElementDisplayed(this WebObject webObject)
        {
            bool result;
            var wait = new WebDriverWait(BrowserFactory.GetWebDriver(), TimeSpan.FromSeconds(GetWaitTimeoutSeconds()));
            try
            {
                result = wait.Until(ExpectedConditions.ElementIsVisible(webObject.By)).Displayed;
                Console.WriteLine(webObject.Name + " is displayed as expected");
                HtmlReporter.Node.Pass(webObject.Name + " is displayed as expected");
            }
            catch (WebDriverTimeoutException)
            {
                result = false;
                Console.WriteLine(webObject.Name + " is not displayed as expected");
                HtmlReporter.Node.Pass(webObject.Name + " is not displayed as expected");
            }
            return result;
        }

        public static string GetTextFromElement(this WebObject webObject)
        {
            try
            {
                var element = webObject.WaitForElementToBeVisible();
                Console.WriteLine("Get text from " + webObject.Name);
                HtmlReporter.Node.Pass("Get text from " + webObject.Name);
                return element.Text;
            }
            catch (WebDriverException exception)
            {
                var message = $"An error happens when trying to get text from element. Element information: {webObject.Name}";
                HtmlReporter.Node.Fail(message);
                throw exception;
            }
        }

        //Action on Element
        public static void ClickOnElement(this WebObject webObject)
        {
            try
            {
                var element = webObject.WaitForElementToBeClickable();
                element.Click();
                Console.WriteLine("Click on " + webObject.Name);
                HtmlReporter.Node.Pass("Click on " + webObject.Name);
            }
            catch (WebDriverException exception)
            {
                var message = $"An error happens when trying to click on an element. Element information: {webObject.Name}";
                HtmlReporter.Node.Fail(message);
                throw exception;
            }
        }

        public static void EnterText(this WebObject webObject, string text)
        {
            try
            {
                var element = webObject.WaitForElementToBeVisible();
                element.Clear();
                element.SendKeys(text);
                Console.WriteLine(text + " is entered in the " + webObject.Name + " field.");
                HtmlReporter.Node.Pass(text + " is entered in the " + webObject.Name + " field.");
            }
            catch (WebDriverException exception)
            {
                var message = $"An error happens when trying to enter text to a field. Element information: {webObject.Name}";
                HtmlReporter.Node.Fail(message);
                throw exception;
            }
        }

        public static void MoveToElement(this WebObject webObject)
        {
            try
            {
                var element = webObject.WaitForElementToBeClickable();
                new Actions(BrowserFactory.GetWebDriver())
                   .MoveToElement(element)
                   .Perform();
                Console.WriteLine("Scroll to " + webObject.Name);
                HtmlReporter.Node.Pass("Scroll to " + webObject.Name);
            }
            catch (WebDriverException exception)
            {
                var message = $"An error happens when trying to move to an element. Element information: {webObject.Name}";
                HtmlReporter.Node.Fail(message);
                throw exception;
            }
        }
    }
}