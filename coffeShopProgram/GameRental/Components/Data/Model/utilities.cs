namespace GameRentalSystem
{
    public static class Utilities
    {
        public static bool IsNonZeroPositive(int value)
        {
            return value > 0;
        }

        // Add this new method for doubles
        public static bool IsNonZeroPositive(double value)
        {
            return value > 0;
        }
    }
}