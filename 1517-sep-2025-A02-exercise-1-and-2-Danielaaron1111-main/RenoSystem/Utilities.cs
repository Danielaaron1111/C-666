using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RenoSystem
{
    public static class Utilities
    {
        // help me >:
        // no instances just helpers support heal mee
        //non -zero , one for >= criteria.
        //can reuse this whatever i want 



        public static bool IsNonZeroPositive(int value)
        {
            return value > 0;
        }

        public static bool MeetsMinimumCriteria(int value, int criteria)
        {
            return value >= criteria;
        }

    }
}

//string myString = "Hello";

//if (!string.IsNullOrEmpty(myString))
//{
//    // This code block will execute if myString is neither null nor an empty string.
//    Console.WriteLine("The string is not null or empty.");
//}
//else
//{
//    // This code block will execute if myString is null or an empty string.
//    Console.WriteLine("The string is null or empty.");
//}

//string anotherString = "   "; // A string with only whitespace

//if (!string.IsNullOrWhiteSpace(anotherString))
//{
//    // This code block will execute if anotherString is not null, not empty, and not just whitespace.
//    Console.WriteLine("The string has content beyond whitespace.");
//}
//else
//{
//    // This code block will execute if anotherString is null, empty, or only whitespace.
//    Console.WriteLine("The string is null, empty, or only whitespace.");
//}