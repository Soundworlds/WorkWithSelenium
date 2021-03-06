using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Chrome;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using Xunit;

namespace Test
{
    
    public class TestClass
    {
     
        private static string browserName = "Firefox"; // Chrome, Firefox or Edge - three names for testing

        private static IWebDriver _webDriver;
        private static string driverPath = Environment.CurrentDirectory;
        private static string _Title = "";
        private static string _Url = "";

        public static void Main()
        {
            SetUp();
            Test();

            string i = Console.ReadLine();
            if (i == "0")
            {
                TearDown();
            }
        }
        
        public static void SetUp()
        {
            if (browserName == "Firefox")
            {
                new DriverManager().SetUpDriver(new FirefoxConfig());
                _webDriver = new FirefoxDriver(driverPath);
            }
            else if (browserName == "Edge")
            {
                new DriverManager().SetUpDriver(new EdgeConfig());
                _webDriver = new EdgeDriver(driverPath);
            }
            else if (browserName == "Chrome")
            {
                new DriverManager().SetUpDriver(new ChromeConfig());
                _webDriver = new ChromeDriver(driverPath);
            }
        }

        
        public static void TearDown()
        {
            _webDriver.Quit();
        }

        
        public static void Test()
        {
            _webDriver.Navigate().GoToUrl("https://www.google.com");
            Assert.True(_webDriver.Title.Contains("Google"));

            
            IWebElement googleAppsButton = _webDriver.FindElement(By.ClassName("gb_Tc"));
            googleAppsButton.Click();
            _Title = _webDriver.Title;
            _Url = _webDriver.Url;
            ConsolePrint(_Title, _Url);

            _webDriver.Navigate().GoToUrl("https://translate.google.ru/?hl=ru&authuser=0");
            _Title = _webDriver.Title;
            _Url = _webDriver.Url;
            ConsolePrint(_Title, _Url);

            _webDriver.FindElement(By.ClassName("er8xn")).SendKeys("Test number one hundred" + Keys.Enter);
            googleAppsButton = _webDriver.FindElement(By.ClassName("VfPpkd-YVzG2b"));
            googleAppsButton.Click();
            ConsolePrint(_Title, _Url);

            _webDriver.Manage().Timeouts().ImplicitWait = new TimeSpan(0, 0, 5);

            googleAppsButton = _webDriver.FindElement(By.ClassName("Llmcnf"));
            googleAppsButton.Click();
            ConsolePrint(_Title, _Url);
        }

        public bool isElementPresent(By locator)
        {
            try
            {
                _webDriver.Manage().Timeouts().ImplicitWait = new TimeSpan(0);
                return _webDriver.FindElement(locator).Displayed;
            }
            catch (NoSuchElementException e)
            {
                return false;
            }
            finally
            {
                _webDriver.Manage().Timeouts().ImplicitWait = new TimeSpan(0,0,30);
            }
        }

        public static void ConsolePrint(string title, string url)
        {
            Console.WriteLine(_Title);
            Console.WriteLine(_Url);
        }
    }
}