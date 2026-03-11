using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalTask.Drivers;
using FinalTask.Pages;
using FluentAssertions;
using log4net;
using OpenQA.Selenium;

namespace FinalTask.Tests
{
    public class LoginTests
    {
        IWebDriver? driver;
        private static readonly ILog log = LogManager.GetLogger(typeof(LoginTests));


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
            try
            {
                driver = DriverFactory.InitDriver(browser);
            }
            catch (Exception ex) 
            {
                log.Info("Error initiating the driver");
                log.Info(ex.Message);
            }

            LoginPage loginPage = new LoginPage(driver!);
            
            //Act
            try
            {
                log.Info("Navigating to https://www.saucedemo.com/");
                loginPage.Navigate();
            }
            catch (Exception ex)
            {
                log.Info("Error opening the page https://www.saucedemo.com/");
                log.Error(ex.Message);
                throw;
            }

            try
            {
                log.Info("Using any credentials");
                loginPage.EnterUsername("user1");
                loginPage.EnterPassword("password1");
            }
            catch (Exception ex)
            {
                log.Info("Error entering Username or Password");
                log.Error(ex.Message);
            }

            try
            {
                log.Info("Clear both inputs");
                loginPage.ClearUserName();
                loginPage.ClearPassword();
            }
            catch (Exception ex)
            {
                log.Info("Error clearing inputs");
                log.Error(ex.Message);
            }

            try
            {
                log.Info("Hit the \"Login\" button");
                loginPage.ClickLoginButton();
            }
            catch (Exception ex) 
            {
                log.Info("Error when clicking the Login button");
                log.Error(ex.Message);
            }

            try
            {
                log.Info("Get the error message");
                string ErrorMessage = loginPage.GetError();
                log.Info("Verifiying that the error message is the expected one: \"Username is required\"");

                //Assert
                ErrorMessage.Should().Contain("Hola");  //assertion using Fluent Assertions
                //"Username is required"
            }
            catch (Exception ex)
            {
                log.Info("Error getting the error message");
                log.Error(ex.Message);
            }

            log.Info($"-------------- End of test case 1 using {browser}-----------------");
        }

        [TestCase("chrome")]
        [TestCase("edge")]
        [TestCase("firefox")]
        public void UC_2_Empty_Password_Field_Shows_Password_is_required(string browser)
        {
            log.Info($"-------------- Start of test case 2 using {browser}----------------");

            //Arrange
            try
            {
                driver = DriverFactory.InitDriver(browser);
            }
            catch (Exception ex)
            {
                log.Info("Error initiating the driver");
                log.Info(ex.Message);
            }

            LoginPage loginPage = new LoginPage(driver!);

            //Act
            try
            {
                log.Info("Navigating to https://www.saucedemo.com/");
                loginPage.Navigate();
            }
            catch (Exception ex)
            {
                log.Info("Error opening the page https://www.saucedemo.com/");
                log.Error(ex.Message);
                throw;
            }

            try
            {
                log.Info("Using any credentials");
                loginPage.EnterUsername("user2");
                loginPage.EnterPassword("password2");
            }
            catch (Exception ex)
            {
                log.Info("Error entering Username or Password");
                log.Error(ex.Message);
            }

            try
            {
                log.Info("Clear the \"Password\" input");
                loginPage.ClearPassword();
            }
            catch (Exception ex)
            {
                log.Info("Error clearing inputs");
                log.Error(ex.Message);
            }

            try
            {
                log.Info("Hit the \"Login\" button");
                loginPage.ClickLoginButton();
            }
            catch (Exception ex)
            {
                log.Info("Error when clicking the Login button");
                log.Error(ex.Message);
            }

            try
            {
                log.Info("Get the error message");
                string ErrorMessage = loginPage.GetError();
                log.Info("Verifiying that the error message is the expected one: \"Password is required\"");

                //Assert
                ErrorMessage.Should().Contain("Password is required");  //assertion using Fluent Assertions
            }
            catch (Exception ex)
            {
                log.Info("Error getting the error message");
                log.Error(ex.Message);
            }

            log.Info($"-------------- End of test case 2 using {browser} -----------------");
        }


        [TestCase("chrome")]
        [TestCase("edge")]
        [TestCase("firefox")]
        public void UC_3_Successful_Login_With_Accepted_Username_and_Password(string browser)
        {
            log.Info($"-------------- Start of test case 3 using {browser}----------------");

            //Arrange
            try
            {
                driver = DriverFactory.InitDriver(browser);
            }
            catch (Exception ex)
            {
                log.Info("Error initiating the driver");
                log.Info(ex.Message);
            }

            LoginPage loginPage = new LoginPage(driver!);
            InventoryPage swagLabsPage = new InventoryPage(driver);

            //Act
            try
            {
                log.Info("Navigating to https://www.saucedemo.com/");
                loginPage.Navigate();
            }
            catch (Exception ex)
            {
                log.Info("Error opening the page https://www.saucedemo.com/");
                log.Error(ex.Message);
                throw;
            }

            try{
                log.Info("Login with the accepted credentials: Username: standard_user, Password: secret_sauce");
                loginPage.EnterUsername("standard_user");
                loginPage.EnterPassword("secret_sauce");
            }
            catch (Exception ex)
            {
                log.Info("Error entering Username or Password");
                log.Error(ex.Message);
            }

            try
            {
                log.Info("Hit the \"Login\" button");
                loginPage.ClickLoginButton();
            }
            catch (Exception ex)
            {
                log.Info("Error when clicking the Login button");
                log.Error(ex.Message);
            }

            try
            {
                log.Info("Get the title from the page that is opened when accepted credentials are used");
                string title = swagLabsPage.GetTitle();

                //Assert
                log.Info("Validate the title \"Swag Labs\" in the dashboard.");
                title.Should().Contain("Swag Labs"); //assertion using Fluent Assertions
            }
            catch (Exception ex) 
            {
                log.Info("Error retrieving the message on inventory page");
                log.Error(ex.Message);
            }

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
