using OpenQA.Selenium;
using System;
using System.Threading;

namespace SpecFlowJiraIntegration.Core.Drivers
{
    public class BrowserFactory
    {
        public static ThreadLocal<IWebDriver> ThreadLocalWebDriver = new ThreadLocal<IWebDriver>();

        public static void InitializeDriver(string browserName)
        {
            IDriverSetup driverSetup = browserName.ToLower() switch
            {
                "chrome" => new ChromeDriverSetup(),
                "firefox" => new FirefoxDriverSetup(),
                _ => throw new ArgumentOutOfRangeException(browserName),
            };

            ThreadLocalWebDriver.Value = driverSetup.CreateInstance();
        }

        public static IWebDriver GetWebDriver()
        {
            return ThreadLocalWebDriver.Value;
        }

        public static void CleanUp()
        {
            if (GetWebDriver() != null)
            {
                GetWebDriver().Quit();
                GetWebDriver().Dispose();
            }
        }
    }
}
