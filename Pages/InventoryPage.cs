using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalTask.Pages
{
    public class InventoryPage
    {
        private readonly IWebDriver _driver;

        public InventoryPage(IWebDriver driver) 
        {
            _driver = driver;
        }

        //Locators
        private readonly By dashboardTitle = By.CssSelector(".app_logo");

        //Methods
        public string GetTitle() 
        {
            var title = _driver.FindElement(dashboardTitle);
            return title.Text;
        }
    }
}
