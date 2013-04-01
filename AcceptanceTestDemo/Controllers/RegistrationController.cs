using System.Web.Mvc;
using AcceptanceTestDemo.Application;

namespace AcceptanceTestDemo.Controllers
{
    public class RegistrationController : Controller
    {
        readonly RegistrationFacade registrationFacade = new RegistrationFacade();

        public virtual JsonResult CalculatePrice(string conferenceName, int numRegistrations, string couponCode = "")
        {
            var price = registrationFacade.CalculatePrice(conferenceName, numRegistrations, couponCode);
            return Json(price);
        }
    }
}