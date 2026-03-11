using OpenQA.Selenium;

namespace FinalTask.Pages
{
    public class LoginPage : BasePage
    {
        //Locators
        private readonly By usernameField = By.CssSelector("#user-name");
        private readonly By passwordField = By.CssSelector("#password");
        private readonly By loginButton = By.CssSelector("#login-button");
        private readonly By errorContainer = By.CssSelector(".error-message-container");

        public LoginPage(IWebDriver driver) : base(driver) 
        {
        }

        public void Navigate() 
        {
            driver.Navigate().GoToUrl("https://www.saucedemo.com/");
        }

        public void EnterUsername(string username)
        {
            EnterText(usernameField, username);
        }

        public void EnterPassword(string password)
        {
            EnterText(passwordField, password);
        }

        public void ClickLoginButton() 
        {
            Click(loginButton);
        }

        public void ClearUserName() 
        {
            clearField(usernameField);
        }

        public void ClearPassword()
        {
            clearField(passwordField);
        }

        public string GetError() 
        {
            return GetText(errorContainer);
        }
    }
}
