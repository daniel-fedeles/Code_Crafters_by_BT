using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;
using SpecFlowProject2.Data;
using SpecFlowProject2.Controls;
using NUnit.Framework;
using OpenQA.Selenium.Interactions;
using System.Collections.ObjectModel;
using System.Security.Policy;

namespace SpecFlowProject2.StepDefinitions
{
    [Binding]
    public sealed class BookRoomStepDefinitions
    {
        private readonly ScenarioContext scenarioContext;
        private readonly IWebDriver driver;
        private readonly TestData testData;
        private Navigator navigator;
        private const string url = "https://automationintesting.online/";

        BookRoomControls bookRoomControls => new BookRoomControls(driver);
        LoginControls loginControls => new LoginControls(driver);
        int InitialRoomSectionsOnPage;
        public BookRoomStepDefinitions(ScenarioContext scenarioContext, IWebDriver driver, TestData testData)
        {
            this.scenarioContext = scenarioContext;
            this.testData = testData;
            this.driver = driver;
            navigator = new Navigator(driver);
            navigator.GoTo(new Uri(url));
        }



        [Given(@"some random valid room booking details are generated")]
        public void GivenSomeRandomValidRoomBookingDetailsAreGenerated()
        {
            testData.MyRoomBooking.FirstName = TestUtilities.GenerateRandomString(10);
            testData.MyRoomBooking.LastName = TestUtilities.GenerateRandomString(10);
            testData.MyRoomBooking.PhoneNumber = $"0{TestUtilities.GenerateRandomNumber(11)}";
            testData.MyRoomBooking.Email = $"{TestUtilities.GenerateRandomString(8)}@{TestUtilities.GenerateRandomString(8)}.com";
        }

        [Given(@"at least (.*) room exists in the hotel")]
        public void GivenAtLeastRoomExistsInTheHotel(int p0)
        {
            loginControls.LetMeHackButton.Click();
            Assert.True(bookRoomControls.RoomInformation.Count > 0, "No rooms currently exist in the hotel");
        }

        [When(@"the book a room button is clicked")]
        public void WhenTheBookARoomButtonIsClicked()
        {
            InitialRoomSectionsOnPage = bookRoomControls.RoomInformation.Count; //When room section expands, an additional section is created on the page
            bookRoomControls.BookRoomButton.Click();
        }

        [Then(@"the room info section should appear")]
        public void ThenTheRoomInfoSectionShouldAppear()
        {
            Assert.True(bookRoomControls.RoomInformation.Count > InitialRoomSectionsOnPage);
        }

        [When(@"a valid date range is selected from the book room calendar")]
        public void WhenAValidDateRangeIsSelectedFromTheBookRoomCalendar()
        {

            bool dateSet = false;
            bool available = false;

            bool todayDay = false;
            int dayNr = 0;
            bool grayedOutDays = false;

            try
            {
                todayDay = bookRoomControls.Today.Displayed;
                string x = bookRoomControls.Today.Text;
                int.TryParse(x, out dayNr);
            }
            catch (NoSuchElementException)
            {
                todayDay = false;
            }

            if (dayNr>14 && todayDay) {

                bookRoomControls.NextMonth.Click();
            }

            while (!dateSet)
            {
                var testingMonth = bookRoomControls.CalendarRows;
                var month = bookRoomControls.CalendarMonth.Text;
                foreach (IWebElement week in testingMonth)
                {
                    var monthh = bookRoomControls.CalendarMonth.Text;
                    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
                    var x = week.Text;
                    week.ScrollToElement(driver);
                    try
                    {
                        available = !week.FindElement(By.XPath(".//div[@title='Unavailable']")).Displayed;
                    }
                    catch (NoSuchElementException)
                    {
                        available = true;                        
                    }
                    try
                    {
                        grayedOutDays = week.FindElement(bookRoomControls.DaysFromLastMonthInFirstWeek).Displayed;
                    }
                    catch (NoSuchElementException)
                    {

                        grayedOutDays = false;
                    }
                    driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(TestUtilities.Timeout);

                    if (available && (!grayedOutDays))
                    {
                        week.ScrollToElement(driver);
                        SelectDateRangeInWeek(week);
                        dateSet = true;
                        break;
                    }
                    else if(!available)
                    {
                        bookRoomControls.NextMonth.Click();
                    }
                }

             //   bookRoomControls.NextMonth.Click();
            }
        }

        public void SelectDateRangeInWeek(IWebElement week)
        {
            ReadOnlyCollection<IWebElement> days = week.FindElements(bookRoomControls.DaysInWeek);
            IWebElement day1 = days[1];
            IWebElement day2 = days[days.Count - 3];

            var action = new Actions(driver);

            action.ClickAndHold(day2);
            action.MoveToElement(day2);
            action.MoveToElement(day1);
            action.DragAndDrop(day1, day2);
            action.Perform();
        }


        [When(@"the book room form is completed and submitted")]
        public void WhenTheBookRoomFormIsCompletedAndSubmitted()
        {
            bookRoomControls.FirstNameTextbox.SendKeys(testData.MyRoomBooking.FirstName);
            bookRoomControls.LastNameTextbox.SendKeys(testData.MyRoomBooking.LastName);
            bookRoomControls.EmailTextbox.SendKeys(testData.MyRoomBooking.Email);
            bookRoomControls.PhoneTextbox.SendKeys(testData.MyRoomBooking.PhoneNumber);
            bookRoomControls.SubmitBookingButton.Click();
        }

        [Then(@"the successful room booking message should appear")]
        public void ThenTheSuccessfulRoomBookingMessageShouldAppear()
        {
            Assert.AreEqual("Booking Successful!", bookRoomControls.SuccessfulBookingMessage.Text);
        }
    }
}
