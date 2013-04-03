<table><tr><td><img src="http://cdn.ttgtmedia.com/rms/onlineImages/sSoftwareQuality_testautomation_strategy.jpg"></td>
<td><img src="http://upload.wikimedia.org/wikipedia/commons/6/6d/USDA_Food_Pyramid.gif"></td></tr></table>

Acceptance testing and vegetables have a lot in common. Most of us agree we ought to be doing more acceptance testing... and in practice we often find other things to do. 

What benefits would you hope to get from automated acceptance testing? That you don't get from other kinds of testing?

This demo app shows the use of three different tools for acceptance testing: NUnit, SpecFlow, and FitNesse.

I was too lazy to write a UI, so you'll have to imagine this as a web app where you can register for various conferences. The app allows you to preview your registration price, given certain information:
  
  - Which conference (Not surprisingly, different conferences have different prices)
  - Number of attendees you're registering (There may be a group discount)
  - Discount code, if any
  - Date (Some conferences adjust price for earlybird/normal/late registration)

The easiest way to get started writing acceptance tests is just pick up your favorite unit testing tool, and write a test that spans the system from top to bottom. In __RegistrationPriceTest__, you'll see someone has already written a set of acceptance tests using NUnit. 

````cs
        [Test]
        public void SingleRegistration()
        {
            var result = conferencesController.RegistrationPrice(
                conferenceName: "Agile Austria",
                numRegistrations: 1);
            var value = result.Data as RegistrationPriceResult;
            Assert.That(value.TotalPrice, Is.EqualTo(500m));
        }
        
        ...
````

There's a lot of repetition in these tests, which makes it hard to scan them quickly and really see what they're testing. But we have a tool for that, we can create NUnit TestCases to remove the duplication.

````cs
        [TestCase("Agile Austria", 1, 500)]
        public void SingleRegistration(string conferenceName, int numRegistrations, decimal expectedTotalPrice)
        {
            var result = conferencesController.RegistrationPrice(
                conferenceName: conferenceName,
                numRegistrations: numRegistrations);
            var value = result.Data as RegistrationPriceResult;
            Assert.That(value.TotalPrice, Is.EqualTo(expectedTotalPrice));
        }
````

As we keep going, we find we have to introduce more parameters. We can also see every test uses the same conference name, so we can save one parameter by extracting that as a constant. We should also rename the test at this point.

````cs
        const string ConferenceName = "Agile Austria";
        
        ...
        
        [TestCase(1, "", 500, 500)]
        [TestCase(5, "", 450, 2250)]
        [TestCase(1, "HALFOFF", 250, 250)]
        public void RegistrationPrice(int numRegistrations, string couponCode, decimal expectedUnitPrice, decimal expectedTotalPrice)
        {
            var result = conferencesController.RegistrationPrice(
                conferenceName: ConferenceName,
                numRegistrations: numRegistrations,
                couponCode: couponCode);
            var value = result.Data as RegistrationPriceResult;
            Assert.That(value.TotalPrice, Is.EqualTo(expectedTotalPrice));
        }
        
````

As we get to the test case for late registration, we make use of the provided utility to force our current time to a specific time. This kind of time manipulation is always handy for testing. We notice that previously, the setup method was setting our time to "10 days before the conference" so we'll use that on our other test cases.

````cs
        [SetUp]
        public void SetDateForNormalOnTimeRegistration()
        {
            EventDate = conferencesRepository.LoadAll().First(x => x.ConferenceName.Equals(ConferenceName)).EventDate;
        }
        
        ...

        [TestCase(1, "", 10, 500, 500)]
        [TestCase(5, "", 10, 450, 2250)]
        [TestCase(1, "HALFOFF", 10, 250, 250)]
        [TestCase(1, "", 6, 700, 700)]
        public void RegistrationPrice(int numRegistrations, string couponCode, int daysTillConference, decimal expectedUnitPrice, decimal expectedTotalPrice)
        {
            UtcTime.Stop(EventDate.AddDays(-daysTillConference));
            var result = conferencesController.RegistrationPrice(
                conferenceName: ConferenceName,
                numRegistrations: numRegistrations,
                couponCode: couponCode);
            var value = result.Data as RegistrationPriceResult;
            Assert.That(value.TotalPrice, Is.EqualTo(expectedTotalPrice));
        }
````



Resources

Table based testing technique:
http://www.concordion.org/Technique.html

FitNesse:
http://fitnesse.org/
http://gojko.net/fitnesse/
http://tech.groups.yahoo.com/group/fitnesse/



