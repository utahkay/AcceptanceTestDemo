using TechTalk.SpecFlow;

namespace AcceptanceTestDemo.SpecFlow
{
    [Binding]
    public class RegistrationPriceSteps
    {
        [Given(@"I am purchasing (.*) registration for (.*)")]
        [Given(@"I am purchasing (.*) registrations for (.*)")]
        public void GivenIAmPurchasingRegistrationFor(int numberOfRegistrations, string conferenceName)
        {
            ScenarioContext.Current.Pending();
        }
        
        [Given(@"I provide coupon code (.*)")]
        public void GivenIProvideCouponCode(string couponCode)
        {
            ScenarioContext.Current.Pending();
        }
        
        [Given(@"there are (.*) days until the conference")]
        public void GivenThereAreDaysUntilTheConference(int daysUntilConference)
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"the total price should be (.*)")]
        public void ThenTheTotalPriceShouldBe(decimal expectedTotalPrice)
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"the unit price should be (.*)")]
        public void ThenTheUnitPriceShouldBe(decimal expectedUnitPrice)
        {
            ScenarioContext.Current.Pending();
        }
    }
}
