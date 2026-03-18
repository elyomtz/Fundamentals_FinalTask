using OpenQA.Selenium;

namespace FinalTask.Pages
{
    public class InventoryPage : BasePage
    {

        public InventoryPage(IWebDriver driver) : base(driver) 
        {
        }

        //Locators
        private readonly By dashboardTitle = By.CssSelector(".app_logo");

        //Methods
        public string GetTitle()
        {
            var title = driver.FindElement(dashboardTitle);
            return title.Text;
        }
    }
}
