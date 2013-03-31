using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AcceptanceTestDemo.Fitnesse
{
    public class CalculatorFixture
    {
        public decimal op1;
        public decimal op2;

        public decimal Add()
        {
            return op1 + op2;
        }
    }
}
