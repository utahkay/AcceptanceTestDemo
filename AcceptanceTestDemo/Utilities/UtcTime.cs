using System;

namespace AcceptanceTestDemo.Utilities
{
    public static class UtcTime
    {
        static Func<DateTime> utcNow = () => DateTime.UtcNow;

        public static DateTime Now
        {
            get { return utcNow(); }
        }

        public static DateTime Today
        {
            get { return Now.Date; }
        }

        public static void Stop(DateTime dateTime)
        {
            utcNow = () => dateTime;
        }

        public static void RestartTime()
        {
            utcNow = () => DateTime.UtcNow;
        }
    }
}