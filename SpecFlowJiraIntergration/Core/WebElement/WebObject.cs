using OpenQA.Selenium;

namespace SpecFlowJiraIntegration.Core.WebElement
{
    public class WebObject
    {
        public By By { get; set; }
        public string Name { get; set; }

        public WebObject(By by, string name = "")
        {
            By = by;
            Name = name;
        }
    }
}