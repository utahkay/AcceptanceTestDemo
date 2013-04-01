using System;
using AcceptanceTestDemo.Application;
using AcceptanceTestDemo.Controllers;
using AcceptanceTestDemo.Utilities;
using NUnit.Framework;

namespace AcceptanceTestDemo.NUnitAcceptanceTests
{
    [TestFixture]
    public class RegistrationPriceTest
    {
        readonly RegistrationController registrationController = new RegistrationController();
        const string ConferenceName = "AgileExperience";
        DateTime ConferenceDate = new DateTime(2013, 07, 13);

        [SetUp]
        public void SetDateForNormalOnTimeRegistration()
        {
            UtcTime.Stop(ConferenceDate.AddDays(-10));
        }

        [Test]
        public void TestSingleRegistration()
        {
            var result = registrationController.CalculatePrice(ConferenceName, 1);
            var value = result.Data as CalculatePriceResult;
            Assert.That(value.TotalPrice, Is.EqualTo(500m));
        }

        [Test]
        public void GroupRegistration()
        {
            var result = registrationController.CalculatePrice(ConferenceName, 5);
            var value = result.Data as CalculatePriceResult;
            Assert.That(value.UnitPrice, Is.EqualTo(450m));
            Assert.That(value.TotalPrice, Is.EqualTo(2250m));
        }

        [Test]
        public void DiscountCode()
        {
            var result = registrationController.CalculatePrice(ConferenceName, 1, "HALFOFF");
            var value = result.Data as CalculatePriceResult;
            Assert.That(value.TotalPrice, Is.EqualTo(250m));
        }

        [Test]
        public void LateRegistration()
        {
            UtcTime.Stop(ConferenceDate.AddDays(-6));
            var result = registrationController.CalculatePrice(ConferenceName, 1);
            var value = result.Data as CalculatePriceResult;
            Assert.That(value.TotalPrice, Is.EqualTo(700m));
        }
    }
}