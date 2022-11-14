using System;
using System.Configuration;
using System.Reflection;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using log4net;
using EPAM_TAO_CORE_COMMON_TAF.CommonHelpers;
using EPAM_TAO_CORE_UI_TAF.TestSetup;
using EPAM_TAO_CORE_UI_TAF.UI_Helpers;
using EPAM_TAO_UI_POM.Pages.Products;
using EPAM_TAO_UI_POM.Pages.Cart;
using EPAM_TAO_UI_POM.Pages.Checkout;

namespace EPAM_TAO_UI_TESTS.BaseTestConfig
{
    [TestFixture]
    public abstract class BaseTest : TestHookup
    {        
        private string strBrowser { get { return ConfigurationManager.AppSettings["Browser"].ToString(); } }
        private string strSiteURL { get { return ConfigurationManager.AppSettings["SiteURL"].ToString(); } }
        private string strAUT { get { return ConfigurationManager.AppSettings["AUT"].ToString(); } }
        public string strUserName { get { return ConfigurationManager.AppSettings["UserName"].ToString(); } }
        public string strPassword { get { return ConfigurationManager.AppSettings["Password"].ToString(); } }
        public Exception testEx { get; set; }
        public ProductsPage productsPage {get; set;}
        public CartPage cartPage { get; set; }
        public CheckoutPage checkoutPage { get; set; }

        [OneTimeSetUp]
        public void setUpOneTime()
        {
            Log4NetLogger.log4NetLogger.ConfigureLogFile();
        }

        [SetUp]
        public void setupDriverSession()
        {
            try
            {
                driver = InitBrowser((BrowserType)Enum.Parse(typeof(BrowserType), strBrowser.ToUpper()));
                Log4NetLogger.log.Info("Browser Initialized");

                SeleniumUtilities.seleniumUtilities.NavigateToURL(driver, strSiteURL);
                Log4NetLogger.log.Info("Navigated to URL: " + strSiteURL);

                SeleniumUtilities.seleniumUtilities.MaximizeWindow(driver);
                Log4NetLogger.log.Info("Browser Maximized");

                ExtentReportHelper.GetInstance(strAUT, driver).CreateTest(TestContext.CurrentContext.Test.Name);
            }
            catch(Exception ex)
            {
                Log4NetLogger.log.Error(ex.Message, ex);
                ErrorLogger.errorLogger.ErrorLog(MethodBase.GetCurrentMethod().Name, ex, driver);
                ExtentReportHelper.GetInstance(strAUT, driver).SetTestStatusFail($"<br>{ex.Message}<br>Stack Trace: <br>{ex.StackTrace}<br>");
            }
        }

        [TearDown]
        public void closeDriverSession()
        {
            try
            {
                var status = TestContext.CurrentContext.Result.Outcome.Status;                

                switch (status)
                {
                    case TestStatus.Failed:
                        var stacktrace = TestContext.CurrentContext.Result.StackTrace;
                        var errorMessage = "<pre>" + TestContext.CurrentContext.Result.Message + "</pre>";

                        Log4NetLogger.log.Error(testEx.Message, testEx);
                        ErrorLogger.errorLogger.ErrorLog(MethodBase.GetCurrentMethod().Name, testEx, driver);
                        ExtentReportHelper.GetInstance(strAUT, driver).SetTestStatusFail($"<br>{errorMessage}<br>Stack Trace: <br>{stacktrace}<br>", SeleniumUtilities.seleniumUtilities.TakeScreenshot(driver, TestContext.CurrentContext.Test.Name));
                        break;
                    case TestStatus.Skipped:                        
                        ExtentReportHelper.GetInstance(strAUT, driver).SetTestStatusSkipped();
                        Log4NetLogger.log.Warn("Test Skipped");
                        break;
                    default:                        
                        ExtentReportHelper.GetInstance(strAUT, driver).SetTestStatusPass();
                        Log4NetLogger.log.Info("Test Passed");
                        break;
                }                
            }
            catch(Exception ex)
            {
                Log4NetLogger.log.Error(ex.Message, ex);
                ErrorLogger.errorLogger.ErrorLog(MethodBase.GetCurrentMethod().Name, ex, driver);
                ExtentReportHelper.GetInstance(strAUT, driver).SetTestStatusFail($"<br>{ex.Message}<br>Stack Trace: <br>{ex.StackTrace}<br>");
            }
            finally
            {
                CloseBrowser();
            }
        }

        [OneTimeTearDown]
        public void CloseAll()
        {
            try
            {
                ExtentReportHelper.GetInstance(strAUT, driver).CloseExtentReport();
            }
            catch(Exception ex)
            {
                ErrorLogger.errorLogger.ErrorLog(MethodBase.GetCurrentMethod().Name, ex);
                ExtentReportHelper.GetInstance(strAUT, driver).SetTestStatusFail($"<br>{ex.Message}<br>Stack Trace: <br>{ex.StackTrace}<br>");
            }
        }
    }
}
