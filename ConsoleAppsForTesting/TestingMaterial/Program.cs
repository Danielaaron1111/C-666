namespace TestingMaterial
{
    internal class Program
    {
        static void Main(string[] args)
        {




        ////specify data store 
        //int[] scores = [91, 92, 93, 93, 95];

        ////define the query expression.
        //IEnumerable<int> scoreQuery =
        //    from score in scores
        //    where score > 90
        //    select score;

        ////execute the query.
        //foreach (var i in scoreQuery)
        //{
        //    Console.WriteLine(i); // if you set with " " in the console is going to appear in a colum otherwise is 
        //    //gonna appear in a column 
        //}
            //https://learn.microsoft.com/en-us/dotnet/csharp/linq/

            List<int> numbers = new List<int> { 1, 2, 3, 4, 27, 35, 23, 43, 43, 25 };

            var NumbersGreaterThan20 = from numb in numbers
                                       where numb > 20
                                       select numb;
            Console.WriteLine("Number greaters than 20");
            foreach (int numb in numbers) 
            {
                Console.WriteLine(numb);
            }
            // LINQ method syntax to sort the list in descending order
            var sortedNumbers = numbers.OrderByDescending(n => n);
            Console.WriteLine("Sorted numbers (descending):");
            foreach (var num in sortedNumbers)
            {
                Console.Write(num + " "); // Output: 10 9 8 7 6 5 4 3 2 1
            }
            Console.WriteLine();

            // LINQ to select numbers greater than 5
            var greaterThanFive = numbers.Where(n => n > 5);
            Console.WriteLine("Numbers greater than 5:");
            foreach (var num in greaterThanFive)
            {
                Console.Write(num + " "); // Output: 6 7 8 9 10
            }
            Console.WriteLine();


        }
    }
}
