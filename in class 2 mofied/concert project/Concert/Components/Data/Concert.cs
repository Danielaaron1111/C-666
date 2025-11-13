namespace ConcertSystem
{
    public class Concert
    {
        private string _ArtistName;
        private double _TicketPrice;

        public string ArtistName
        {
            get { return _ArtistName; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("ArtistName", "Artist name is required. Cannot be empty.");
                }
                _ArtistName = value;
            }
        }

        public DateTime ConcertDate { get; set; }

        public TimeSpan ConcertTime { get; set; }

        public double TicketPrice
        {
            get { return _TicketPrice; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException($"Ticket price {value} is invalid. Must be greater than 0.", "TicketPrice");
                }
                _TicketPrice = value;
            }
        }

        public int NumberOfTickets { get; set; }

        public VenueType Venue { get; set; }

        public Concert(string artistName, DateTime concertDate, TimeSpan concertTime,
                      double ticketPrice, int numberOfTickets, VenueType venue)
        {
            // Validate concert date is not in the past
            if (concertDate < DateTime.Today)
            {
                throw new ArgumentException($"Concert date {concertDate.ToShortDateString()} is invalid. Cannot book concerts in the past.", "ConcertDate");
            }

            // Validate concert time (concerts must be between 10 AM and 11 PM)
            if (concertTime < new TimeSpan(10, 0, 0) || concertTime > new TimeSpan(23, 0, 0))
            {
                throw new ArgumentException($"Concert time {concertTime} is invalid. Concerts must be between 10:00 AM and 11:00 PM.", "ConcertTime");
            }

            // Validate number of tickets
            if (numberOfTickets <= 0 || numberOfTickets > 10)
            {
                throw new ArgumentException($"Number of tickets {numberOfTickets} is invalid. Must be between 1 and 10.", "NumberOfTickets");
            }

            ArtistName = artistName;
            ConcertDate = concertDate;
            ConcertTime = concertTime;
            TicketPrice = ticketPrice;
            NumberOfTickets = numberOfTickets;
            Venue = venue;
        }

        public double TotalCost()
        {
            return TicketPrice * NumberOfTickets;
        }

        public override string ToString()
        {
            // Format: ArtistName,Date,Time,Price,Tickets,Venue
            return $"{ArtistName},{ConcertDate.ToString("yyyy-MM-dd")},{ConcertTime.ToString(@"hh\:mm")},{TicketPrice},{NumberOfTickets},{Venue}";
        }

        public static Concert Parse(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                throw new ArgumentNullException("ParseString", "No data present to Parse.");
            }

            string[] items = text.Split(',');

            if (items.Length != 6)
            {
                throw new FormatException($"Data string is invalid. Expected 6 values. Data: {text}");
            }

            return new Concert(
                items[0],                           // ArtistName
                DateTime.Parse(items[1]),          // ConcertDate
                TimeSpan.Parse(items[2]),          // ConcertTime
                double.Parse(items[3]),            // TicketPrice
                int.Parse(items[4]),               // NumberOfTickets
                Enum.Parse<VenueType>(items[5])    // Venue
            );
        }
    }
}