using BoDi;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpecFlowProject2.Controls;

namespace SpecFlowProject2
{
    [Binding]
    public sealed class DriverSetup
    {
        private IObjectContainer objectContainer;
        private IWebDriver Driver { get; set; }

        //LoginControls loginControls => new LoginControls(Driver);
        public DriverSetup(IObjectContainer objectContainer)
        {
            this.objectContainer = objectContainer;
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            Driver = new ChromeDriver();
            objectContainer.RegisterInstanceAs(this.Driver);

           // Driver.Navigate().GoToUrl("https://automationintesting.online/#/");
           // Driver.Manage().Window.Maximize();

            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            //this.loginControls.LetMeHackButton.Click();
        }

        [AfterScenario]
        public void AfterScenario()
        {
            Driver.Close();
        }
    }
}
