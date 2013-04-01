using System;
using System.Collections.Generic;
using System.Linq;
using AcceptanceTestDemo.Utilities;

namespace AcceptanceTestDemo.Domain
{
    public class Conference
    {
        public DateTime EventDate;
        public readonly Dictionary<int, decimal> BasePrices;
        public readonly Dictionary<string, decimal> CouponDiscounts;
        public readonly Dictionary<int, decimal> GroupDiscounts;

        public Conference(DateTime eventDate, Dictionary<int, decimal> basePrices, Dictionary<int, decimal> groupDiscounts, Dictionary<string, decimal> couponDiscounts)
        {
            EventDate = eventDate;
            BasePrices = basePrices;
            GroupDiscounts = groupDiscounts;
            CouponDiscounts = couponDiscounts;
        }

        public decimal BasePrice(DateTime registrationDate)
        {
            var daysUntilConference = (EventDate - registrationDate).TotalDays;
            return BasePrices.OrderByDescending(x => x.Key).First(x => daysUntilConference >= x.Key).Value;
        }

        public decimal UnitPrice(int numRegistrations, string couponCode, DateTime registrationDate)
        {
            if (couponCode == null) couponCode = "";
            if (numRegistrations < 0) numRegistrations = 0;

            var basePrice = BasePrice(registrationDate);
            var groupDiscounts = GroupDiscounts;
            var couponDiscounts = CouponDiscounts;

            var groupDiscount = groupDiscounts.OrderByDescending(x => x.Key).First(x => numRegistrations >= x.Key).Value;
            var couponDiscount = couponDiscounts.First(x => x.Key.Equals(couponCode)).Value;
            return basePrice * (1 - groupDiscount) * (1 - couponDiscount);
        }

        public decimal TotalPrice(int numRegistrations, string couponCode, DateTime registrationDate)
        {
            if (couponCode == null) couponCode = "";
            if (numRegistrations < 0) numRegistrations = 0;

            return numRegistrations * UnitPrice(numRegistrations, couponCode, registrationDate);
        }
    }
}