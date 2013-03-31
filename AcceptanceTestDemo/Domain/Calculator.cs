using System;
using System.Collections.Generic;
using System.Linq;

namespace AcceptanceTestDemo.Domain
{
    public class Calculator
    {
        public decimal Add(List<decimal> addends)
        {
            return addends.Sum();
        }
    }
}