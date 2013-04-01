using System.Linq;
using AcceptanceTestDemo.Application;
using AcceptanceTestDemo.Controllers;
using AcceptanceTestDemo.Repositories;
using AcceptanceTestDemo.Utilities;

namespace AcceptanceTestDemo.Fitnesse
{
    public class RegistrationPriceFixture
    {
        public string ConferenceName;
        public int NumberOfRegistrations;
        public string CouponCode;
        public int DaysUntilConference;

        readonly ConferencesController conferencesController = new ConferencesController();
        readonly ConferencesRepository conferencesRepository = new ConferencesRepository();
        RegistrationPriceResult result;

        public int UnitPrice()
        {
            return (int)result.UnitPrice;
        }
        
        public int TotalPrice()
        {
            return (int)result.TotalPrice;
        }

        public void Execute()
        {
            result = GetRegistrationPrice();
        }

        RegistrationPriceResult GetRegistrationPrice()
        {
            var eventDate = conferencesRepository.LoadAll().First(x => x.ConferenceName.Equals(ConferenceName)).EventDate;
            UtcTime.Stop(eventDate.AddDays(-DaysUntilConference));
            return conferencesController.RegistrationPrice(ConferenceName, NumberOfRegistrations, CouponCode).Data as RegistrationPriceResult;
        }
    }
}