using FinalTask.Drivers;
using FinalTask.Pages;
using FluentAssertions;
using log4net;
using OpenQA.Selenium;

namespace FinalTask.Tests
{
   // [TestFixture]
   // [Parallelizable(ParallelScope.All)]
    public class Tests
    {
        IWebDriver ?driver;
        private static readonly ILog log = LogManager.GetLogger(typeof(Tests));


        [SetUp]
        public void Setup()
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo("log4net.config"));
        }

        [TestCase("chrome")]
        [TestCase("edge")]
        [TestCase("firefox")]
        public void UC_1_Empty_Username_and_Password_Fields_Shows_Username_is_required(string browser) 
        {

            log.Info($"-------------- Start of test case 1 using {browser}-----------------");

            //Arrange
            driver = DriverFactory.InitDriver(browser);

            log.Info("Navigating to https://www.saucedemo.com/");
            driver.Navigate().GoToUrl("https://www.saucedemo.com/");

            LoginPage loginPage = new LoginPage(driver);

            //Act
            log.Info("Using any credentials");
            loginPage.EnterUsername("user1");
            loginPage.EnterPassword("password1");

            log.Info("Clear both inputs");
            loginPage.ClearUserName();
            loginPage.ClearPassword();

            log.Info("Hit the \"Login\" button");
            loginPage.ClickLogin();

            log.Info("Get the error message");
            string ErrorMessage = loginPage.GetErrorMessage();

            //Assert
            log.Info("Verifiying that the error message is the expected one: \"Username is required\"");
            ErrorMessage.Should().Contain("Username is required");  //assertion using Fluent Assertions

            log.Info($"-------------- End of test case 1 using {browser}-----------------");
        }


        [TestCase("chrome")]
        [TestCase("edge")]
        [TestCase("firefox")]
        public void UC_2_Empty_Password_Field_Shows_Password_is_required(string browser)
        {
            log.Info($"-------------- Start of test case 2 using {browser}----------------");

            //Arrange
            driver = DriverFactory.InitDriver(browser);

            log.Info("Navigating to https://www.saucedemo.com/");
            driver.Navigate().GoToUrl("https://www.saucedemo.com/");

            LoginPage loginPage = new LoginPage(driver);

            //Act
            log.Info("Using any credentials");
            loginPage.EnterUsername("user2");
            loginPage.EnterPassword("password2");

            log.Info("Clear the \"Password\" input");
            loginPage.ClearPassword();

            log.Info("Hit the \"Login\" button");
            loginPage.ClickLogin();

            log.Info("Get the error message");
            string ErrorMessage = loginPage.GetErrorMessage();

            //Assert
            log.Info("Verifiying that the error message is the expected one: \"Password is required\"");
            ErrorMessage.Should().Contain("Password is required"); //assertion using Fluent Assertions

            log.Info($"-------------- End of test case 2 using {browser} -----------------");
        }


        [TestCase("chrome")]
        [TestCase("edge")]
        [TestCase("firefox")]
        public void UC_3_Successful_Login_With_Accepted_Username_and_Password(string browser)
        {
            log.Info($"-------------- Start of test case 3 using {browser}----------------");

            //Arrange
            driver = DriverFactory.InitDriver(browser);

            log.Info("Navigating to https://www.saucedemo.com/");
            driver.Navigate().GoToUrl("https://www.saucedemo.com/");

            LoginPage loginPage = new LoginPage(driver);
            InventoryPage swagLabsPage = new InventoryPage(driver);

            //Act
            log.Info("Login with the accepted credentials: Username: standard_user, Password: secret_sauce");
            loginPage.EnterUsername("standard_user");
            loginPage.EnterPassword("secret_sauce");

            log.Info("Hit the \"Login\" button");
            loginPage.ClickLogin();

            log.Info("Get the title from the page that is opened when accepted credentials are used");
            string title = swagLabsPage.GetTitle();

            //Assert
            log.Info("Validate the title \"Swag Labs\" in the dashboard.");
            title.Should().Contain("Swag Labs"); //assertion using Fluent Assertions

            log.Info($"-------------- End of test case 3 using {browser}-----------------");
        }
        
        
        [TearDown]
        public void TearDown()
        {
            driver?.Close();
            DriverFactory.QuitDriver();
        }
    }
}