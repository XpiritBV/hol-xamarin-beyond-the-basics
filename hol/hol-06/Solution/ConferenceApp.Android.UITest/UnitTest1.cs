using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.MultiTouch;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Remote;
using System;
using System.IO;
using System.Reflection;

namespace ConferenceApp.Android.UITest
{
    [TestClass]
    public class UnitTest1
    {
        private AndroidDriver<AppiumWebElement> driver;
        private AppiumLocalService _appiumLocalService;

        [TestInitialize]
        public void initialize()
        {
            System.Environment.SetEnvironmentVariable("ANDROID_HOME", @"C:\Program Files (x86)\Android\android-sdk");
            System.Environment.SetEnvironmentVariable("JAVA_HOME", @"C:\Program Files\Android\jdk\microsoft_dist_openjdk_1.8.0.25\bin");


            var capabilities = new AppiumOptions();
            //capabilities.AddAdditionalCapability(MobileCapabilityType.PlatformVersion, "5.0.1");
            capabilities.AddAdditionalCapability(AndroidMobileCapabilityType.AppPackage, "com.yourcompany.conferenceapp");
            capabilities.AddAdditionalCapability(AndroidMobileCapabilityType.AppActivity, "mainactivity");
            capabilities.AddAdditionalCapability(AndroidMobileCapabilityType.Avd, "demo_device");
            capabilities.AddAdditionalCapability(AndroidMobileCapabilityType.AvdArgs, "demo_device");
            capabilities.AddAdditionalCapability(MobileCapabilityType.DeviceName, "demo_device");
            capabilities.AddAdditionalCapability(MobileCapabilityType.AutomationName, "UiAutomator2");
            capabilities.AddAdditionalCapability(MobileCapabilityType.App, @"C:\source\hol-xamarin-beyond-the-basics\hol\hol-06\Solution\ConferenceApp.Android\bin\Debug\com.yourcompany.conferenceapp-Signed.apk");
            var currentPath = Directory.GetCurrentDirectory();
            Console.WriteLine($"Current path: {currentPath}");
            Uri serverUri = new Uri("http://127.0.0.1:4723/wd/hub");
            //var _appiumLocalService = new AppiumServiceBuilder().UsingAnyFreePort().Build();
            //_appiumLocalService.Start(); ;
            driver = new AndroidDriver<AppiumWebElement>(serverUri, capabilities);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            driver.CloseApp();

        }
       [TestCleanup]
       public void CleanUp()
        {
            _appiumLocalService?.Dispose();
            _appiumLocalService = null;
        }

        [TestMethod]
        public void TestMethod1()
        {
            driver.LaunchApp();
            var el1 = driver.FindElementByXPath("/hierarchy/android.widget.FrameLayout/android.widget.LinearLayout/android.widget.FrameLayout/android.widget.FrameLayout/android.widget.FrameLayout/android.widget.RelativeLayout/android.view.ViewGroup/android.support.v4.widget.DrawerLayout/android.widget.FrameLayout/android.widget.LinearLayout/android.widget.FrameLayout[1]/android.view.ViewGroup/android.support.v4.view.ViewPager/android.view.ViewGroup/android.view.ViewGroup/android.view.ViewGroup/android.support.v7.widget.RecyclerView/android.view.ViewGroup[4]/android.view.ViewGroup/android.widget.FrameLayout");
            TouchAction a = new TouchAction(driver);
            a.Tap(el1);
            
            el1.Click();
            var el2 = driver.FindElementByXPath("/hierarchy/android.widget.FrameLayout/android.widget.LinearLayout/android.widget.FrameLayout/android.widget.FrameLayout/android.widget.FrameLayout/android.widget.RelativeLayout/android.view.ViewGroup/android.support.v4.widget.DrawerLayout/android.widget.FrameLayout/android.widget.LinearLayout/android.widget.FrameLayout[1]/android.view.ViewGroup/android.view.ViewGroup/android.view.ViewGroup/android.widget.ScrollView/android.view.ViewGroup/android.view.ViewGroup/android.widget.FrameLayout[2]");
            

            Assert.IsTrue(el2.Text.Contains("will"));

            driver.CloseApp();

        }
    }
}
