namespace SimpleLibrarySystem
{
    public class Book
    {

        private string _title;
        private string _author;
        private int _year;
        public string Title
        {
            get { return _title; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Title cannot be empty  or whitespace.");

                }
                _title = value;
            }
        }


        public string Author
        {
            get { return _author; }

            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Author cannot be empty or  whitespace");
                }
                _author = value.Trim();

            }




        }

        public int Year
        {
            get { return _year; }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Year nyst ve a positive value");
                }
                _year = value;

            }

        }
        // control k d for format text


        //constructor 
        public Book(string title, string author, int year)
        {
            Title = title;
            Author = author;
            Year = year;
        }
      

        public override string ToString()
        {
            return $"{Title} by {Author} {Year}";
        }
    }
}
