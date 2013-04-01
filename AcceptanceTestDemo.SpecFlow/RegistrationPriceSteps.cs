using System.Linq;
using AcceptanceTestDemo.Application;
using AcceptanceTestDemo.Controllers;
using AcceptanceTestDemo.Repositories;
using AcceptanceTestDemo.Utilities;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace AcceptanceTestDemo.SpecFlow
{
    [Binding]
    public class RegistrationPriceSteps
    {
        readonly ConferencesController conferencesController = new ConferencesController();
        readonly ConferencesRepository conferencesRepository = new ConferencesRepository();

        int NumberOfRegistrations;
        string ConferenceName;
        string CouponCode = "";
        int DaysUntilConference = 10;

        [Given(@"I am purchasing (.*) registration for (.*)")]
        [Given(@"I am purchasing (.*) registrations for (.*)")]
        public void GivenIAmPurchasingRegistrationFor(int numberOfRegistrations, string conferenceName)
        {
            NumberOfRegistrations = numberOfRegistrations;
            ConferenceName = conferenceName;
        }
        
        [Given(@"I provide coupon code (.*)")]
        public void GivenIProvideCouponCode(string couponCode)
        {
            CouponCode = couponCode;
        }
        
        [Given(@"there are (.*) days until the conference")]
        public void GivenThereAreDaysUntilTheConference(int daysUntilConference)
        {
            DaysUntilConference = daysUntilConference;
        }
        
        [Then(@"the total price should be (.*)")]
        public void ThenTheTotalPriceShouldBe(decimal expectedTotalPrice)
        {
            var result = GetRegistrationPrice();
            Assert.That(result.TotalPrice, Is.EqualTo(expectedTotalPrice));
        }

        [Then(@"the unit price should be (.*)")]
        public void ThenTheUnitPriceShouldBe(decimal expectedUnitPrice)
        {
            var result = GetRegistrationPrice();
            Assert.That(result.UnitPrice, Is.EqualTo(expectedUnitPrice));
        }

        RegistrationPriceResult GetRegistrationPrice()
        {
            var eventDate = conferencesRepository.LoadAll().First(x => x.ConferenceName.Equals(ConferenceName)).EventDate;
            UtcTime.Stop(eventDate.AddDays(-DaysUntilConference));
            return conferencesController.RegistrationPrice(ConferenceName, NumberOfRegistrations, CouponCode).Data as RegistrationPriceResult;
        }
    }
}
