using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Collections.ObjectModel;

namespace SpecFlowProject2.Controls
{
    public class AdminControls
    {
        private readonly IWebDriver driver;
        WebDriverWait wait;
        public AdminControls(IWebDriver driver)
        {
            this.driver = driver;
            this.wait = new WebDriverWait(driver, new TimeSpan(0, 0, 20));
        }

        public By Room(string roomName) => By.XPath("/.//p[@id='roomName" + roomName + "']");

        public IWebElement FirstRoomNameField => this.driver.FindElement(By.Id("roomName101"));
        public IWebElement SelectRoomName(string roomName) => this.driver.FindElement(Room(roomName));

        public ReadOnlyCollection<IWebElement> Rooms => this.driver.FindElements(By.XPath(".//div[@class='row detail']"));

        public IWebElement RoomNameField => this.driver.FindElement(By.Id("roomName"));
        public IWebElement RoomTypeSelect => this.driver.FindElement(By.Id("type"));
        public IWebElement RoomAccessibleSelect => this.driver.FindElement(By.Id("accessible"));
        public IWebElement RoomPriceField => this.driver.FindElement(By.Id("roomPrice"));
        public IWebElement RoomWifiCheckbox => this.driver.FindElement(By.Id("wifiCheckbox"));
        public IWebElement RoomTvCheckbox => this.driver.FindElement(By.Id("tvCheckbox"));
        public IWebElement RoomRefreshCheckbox => this.driver.FindElement(By.Id("refreshCheckbox"));
        public IWebElement RoomSafeCheckbox => this.driver.FindElement(By.Id("safeCheckbox"));
        public IWebElement RoomViewsCheckbox => this.driver.FindElement(By.Id("viewsCheckbox"));
        public IWebElement CreateRoomButton => this.driver.FindElement(By.Id("createRoom"));
        public IWebElement EditRoomButton => this.driver.FindElement(By.XPath("//button[.='Edit']"));
        public IWebElement DescriptionTestArea => this.driver.FindElement(By.Id("description"));
        public IWebElement ImageInput => this.driver.FindElement(By.Id("image"));
        public IWebElement UpdateButton => this.driver.FindElement(By.Id("update"));
        // public IWebElement UpdatedDescription => this.driver.FindElement(By.CssSelector("div:nth-child(2) > p > span"));
        // public IWebElement UpdatedDescription => this.driver.FindElement(By.XPath("//p[contains(text(),'Description: ')]/.//span"));
        public IWebElement UpdatedDescription => wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("div:nth-child(2) > p > span")));
        public IWebElement UpdatedImage => this.driver.FindElement(By.CssSelector("div:nth-child(2) > img"));
        //public IWebElement UpdatedImage => this.driver.FindElement(By.XPath("/img"));
    }
}
