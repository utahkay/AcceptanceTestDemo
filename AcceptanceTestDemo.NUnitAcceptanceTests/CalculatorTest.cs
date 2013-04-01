using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AcceptanceTestDemo.Domain;
using NUnit.Framework;

namespace AcceptanceTestDemo.NUnitAcceptanceTests
{
    [TestFixture]
    public class CalculatorTest
    {
        [Test]
        public void TestAdd()
        {
            var calc = new Calculator();
            Assert.That(calc.Add(new List<decimal> {50, 70}), Is.EqualTo(120));
        }
    }
}
