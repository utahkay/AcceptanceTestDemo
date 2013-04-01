using System.Collections.Generic;
using System.Linq;
using AcceptanceTestDemo.Application;
using AcceptanceTestDemo.Controllers;

namespace AcceptanceTestDemo.Fitnesse
{
    public class ConferenceListingFixture
    {
        public string Conference;

        readonly IEnumerable<ConferenceResult> conferences;

        public ConferenceListingFixture()
        {
            var controller = new ConferencesController();
            var conferencesJson = controller.Conferences();
            conferences = conferencesJson.Data as IEnumerable<ConferenceResult>;
        }

        public string Location()
        {
            return conferences.First(x => x.ConferenceName.Equals(Conference)).Location;
        }

        public string Date()
        {
            return conferences.First(x => x.ConferenceName.Equals(Conference)).EventDate.ToString("yyyy-MM-dd");
        }
    }
}