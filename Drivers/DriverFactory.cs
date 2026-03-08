using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

namespace FinalTask.Drivers
{
    public static class DriverFactory
    {
        private static IWebDriver ?driver;

        public static IWebDriver InitDriver(string browser)
        {
            switch (browser.ToLower())
            {
                case "chrome":
                    driver = new ChromeDriver();
                    break;

                case "edge":
                    driver = new EdgeDriver();
                    break;

                case "firefox":
                    driver = new FirefoxDriver();
                    break;

                default:
                    throw new ArgumentException("Unsopported browser");
            }

            driver.Manage().Window.Maximize();
            return driver;
        }

        public static IWebDriver GetDriver()
        {
            return driver!;
        }

        public static void QuitDriver()
        {
            driver?.Quit();
        }

    }
}
