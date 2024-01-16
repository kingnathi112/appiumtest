using Appium.Elements;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;

namespace AppiumTest.Tests
{
    public class AppiumTests
    {
        private AndroidDriver _driver;

        [OneTimeSetUp]
        public void SetUp()
        {
            var serverUri = new Uri(Environment.GetEnvironmentVariable("APPIUM_HOST") ?? "http://127.0.0.1:4723/");
            var driverOptions = new AppiumOptions()
            {
                AutomationName = AutomationName.AndroidUIAutomator2,
                PlatformName = "Android",
                DeviceName = "Android Emulator",
            };

            driverOptions.AddAdditionalAppiumOption("appPackage", "com.bitbar.testdroid");
            driverOptions.AddAdditionalAppiumOption("appActivity", ".BitbarSampleApplicationActivity");
            // NoReset assumes the app com.google.android is preinstalled on the emulator
            //driverOptions.AddAdditionalAppiumOption("noReset", true);

            _driver = new AndroidDriver(serverUri, driverOptions, TimeSpan.FromSeconds(180));
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _driver.Dispose();
        }

        [Test, Order(10)]
        public void WhenAppLoadsThenRadioButtonsMustBeThree()
        {
            var radioButtons = _driver.FindElements(By.ClassName(Elements.RadioClassBtns));
            Assert.That(radioButtons.Count, Is.EqualTo(3));
        }

        [Test, Order(20)]
        public void RadioButtonOneTextMustBeBuy101Devices()
        {
            var radioBtn1 = _driver.FindElement(By.Id(Elements.RadioIdBtn + "0"));
            Assert.That(radioBtn1.Text, Is.EqualTo("Buy 101 devices"));
        }

        [Test, Order(30)]
        public void UserToEnterRandomTextOnTheEditor()
        {
            var editText = _driver.FindElement(By.Id(Elements.TextEditorId));
            var randomText = "Nduduzo Was Here";
            editText.SendKeys(randomText);
            Assert.That(editText.Text, Is.EqualTo("Buy 101 devices"));
        }
    }
}