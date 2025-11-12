namespace GameRentalSystem
{
    public class GameRental
    {
        private string _RentalId;
        private string _GameTitle;
        private string _CustomerName;
        private int _RentalDays;
        private double _DailyRate;

        private const int MIN_DAYS = 1;
        private const int MAX_DAYS = 30;
        private const double MIN_RATE = 1.00;
        private const double MAX_RATE = 100.00;

        public string RentalId
        {
            get { return _RentalId; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("Rental ID required");
                _RentalId = value.Trim();
            }
        }

        public string GameTitle
        {
            get { return _GameTitle; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("Game title required");
                _GameTitle = value.Trim();
            }
        }

        public string CustomerName
        {
            get { return _CustomerName; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("Customer name required");
                _CustomerName = value.Trim();
            }
        }

        public GamePlatform Platform { get; private set; }

        public int RentalDays
        {
            get { return _RentalDays; }
            private set
            {
                if (!Utilities.IsNonZeroPositive(value))
                    throw new ArgumentException("Rental days must be positive");

                if (value < MIN_DAYS || value > MAX_DAYS)
                    throw new ArgumentException($"Rental days must be between {MIN_DAYS} and {MAX_DAYS}");

                _RentalDays = value;
            }
        }

        public double DailyRate
        {
            get { return _DailyRate; }
            private set
            {
                if (value < MIN_RATE || value > MAX_RATE)
                    throw new ArgumentException($"Rate must be between ${MIN_RATE} and ${MAX_RATE}");

                _DailyRate = value;
            }
        }

        public GameRental(string rentalId, string gameTitle, string customerName,
                         GamePlatform platform, int rentalDays, double dailyRate)
        {
            RentalId = rentalId;
            GameTitle = gameTitle;
            CustomerName = customerName;
            Platform = platform;
            RentalDays = rentalDays;
            DailyRate = dailyRate;
        }

        public override string ToString()
        {
            return $"{RentalId},{GameTitle},{CustomerName},{Platform},{RentalDays},{DailyRate}";
        }

        public static GameRental Parse(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException("No data supplied");

            string[] data = value.Split(',');

            if (data.Length != 6)
                throw new FormatException($"Expected 6 fields, got {data.Length}");

            return new GameRental(
                data[0].Trim(),
                data[1].Trim(),
                data[2].Trim(),
                (GamePlatform)Enum.Parse(typeof(GamePlatform), data[3]),
                int.Parse(data[4]),
                double.Parse(data[5])
            );
        }
    }
}