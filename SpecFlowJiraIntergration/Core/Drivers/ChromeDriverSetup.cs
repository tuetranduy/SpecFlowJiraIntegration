using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace SpecFlowJiraIntegration.Core.Drivers
{
    public class ChromeDriverSetup : IDriverSetup
    {
        public IWebDriver CreateInstance()
        {
            new DriverManager().SetUpDriver(new ChromeConfig());

            return new ChromeDriver(GetDriverOptions());
        }

        private ChromeOptions GetDriverOptions()
        {
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("test-type --no-sandbox --start-maximized");
            if (TestContext.Parameters.Get("HeadlessMode").ToLower().Equals("true"))
            {
                chromeOptions.AddArgument("headless");
                chromeOptions.AddArgument("--window-size=1325x744");
            }
            return chromeOptions;
        }
    }
}
