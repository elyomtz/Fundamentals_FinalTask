using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace FinalTask.Pages
{
    public class BasePage
    {
        protected IWebDriver driver;
        protected WebDriverWait wait;

        public BasePage(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(2));
        }

        protected IWebElement WaitForElement(By locator)
        {
            try
            {
                return wait.Until(driver => driver.FindElement(locator));
            }
            catch (WebDriverTimeoutException ex)
            {
                throw new Exception($"Element not found: {locator}", ex);
            }
        }

        protected void clearField(By locator)
        {
            try
            {
                var element = WaitForElement(locator);
                element.Clear();
                element.SendKeys(" ");
                element.SendKeys(Keys.Backspace);
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to clear text in: {locator}", ex);
            }
        }


        protected void EnterText(By locator, string text)
        {
            try
            {
                var element = WaitForElement(locator);
                element.Clear();
                element.SendKeys(text);
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to enter text in: {locator}", ex);
            }
        }

        protected void Click(By locator)
        {
            try
            {
                WaitForElement(locator).Click();
            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to click element: {locator}", ex);
            }
        }

        protected string GetText(By locator)
        {
            try 
            {
                var element = WaitForElement(locator);
                return element.Text;

            }
            catch (Exception ex)
            {
                throw new Exception($"Unable to get the text from: {locator}", ex);
            }
        }

    }
}
