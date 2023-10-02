using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SpecFlowProject2.Controls;
using SpecFlowProject2.Data;


namespace SpecFlowProject2.StepDefinitions
{
    [Binding]
    public class AdminCreateNewDoubleRoomStepDefinitions
    {
        private readonly ScenarioContext scenarioContext;
        private readonly IWebDriver driver;
        private readonly TestData testData;
        private Navigator navigator;
        private const string url = "https://automationintesting.online/#/admin/ ";
        private string roomName = "";
        private int roomNumber = 0;

        AdminControls adminControls => new AdminControls(driver);
        LoginControls loginControls => new LoginControls(driver);

        public AdminCreateNewDoubleRoomStepDefinitions(ScenarioContext scenarioContext, IWebDriver driver, TestData testData)
        {
            this.scenarioContext = scenarioContext;
            this.driver = driver;
            this.testData = testData;
            navigator = new Navigator(driver);
            navigator.GoTo(new Uri(url));
        }


        [Given(@"the user has logged into the admin section with ""([^""]*)"" and ""([^""]*)""")]
        public void GivenTheUserHasLoggedIntoTheAdminSectionWithAnd(string username, string password)
        {
            loginControls.LetMeHackButton.Click();
            loginControls.UsernameTextbox.SendKeys(username);
            loginControls.PasswordTextbox.SendKeys(password);
            loginControls.LoginButton.Click();
        }


        [Given(@"the admin page is opened")]
        public void GivenTheAdminPageIsOpened()
        {
            Assert.True(adminControls.RoomNameField.Displayed);
        }

        [When(@"the new room with ""([^""]*)"" and amenities ""([^""]*)"" ""([^""]*)"" ""([^""]*)"" is created")]
        public void WhenTheNewRoomWithAndAmenitiesIsCreated(string type, string p1, string p2, string p3)
        {
            var numberOfRooms = adminControls.Rooms.Count;
            Console.WriteLine("Rooms "+ numberOfRooms);

            //roomName = adminControls.FirstRoomNameField.Text;
            //int.TryParse(roomName, out roomNumber);
            roomNumber = 100 + numberOfRooms + 1;
            adminControls.RoomNameField.SendKeys(roomNumber.ToString());
            var select = new SelectElement(adminControls.RoomTypeSelect);
            select.SelectByValue(type);
            adminControls.RoomPriceField.SendKeys((roomNumber+100).ToString());
            adminControls.RoomWifiCheckbox.Click();
            adminControls.RoomTvCheckbox.Click();
            adminControls.RoomSafeCheckbox.Click();
            Assert.True(adminControls.RoomWifiCheckbox.Selected, p1);
            Assert.True(adminControls.RoomTvCheckbox.Selected, p2);
            Assert.True(adminControls.RoomSafeCheckbox.Selected, p3);
            adminControls.CreateRoomButton.Click();
        }

        [Then(@"the new room with ""([^""]*)"" and amenities ""([^""]*)"" ""([^""]*)"" ""([^""]*)"" is displayed on admin page")]
        public void ThenTheNewRoomWithAndAmenitiesIsDisplayedOnAdminPage(string type, string p1, string p2, string p3)
        {
            throw new PendingStepException();
        }

        [When(@"selecting the room")]
        public void WhenSelectingTheRoom()
        {
            adminControls.SelectRoomName(roomNumber.ToString()).Click();
        }

        [Then(@"edit button is displayed on page")]
        public void ThenEditButtonIsDisplayedOnPage()
        {
            Assert.True(adminControls.EditRoomButton.Displayed);
        }

        [When(@"enter edit mode")]
        public void WhenEnterEditMode()
        {
            adminControls.EditRoomButton.Click();
        }

        [Then(@"description field is displayed")]
        public void ThenDescriptionFieldIsDisplayed()
        {
            Assert.True(adminControls.DescriptionTestArea.Displayed);
            
        }

        [Then(@"image field is displayed")]
        public void ThenImageFieldIsDisplayed()
        {
            Assert.True(adminControls.ImageInput.Displayed);
        }

        [Then(@"update ""([^""]*)"" and ""([^""]*)""")]
        public void ThenUpdateAnd(string description, string image)
        {
            adminControls.DescriptionTestArea.Clear();
            adminControls.DescriptionTestArea.SendKeys(description);
            adminControls.ImageInput.Clear();
            adminControls.ImageInput.SendKeys(image);
        }

        [When(@"clicking update button")]
        public void WhenClickingUpdateButton()
        {
           adminControls.UpdateButton.Click();
        }

        [Then(@"description room should be updated with ""([^""]*)""")]
        public void ThenDescriptionRoomShouldBeUpdatedWith(string p0)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            var a = adminControls.UpdatedDescription;
            Assert.AreEqual(p0, a.Text);

        }

        [Then(@"image room with should be updated ""([^""]*)""")]
        public void ThenImageRoomWithShouldBeUpdated(string p0)
        {
            var a = adminControls.UpdatedImage.GetAttribute("src");
            Assert.AreEqual(p0, a);
        }

    }
}
