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

Now, do we like this better? We've consolidated the data and can better see what test cases we have (and what ones we might be missing). We've also lost some information, such as the names of the tests, which were kind of helpful.

In London some years ago, several developers began feeling that "testing" might be the wrong metaphor. Dan North started writing and creating tools that allowed you to write "behavior specs" instead of "tests." JBehave was ported to RSpec StoryRunner, which ended up being rewritten as Cucumber. In Cucumber, you specify behaviors in a natural-like language, and use regular expressions to pull the data values out for your test, er, spec code to use.

http://cukes.info/

In __RegistrationPrice.feature__ you can see the specs that are equivalent to our four NUnit test cases. 

> Feature: RegistrationPrice
>   In order to decide whether to attend the conference
> 	As a potential attendee
> 	I want to know my registration price
> 
> @mytag
> Scenario: Single registration
> 	Given I am purchasing 1 registration for Agile Austria
> 	Then the total price should be 500
> 
> Scenario: Group registration
>   Given I am purchasing 5 registrations for Agile Austria
> 	Then the unit price should be 450
> 	And the total price should be 2250
> 
> Scenario: Discount code
>   Given I am purchasing 1 registration for Agile Austria
> 	And I provide coupon code HALFOFF
> 	Then the total price should be 250
> 
> Scenario: Late registration
>   Given I am purchasing 1 registration for Agile Austria
> 	And there are 6 days until the conference
> 	Then the total price should be 700

This text is backed by __RegistrationPriceSteps.cs__, which we'll need to implement.

Resources

Table based testing technique:
http://www.concordion.org/Technique.html

FitNesse:
http://fitnesse.org/
http://gojko.net/fitnesse/
http://tech.groups.yahoo.com/group/fitnesse/



