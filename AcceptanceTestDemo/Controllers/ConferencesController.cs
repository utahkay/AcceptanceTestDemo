using System.Web.Mvc;
using AcceptanceTestDemo.Application;

namespace AcceptanceTestDemo.Controllers
{
    public class ConferencesController : Controller
    {
        readonly ConferencesFacade conferencesFacade = new ConferencesFacade();

        public virtual JsonResult RegistrationPrice(string conferenceName, int numRegistrations, string couponCode = "")
        {
            var price = conferencesFacade.RegistrationPrice(conferenceName, numRegistrations, couponCode);
            return Json(price);
        }

        public virtual JsonResult Conferences()
        {
            var conferences = conferencesFacade.AllConferences();
            return Json(conferences);
        }
    }
}