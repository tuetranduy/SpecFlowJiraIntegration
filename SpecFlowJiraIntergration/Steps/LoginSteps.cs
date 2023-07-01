using NUnit.Framework;
using SpecFlow.Framework.PageObjects.Pages;
using SpecFlowJiraIntegration.Helpers;
using SpecFlowJiraIntegration.PageObjects.Pages;
using System.Data;
using TechTalk.SpecFlow;

namespace SpecFlowJiraIntegration.Steps
{
    [Binding]
    public class LoginSteps
    {
        private readonly HomePage _homePage = new HomePage();
        private readonly LoginPage _loginPage = new LoginPage();
        private readonly ScenarioContext _scenarioContext;
        private readonly FeatureContext _featureContext;

        public LoginSteps(ScenarioContext scenarioContext, FeatureContext featureContext)
        {
            _scenarioContext = scenarioContext;
            _featureContext = featureContext;
        }

        [Given(@"I open Homepage")]
        public void GivenIopenHomepage()
        {
            _homePage.VisitHomePage();
        }

        [Given(@"I navigate to Login page")]
        public void GivenInavigatetoLoginpage()
        {
            _homePage.ClickLoginLink();
        }

        [When(@"I log in with ""(.*)"" and ""(.*)""")]
        public void WhenIloginwithand(string email, string password)
        {
            _loginPage.Login(email, password);
        }


        [When(@"I log in with following account")]
        public void WhenIloginwithfollowingaccount(Table table)
        {
            var dictionary = TableExtensions.ToDictionary(table);
            _loginPage.Login(dictionary["Email"], dictionary["Password"]);
        }

        [When(@"I log in with account below")]
        public void WhenIloginwithaccountbelow(Table table)
        {
            var dataTable = TableExtensions.ToDataTable(table);
            foreach (DataRow row in dataTable.Rows)
            {
                _loginPage.Login(row.ItemArray[0].ToString(), row.ItemArray[1].ToString());
            }
        }

        [Then(@"the ""(.*)"" should display")]
        public void Thentheshoulddisplays(string message)
        {
            Assert.That(_loginPage.IsMessageErrorAvailable(message), Is.True);
        }
    }
}