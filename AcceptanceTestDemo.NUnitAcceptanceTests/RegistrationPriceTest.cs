using System;
using System.Linq;
using AcceptanceTestDemo.Application;
using AcceptanceTestDemo.Controllers;
using AcceptanceTestDemo.Repositories;
using AcceptanceTestDemo.Utilities;
using NUnit.Framework;

namespace AcceptanceTestDemo.NUnitAcceptanceTests
{
    [TestFixture]
    public class RegistrationPriceTest
    {
        readonly ConferencesController conferencesController = new ConferencesController();
        readonly ConferencesRepository conferencesRepository = new ConferencesRepository();
        DateTime EventDate;

        [SetUp]
        public void SetDateForNormalOnTimeRegistration()
        {
            EventDate = conferencesRepository.LoadAll().First(x => x.ConferenceName.Equals("Agile Austria")).EventDate;
            UtcTime.Stop(EventDate.AddDays(-10));
        }

        [Test]
        public void SingleRegistration()
        {
            var result = conferencesController.RegistrationPrice(
                conferenceName: "Agile Austria",
                numRegistrations: 1);
            var value = result.Data as RegistrationPriceResult;
            Assert.That(value.TotalPrice, Is.EqualTo(500m));
        }

        [Test]
        public void GroupRegistration()
        {
            var result = conferencesController.RegistrationPrice(
                conferenceName: "Agile Austria",
                numRegistrations: 5);
            var value = result.Data as RegistrationPriceResult;
            Assert.That(value.UnitPrice, Is.EqualTo(450m));
            Assert.That(value.TotalPrice, Is.EqualTo(2250m));
        }

        [Test]
        public void DiscountCode()
        {
            var result = conferencesController.RegistrationPrice(
                conferenceName: "Agile Austria",
                numRegistrations: 1,
                couponCode: "HALFOFF");
            var value = result.Data as RegistrationPriceResult;
            Assert.That(value.TotalPrice, Is.EqualTo(250m));
        }

        [Test]
        public void LateRegistration()
        {
            UtcTime.Stop(EventDate.AddDays(-6));
            var result = conferencesController.RegistrationPrice(
                conferenceName: "Agile Austria",
                numRegistrations: 1);
            var value = result.Data as RegistrationPriceResult;
            Assert.That(value.TotalPrice, Is.EqualTo(700m));
        }
    }
}