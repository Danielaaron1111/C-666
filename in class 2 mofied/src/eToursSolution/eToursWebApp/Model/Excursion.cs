namespace eToursWebApp.Model
{
    public class Excursion
    {
        private string _Name;
        private double _Duration;
        public string Name
        {
            get { return _Name; }
            set
            {
                if(string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Name", "Name is required. Cannot be empty.");
                }
                _Name =value;
            }
        }

        public DateTime ExcursionDate { get; set; }

        public double Duration
        {
            get { return _Duration; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException($"Duration {value} is invalid. Must be greater than 0.","Duration");
                }
                _Duration = value;
            }
        }

        public ExcursionType TourType { get; set; }

        public Excursion(string name, DateTime excursiondate, double duration, ExcursionType tourtype)
        {
            if (excursiondate < DateTime.Today)
            {
                throw new ArgumentException($"Excursion date {excursiondate} is invalid. Must be a date greater than today", "ExcurationDate");
            }
            Name = name;
            ExcursionDate = excursiondate;
            Duration = duration;
            TourType = tourtype;
        }

        public override string ToString()
        {
            return $"{Name},{ExcursionDate.ToString("MMM-dd-yyyy")},{Duration},{TourType}";
        }

        public static Excursion Parse(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                throw new ArgumentNullException("ParseString", "No data present to Parse.");
            }
            string[] items = text.Split(',');
            if (items.Length != 4)
            {
                throw new FormatException($"Data string is invalid. Insufficent/Excessive string values. Data: {text}");
            }
            return new Excursion(items[0],
                                 DateTime.Parse(items[1]),
                                 double.Parse(items[2]),
                                 Enum.Parse<ExcursionType>(items[3]));
        }
    }
}
