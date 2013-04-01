using AcceptanceTestDemo.Repositories;
using AcceptanceTestDemo.Utilities;

namespace AcceptanceTestDemo.Application
{
    public class RegistrationFacade
    {
        readonly ConferencesRepository conferencesRepository = new ConferencesRepository();

        public CalculatePriceResult CalculatePrice(string conferenceName, int numRegistrations, string couponCode)
        {
            var conference = conferencesRepository.Load(conferenceName);
            return new CalculatePriceResult
                       {
                           UnitPrice = conference.UnitPrice(numRegistrations, couponCode, registrationDate: UtcTime.Now),
                           TotalPrice = conference.TotalPrice(numRegistrations, couponCode, registrationDate: UtcTime.Now)
                       };
        }
    }
}