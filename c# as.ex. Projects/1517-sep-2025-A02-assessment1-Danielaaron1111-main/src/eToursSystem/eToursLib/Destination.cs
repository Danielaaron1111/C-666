using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace eToursLib
{
    public class Destination
    {
        //TODO: Activity 1 place your code here


        // data members
        private string _Location;
        private DateTime _VisitDate;


        //A file has been created called Destination.cs in the project. Code a class definition creating a class called Destination that has two fully-implemented properties (with public get and set):

//        property: a string called Location(could be a City, country, busines, etc.)
//property: a DateTime called VisitDate(date of visit)
//a greedy constructor
//a ToString() method to return the Destination's data, as a comma delimited string (no additional spaces). Order data to match constructor input. Date should be in the format of MMM dd yyyy.
//Data will be validated within their respective properties.Location must have a value. VisitDate must be a date that is today or in the future.

        //properties
        public string Location
        {
            get { return _Location; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(nameof(Location), "Location must have a value.");
                }
                _Location = value.Trim();

            }


        }

        
        // property: a DateTime called VisitDate(date of visit)


        public DateTime VisitDate
        {
            get { return _VisitDate; }
            set
            {
                if (value < DateTime.Today)
                {
                    throw new ArgumentException($"VisitDate {value} must be today or in the future.", nameof(VisitDate));
                }
                _VisitDate = value;
            }
        }

        //methods

        // a greedy constructor 

        // Replace the constructor and ToString() method with the following:

        public Destination(string location, DateTime visitDate)
        {
            Location = location;
            VisitDate = visitDate;
        }

        public override string ToString()
        {
            return $"{Location},{VisitDate:MMM dd yyyy}";
        }
        


        //a ToString() method to return the Destination's data, as a comma delimited string (no additional spaces). Order data to match constructor input. Date should be in the format of MMM dd yyyy.

    }
}
