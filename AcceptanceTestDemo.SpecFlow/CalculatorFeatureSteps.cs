using System;
using System.Collections.Generic;
using AcceptanceTestDemo.Domain;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace AcceptanceTestDemo.SpecFlow
{
    [Binding]
    public class CalculatorFeatureSteps
    {
        readonly List<decimal> addends = new List<decimal>();
        decimal result;
        Exception exception;


        [Given(@"I have entered (.*) into the calculator")]
        public void GivenIHaveEnteredIntoTheCalculator(decimal operand)
        {
            addends.Add(operand);
        }

        [When(@"I press add")]
        public void WhenIPressAdd()
        {
            result = new Calculator().Add(addends);
        }

        [Then(@"the result should be (.*) on the screen")]
        public void ThenTheResultShouldBeOnTheScreen(decimal expectedResult)
        {
            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}