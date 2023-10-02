using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowProject2.Controls
{
    [Binding]
    public sealed class LoginControls
    {
        private readonly IWebDriver driver;

        public LoginControls(IWebDriver driver)
        {
            this.driver = driver;
        }
        public IWebElement LetMeHackButton => this.driver.FindElement(By.XPath("//button[text()='Let me hack!']"));
        public IWebElement UsernameTextbox => this.driver.FindElement(By.Id("username"));
        public IWebElement PasswordTextbox => this.driver.FindElement(By.Id("password"));
        public IWebElement LoginButton => this.driver.FindElement(By.Id("doLogin"));
    }
}
