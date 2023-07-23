using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using tieuluan_ban_giay.Models;
using tieuluan_ban_giay.Controllers;

namespace NunitTest_Login
{
    [TestFixture]
    public class Tests
    {
        QLBangiayyEntities db = new QLBangiayyEntities();
        public IWebDriver driver;
        public string baseURL;
        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            baseURL = "";
            driver.Navigate().GoToUrl(baseURL);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.FindElement(By.LinkText("Đăng nhập")).Click();
        }

        [TestCase()]
        public void TC_LOGIN_01(string Ten_Dang_Nhap, string Mat_Khau, string expected)
        {
            var LoginController = new HomeController();
            //new Login = db.Khach_Hang.
            Assert.Pass();
        }
    }
}