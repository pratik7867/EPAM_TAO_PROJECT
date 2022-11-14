using OpenQA.Selenium;
using EPAM_TAO_CORE_UI_TAF.UI_Helpers;
using EPAM_TAO_CORE_COMMON_TAF.CommonHelpers;
using EPAM_TAO_UI_POM.Pages.Cart;

namespace EPAM_TAO_UI_POM.Pages.Products
{
    public class ProductsPage
    {
        private static readonly object syncLock = new object();
        private static ProductsPage _productsPage = null;

        private IWebDriver driver { get; set; }

        ProductsPage(IWebDriver _driver)
        {
            driver = _driver;
        }

        public static ProductsPage GetInstance(IWebDriver _driver)
        {
            lock (syncLock)
            {
                if (_productsPage == null)
                {
                    _productsPage = new ProductsPage(_driver);
                }
                return _productsPage;
            }
        }

        #region Elements/Locators

        private string strDivProductPriceLocatorValue = ".//button[@id='add-to-cart-{0}']/preceding::div[@class='inventory_item_price']";
        private By divProductPriceLocator { get; set; }
        private IWebElement divProductPriceElement { get; set; }

        private string strBtnAddToCartLocatorValue = "add-to-cart-{0}";
        private By btnAddToCartLocator { get; set; }
        private IWebElement btnAddToCartElement { get; set; }

        private By btnShoppingCartLocator = By.Id("shopping_cart_container");
        private IWebElement btnShoppingCartElement { get; set; }

        #endregion

        #region Action Methods

        public string getProductPrice(string strProductName)
        {
            SeleniumUtilities.seleniumUtilities.WaitForPageLoad(driver, 10);
            Log4NetLogger.log.Info("Waited 10 sec. for page to load");

            divProductPriceLocator = By.XPath(SeleniumUtilities.seleniumUtilities.GetDynamicLocatorString(strDivProductPriceLocatorValue, strProductName, " ", "-").ToLower());
            divProductPriceElement = SeleniumUtilities.seleniumUtilities.WaitForElementToBeVisible(driver, divProductPriceLocator, 5);
            Log4NetLogger.log.Info("Waited 5 sec. for Product Price Element to be Visible");

            return divProductPriceElement.Text;
        }

        public void AddToCart(string strProductName)
        {
            btnAddToCartLocator = By.Id(SeleniumUtilities.seleniumUtilities.GetDynamicLocatorString(strBtnAddToCartLocatorValue, strProductName, " ", "-").ToLower());
            btnAddToCartElement = SeleniumUtilities.seleniumUtilities.WaitForElementToBeVisible(driver, btnAddToCartLocator, 5);
            Log4NetLogger.log.Info("Waited 5 sec. for Add To Cart button Element to be Visible");

            btnAddToCartElement.Click();
            Log4NetLogger.log.Info("Clicked on Add To Cart button");
        }

        public CartPage ClickOnShoppingCart()
        {
            btnShoppingCartElement = SeleniumUtilities.seleniumUtilities.GetElement(driver, btnShoppingCartLocator);
            btnShoppingCartElement.Click();
            Log4NetLogger.log.Info("Clicked on Shopping Cart button");

            return CartPage.GetInstance(driver);
        }

        #endregion
    }
}
