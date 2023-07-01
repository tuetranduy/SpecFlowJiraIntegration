using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using SpecFlowJiraIntegration.Core.Drivers;
using SpecFlowJiraIntegration.Core.Report;
using SpecFlowJiraIntegration.Helpers;
using System;
using TechTalk.SpecFlow;

namespace SpecFlowJiraIntegration.Steps
{
    [Binding]
    public class Hooks
    {
        public static IConfiguration Config;

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            TestContext.Progress.WriteLine("=========> Global OneTimeSetUp");

            //Read Configuration file
            var environment = TestContext.Parameters.Get("Environment");
            Config = ConfigurationHelper.ReadConfiguration($"Configurations\\Environment\\{environment}\\appsettings.json");

            //Init Extend report
            var reportPath = FileHelper.GetProjectFolderPath() + ConfigurationHelper.GetConfigurationByKey(Config, "TestResult.FilePath");
            HtmlReporter.CreateInstance(reportPath, "Demo Test", TestContext.Parameters.Get("Environment"), TestContext.Parameters.Get("Browser"));
        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext)
        {
            HtmlReporter.CreateTest(featureContext.FeatureInfo.Title);
        }

        [BeforeScenario]
        public static void BeforeScenario(FeatureContext featureContext, ScenarioContext scenarioContext)
        {
            Console.WriteLine("BaseTest Set up");
            HtmlReporter.CreateNode(scenarioContext.ScenarioInfo.Title);


            foreach (var tag in featureContext.FeatureInfo.Tags)
            {
                if (tag.ToLower().Equals("apitest"))
                {
                    return;
                }
            }
            BrowserFactory.InitializeDriver(TestContext.Parameters.Get("Browser"));
        }

        [AfterScenario]
        public static void AfterScenario(FeatureContext featureContext, ScenarioContext scenarioContext)
        {
            Console.WriteLine("BaseTest Tear Down");
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stacktrace = string.IsNullOrEmpty(TestContext.CurrentContext.Result.StackTrace)
            ? ""
            : string.Format("{0}", TestContext.CurrentContext.Result.StackTrace);
            HtmlReporter.CreateTestResult(status, stacktrace, featureContext.FeatureInfo.Title, TestContext.CurrentContext.Test.Name);
            BrowserFactory.CleanUp();
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            TestContext.Progress.WriteLine("=========> Global OneTimeTearDown");
            BrowserFactory.ThreadLocalWebDriver.Dispose();
            HtmlReporter.Flush();
        }
    }
}
