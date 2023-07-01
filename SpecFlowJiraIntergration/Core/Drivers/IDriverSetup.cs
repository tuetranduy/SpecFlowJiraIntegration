using OpenQA.Selenium;

namespace SpecFlowJiraIntegration.Core.Drivers
{
    public interface IDriverSetup
    {
        IWebDriver CreateInstance();
    }
}
