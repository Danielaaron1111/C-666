using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eToursLib
{
    public class TourGuide
    {
        //fields 
        private string _FullName;
        private string _Language;

        //Properties 

        public string FullName
        { 
            get => _FullName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(nameof(FullName), "FullName must have a value");
                }
                _FullName = value.Trim();
            }
        
        }

        public string Language
        {
            get => _Language;
            set 
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(nameof(Language), "Language must have a value");
                }
                _Language = value.Trim();

            }
        }

        //contructor 

        public TourGuide(string fullname, string language) 
        {
            FullName = fullname;
            Language = language;

        }

        public override string ToString()
        {
            return $"{FullName},{Language}";
        }
    }
}
