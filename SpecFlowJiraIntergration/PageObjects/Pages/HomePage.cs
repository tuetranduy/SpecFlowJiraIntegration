using OpenQA.Selenium;
using SpecFlowJiraIntegration.Core.Drivers;
using SpecFlowJiraIntegration.Core.WebElement;
using SpecFlowJiraIntegration.Helpers;
using SpecFlowJiraIntegration.Steps;

namespace SpecFlow.Framework.PageObjects.Pages
{
    public class HomePage
    {
        //Web Elements
        private WebObject _loginLink = new WebObject(By.ClassName("ico-login"), "Login Link");

        //Contructor
        public HomePage() { }

        //Page Methods
        public void ClickLoginLink()
        {
            _loginLink.ClickOnElement();
        }

        public void VisitHomePage()
        {
            DriverUtils.GoToUrl(ConfigurationHelper.GetConfigurationByKey(Hooks.Config, "TestURL"));
        }
    }
}