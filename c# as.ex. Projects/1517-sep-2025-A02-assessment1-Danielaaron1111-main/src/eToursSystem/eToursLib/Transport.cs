using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace eToursLib
{
    public class Transport
    {
        //TODO: Activity 2 place your code here

        // data members
        private int _Capacity;
        private string _Name;


        //        a string field called Name(could be a Company name, ship name, busines name, etc.)
        //an integer field called Capacity
        //a greedy constructor
        //a ToString() method to return the Transport's data, as a comma delimited string (no additional spaces). Order data to match constructor input.



        //properties
        public int Capacity
        {
            get
            {
                return _Capacity;
            }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException($"Capacity value {value} must be greater than 0.", nameof(Capacity)); // i mess up here should i had write the name og the variable no just value                                           //where i forget to write the name of the value which is Capacity
                }
                _Capacity = value;
            }
        }

        public string Name
        {
            get => _Name;

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(nameof(Name), "Name cannot be null, empty, or whitespace.");
                }
                _Name = value.Trim();

            }
        }
        //methods


        public Transport(string name, int capacity)
        {   
             Name = name;
             Capacity = capacity;
          
          


        }

        public override string ToString() // ovearloaded tostring to separate string value
        {
            return $"{Name},{Capacity}"; // the to string must match the constructor 
        }

       



    }
}
