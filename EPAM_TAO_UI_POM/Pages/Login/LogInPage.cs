﻿using OpenQA.Selenium;
using EPAM_TAO_CORE_UI_TAF.UI_Helpers;
using EPAM_TAO_CORE_COMMON_TAF.CommonHelpers;
using EPAM_TAO_UI_POM.Pages.Products;

namespace EPAM_TAO_UI_POM.Pages.Login
{
    public class LogInPage
    {
        private static readonly object syncLock = new object();
        private static LogInPage _loginPage = null;

        private IWebDriver driver { get; set; }

        LogInPage(IWebDriver _driver)
        {
            driver = _driver;
        }

        public static LogInPage GetInstance(IWebDriver _driver)
        {
            lock (syncLock)
            {
                if (_loginPage == null)
                {
                    _loginPage = new LogInPage(_driver);
                }
                return _loginPage;
            }
        }

        #region Elements/Locators       

        private By txtUserNameLocator = By.Id("user-name");
        private IWebElement txtUserNameElement { get; set; }        

        private By txtPasswordLocator = By.Id("password");
        private IWebElement txtPasswordElement { get; set; }        

        private By btnLoginLocator = By.Id("login-button");
        private IWebElement btnLoginElement { get; set; }

        #endregion

        #region Action Methods

        public ProductsPage LogIntoApplication(string strUserName, string strPassword)
        {            
            SeleniumUtilities.seleniumUtilities.WaitForPageLoad(driver, 10);
            Log4NetLogger.log.Info("Waited 10 sec. for page to load");

            txtUserNameElement = SeleniumUtilities.seleniumUtilities.GetElement(driver, txtUserNameLocator);
            txtUserNameElement.Clear();
            Log4NetLogger.log.Info("Cleared the Username textbox");
            txtUserNameElement.SendKeys(strUserName);
            Log4NetLogger.log.Info("Filled up the Username");

            txtPasswordElement = SeleniumUtilities.seleniumUtilities.GetElement(driver, txtPasswordLocator);
            txtPasswordElement.Clear();
            Log4NetLogger.log.Info("Cleared the Password textbox");
            txtPasswordElement.SendKeys(strPassword);
            Log4NetLogger.log.Info("Filled up the Password");

            btnLoginElement = SeleniumUtilities.seleniumUtilities.GetElement(driver, btnLoginLocator);
            btnLoginElement.Click();
            Log4NetLogger.log.Info("Clicked on Login button");

            return ProductsPage.GetInstance(driver);
        }

        #endregion
    }
}
