using System.Collections.Generic;
using System.Linq;
using AcceptanceTestDemo.Repositories;
using AcceptanceTestDemo.Utilities;

namespace AcceptanceTestDemo.Application
{
    public class ConferencesFacade
    {
        readonly ConferencesRepository conferencesRepository = new ConferencesRepository();

        public RegistrationPriceResult RegistrationPrice(string conferenceName, int numRegistrations, string couponCode)
        {
            var conference = conferencesRepository.Load(conferenceName);
            var registrationDate = UtcTime.Now;
            return new RegistrationPriceResult
                       {
                           UnitPrice = conference.UnitPrice(numRegistrations, couponCode, registrationDate),
                           TotalPrice = conference.TotalPrice(numRegistrations, couponCode, registrationDate)
                       };
        }

        public IEnumerable<ConferenceResult> AllConferences()
        {
            var conferences = conferencesRepository.LoadAll();
            return conferences.Select(c => new ConferenceResult {ConferenceName = c.ConferenceName, EventDate = c.EventDate, Location = c.Location});
        }
    }
}