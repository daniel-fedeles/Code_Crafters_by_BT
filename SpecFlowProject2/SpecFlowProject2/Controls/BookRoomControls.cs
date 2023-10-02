using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowProject2.Controls
{
    [Binding]
    public sealed class BookRoomControls
    {
        public readonly IWebDriver driver;
        public BookRoomControls(IWebDriver driver)
        {
            this.driver = driver;
        }

        public IWebElement BookRoomButton => this.driver.FindElement(By.XPath("//button[@class='btn btn-outline-primary float-right openBooking']"));
        public ReadOnlyCollection<IWebElement> RoomInformation => this.driver.FindElements(By.XPath("//div[@class='row hotel-room-info']"));
        public int InitialRoomSectionsOnPage = 0;

        public IWebElement Calendar => this.driver.FindElement(By.XPath("//div[@class='rbc-calendar']"));
        public IWebElement CalendarMonth => this.driver.FindElement(By.XPath(".//span[@class='rbc-toolbar-label']"));
        public IWebElement Today => this.driver.FindElement(By.XPath("//div[@class='rbc-date-cell rbc-now rbc-current']/button"));
        public ReadOnlyCollection<IWebElement> CalendarRows => Calendar.FindElements(By.XPath(".//div[@class='rbc-month-row']"));
        public By DaysInWeek => By.XPath(".//div[@class='rbc-day-bg']");
        public By DaysFromLastMonthInFirstWeek => By.XPath(".//div[@class='rbc-day-bg rbc-off-range-bg']");
        public IWebElement FirstNameTextbox => this.driver.FindElement(By.Name("firstname"));
        public IWebElement LastNameTextbox => this.driver.FindElement(By.Name("lastname"));
        public IWebElement EmailTextbox => this.driver.FindElement(By.Name("email"));
        public IWebElement PhoneTextbox => this.driver.FindElement(By.Name("phone"));
        public IWebElement SubmitBookingButton => this.driver.FindElement(By.XPath("//button[@class='btn btn-outline-primary float-right book-room']"));
        public IWebElement NextMonth => this.driver.FindElement(By.XPath("//button[text()='Next']"));
        public IWebElement SuccessfulBookingMessage => this.driver.FindElement(By.XPath("//div[@class='form-row']//h3"));




    }
}
