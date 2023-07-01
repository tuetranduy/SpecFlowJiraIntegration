using OpenQA.Selenium;
using SpecFlowJiraIntegration.Core.WebElement;

namespace SpecFlowJiraIntegration.PageObjects.Pages
{
    public class LoginPage
    {
        //Web Elements
        private WebObject _emailTextbox = new WebObject(By.Id("Email"), "Email Textbox");
        private WebObject _password_Textbox = new WebObject(By.Id("Password"), "Password Textbox");
        private WebObject _loginButton = new WebObject(By.XPath("//button[contains(text(),'Log in')]"), "Log in Button");

        //Contructor
        public LoginPage() { }

        //Page Methods
        public void Login(string email, string password)
        {
            _emailTextbox.EnterText(email);
            _password_Textbox.EnterText(password);
            _loginButton.ClickOnElement();
        }

        public bool IsMessageErrorAvailable(string message)
        {
            WebObject messageError = new WebObject(By.XPath(string.Format("//*[contains(text(),'{0}')]", message)), "Error Message: " + message);
            return messageError.IsElementDisplayed();
        }

        public WebObject GetLoginButtonWebObject()
        {
            return _loginButton;
        }

        public WebObject GetEmailTxtBoxWebObject()
        {
            return _emailTextbox;
        }
    }
}