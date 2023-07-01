using AventStack.ExtentReports;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SpecFlowJiraIntegration.Core.Report;
using SpecFlowJiraIntegration.Core.WebElement;
using SpecFlowJiraIntegration.Helpers;
using SpecFlowJiraIntegration.Steps;
using System;
using System.IO;

namespace SpecFlowJiraIntegration.Core.Drivers
{
    public class DriverUtils
    {
        public static void GoToUrl(string url)
        {
            BrowserFactory.GetWebDriver().Url = url;
            HtmlReporter.Node.Pass("Open URL: " + url);
        }

        public static void WaitForPageLoadCompletely()
        {
            var wait = new WebDriverWait(BrowserFactory.GetWebDriver(), TimeSpan.FromSeconds(WebObjectExtension.GetWaitTimeoutSeconds()));
            wait.Until(driver1 => ((IJavaScriptExecutor)BrowserFactory.GetWebDriver()).ExecuteScript("return document.readyState").Equals("complete"));
        }

        public static string CaptureScreenshot(IWebDriver driver, string className, string testName)
        {
            ITakesScreenshot ts = (ITakesScreenshot)driver;
            Screenshot screenshot = ts.GetScreenshot();
            var screenshotDirectory = Path.Combine(Directory.GetCurrentDirectory(), ConfigurationHelper.GetConfigurationByKey(Hooks.Config, "Screenshot.Folder"), className);
            testName = testName.Replace("\"", "");
            var fileName = string.Format(@"Screenshot_{0}_{1}", testName, DateTime.Now.ToString("yyyyMMdd_HHmmssff"));
            Directory.CreateDirectory(screenshotDirectory);
            var fileLocation = string.Format(@"{0}\{1}.png", screenshotDirectory, fileName);
            screenshot.SaveAsFile(fileLocation, ScreenshotImageFormat.Png);

            return fileLocation;
        }

        public static MediaEntityModelProvider CaptureScreenShotAndAttachToExtendReport(IWebDriver driver, String screenShotName)
        {
            ITakesScreenshot ts = (ITakesScreenshot)driver;
            var screenshot = ts.GetScreenshot().AsBase64EncodedString;

            return MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot, screenShotName).Build();
        }
    }
}
