using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using Xunit;

namespace Test
{
    //[TestFixture]
    public class TestClass
    {

        private static IWebDriver _webDriver;
        private static string driverFirefoxPath = Environment.CurrentDirectory;
        private static string _Title = "";
        private static string _Url;

        public static void Main()
        {
            SetUp();
            Test();
            Console.WriteLine(_Title);
            Console.WriteLine(_Url);

            string i = Console.ReadLine();
            if (i == "0")
            {
                TearDown();
            }


        }
        
        public static void SetUp()
        {
            //var driver = new FirefoxDriver(@"C:\Downloads\");
            new DriverManager().SetUpDriver(new FirefoxConfig());
            _webDriver = new FirefoxDriver(driverFirefoxPath);
        }

        
        public static void TearDown()
        {
            _webDriver.Quit();
        }

        
        public static void Test()
        {
            _webDriver.Navigate().GoToUrl("https://www.google.com");
            Assert.True(_webDriver.Title.Contains("Google"));

            //_webDriver.FindElement(By.Name("q")).SendKeys("webdriver" + Keys.Enter);
            IWebElement fruits = _webDriver.FindElement(By.ClassName("gb_Tc"));
            fruits.Click();
            _Title = _webDriver.Title;
            _Url = _webDriver.Url;
        }
    }
}