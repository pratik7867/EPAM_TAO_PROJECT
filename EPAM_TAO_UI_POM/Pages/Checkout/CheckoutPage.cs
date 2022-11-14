using OpenQA.Selenium;
using EPAM_TAO_CORE_UI_TAF.UI_Helpers;
using EPAM_TAO_CORE_COMMON_TAF.CommonHelpers;

namespace EPAM_TAO_UI_POM.Pages.Checkout
{
    public class CheckoutPage
    {
        private static readonly object syncLock = new object();
        private static CheckoutPage _checkoutPage = null;

        private IWebDriver driver { get; set; }

        CheckoutPage(IWebDriver _driver)
        {
            driver = _driver;
        }

        public static CheckoutPage GetInstance(IWebDriver _driver)
        {
            lock (syncLock)
            {
                if (_checkoutPage == null)
                {
                    _checkoutPage = new CheckoutPage(_driver);
                }
                return _checkoutPage;
            }
        }

        #region Elements/Locators

        private By txtFirstNameLocator = By.Id("first-name");
        private IWebElement txtFirstNameElement { get; set; }        

        private By txtLastNameLocator = By.Id("last-name");
        private IWebElement txtLastNameElement { get; set; }        

        private By txtPostalCodeLocator = By.Id("postal-code");
        private IWebElement txtPostalCodeElement { get; set; }        

        private By btnContinueLocator = By.Id("continue");
        private IWebElement btnContinueElement { get; set; }        

        private By divProductNameLocator = By.ClassName("inventory_item_name");
        private IWebElement divProductNameElement { get; set; }

        private By divProductPriceLocator = By.ClassName("inventory_item_price");
        private IWebElement divProductPriceElement { get; set; }                

        private By btnFinishLocator = By.Id("finish");
        private IWebElement btnFinishElement { get; set; }

        #endregion

        #region Action Mehods

        public void FillUpCheckoutDetailsAndContiue(string strFirstName, string strLastName, string strPostalCode)
        {
            SeleniumUtilities.seleniumUtilities.WaitForPageLoad(driver, 10);
            Log4NetLogger.log.Info("Waited 10 sec. for page to load");

            txtFirstNameElement = SeleniumUtilities.seleniumUtilities.GetElement(driver, txtFirstNameLocator);
            txtFirstNameElement.Clear();
            Log4NetLogger.log.Info("Cleared the Fistname textbox");
            txtFirstNameElement.SendKeys(strFirstName);
            Log4NetLogger.log.Info("Filled up the Firstname");

            txtLastNameElement = SeleniumUtilities.seleniumUtilities.GetElement(driver, txtLastNameLocator);
            txtLastNameElement.Clear();
            Log4NetLogger.log.Info("Cleared the Lastname textbox");
            txtLastNameElement.SendKeys(strLastName);
            Log4NetLogger.log.Info("Filled up the Lastname");

            txtPostalCodeElement = SeleniumUtilities.seleniumUtilities.GetElement(driver, txtPostalCodeLocator);
            txtPostalCodeElement.Clear();
            Log4NetLogger.log.Info("Cleared the Postalcode textbox");
            txtPostalCodeElement.SendKeys(strPostalCode);
            Log4NetLogger.log.Info("Filled up the Postalcode");

            btnContinueElement = SeleniumUtilities.seleniumUtilities.GetElement(driver, btnContinueLocator);
            btnContinueElement.Click();
            Log4NetLogger.log.Info("Clicked on Continue button");
        }

        public string GetProductName()
        {
            divProductNameElement = SeleniumUtilities.seleniumUtilities.GetElement(driver, divProductNameLocator);

            return divProductNameElement.Text;
        }

        public string GetProductPrice()
        {
            divProductPriceElement = SeleniumUtilities.seleniumUtilities.GetElement(driver, divProductPriceLocator);

            return divProductPriceElement.Text;
        }

        public void ClickOnFinish()
        {
            btnFinishElement = SeleniumUtilities.seleniumUtilities.GetElement(driver, btnFinishLocator);
            btnFinishElement.Click();
            Log4NetLogger.log.Info("Clicked on Finish button");
        }

        #endregion
    }
}
