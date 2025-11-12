namespace EmployeeSystem
{
    public static class Utilities
    {
        // Check if integer is positive and greater than 0
        public static bool IsNonZeroPositive(int value)
        {
            return value > 0;
        }

        // Check if double is positive and greater than 0
        public static bool IsNonZeroPositive(double value)
        {
            return value > 0;
        }

        // Check if date is not in the future
        public static bool IsValidPastDate(DateTime date)
        {
            return date.Date <= DateTime.Today;
        }

        // Check if date is within last 90 days
        public static bool IsWithinLast90Days(DateTime date)
        {
            DateTime ninetyDaysAgo = DateTime.Today.AddDays(-90);
            return date.Date >= ninetyDaysAgo && date.Date <= DateTime.Today;
        }

        // Check if hours worked is reasonable (1-16 hours per shift)
        public static bool IsValidShiftHours(double hours)
        {
            return hours >= 1.0 && hours <= 16.0;
        }
    }
}