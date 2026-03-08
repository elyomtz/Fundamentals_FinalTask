using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalTask.Pages
{
    public class LoginPage
    {
        private readonly IWebDriver _driver;

        public LoginPage(IWebDriver driver) 
        {
            _driver = driver;
        }

        //Locators
        private readonly By usernameField = By.CssSelector("#user-name");
        private readonly By passwordField = By.CssSelector("#password");
        private readonly By loginButton = By.CssSelector("#login-button");
        private readonly By errorContainer = By.CssSelector(".error-message-container");


        //Methods
        public void EnterUsername(string username)
        {
            _driver.FindElement(usernameField).SendKeys(username);
        }

        public void EnterPassword(string password)
        {
            _driver.FindElement(passwordField).SendKeys(password);
        }

        public void ClearUserName()
        {
            _driver.FindElement(usernameField).Clear();
            _driver.FindElement(usernameField).SendKeys(" ");
            _driver.FindElement(usernameField).SendKeys(Keys.Backspace);
        }

        public void ClearPassword()
        {
            _driver.FindElement(passwordField).Clear();
            _driver.FindElement(passwordField).SendKeys(" ");
            _driver.FindElement(passwordField).SendKeys(Keys.Backspace);
        }

        public void ClickLogin() 
        {
            _driver.FindElement(loginButton).Click();
        }

        public string GetErrorMessage()
        {
            var errorMessage = _driver.FindElement(errorContainer);
            return errorMessage.Text;
        }
    }
}
