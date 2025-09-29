namespace TestingMaterial
{
    internal class Program
    {
        static void Main(string[] args)
        {




            //specify data store 
            int[] scores = [91, 92, 93, 93, 95];

            //define the query expression.
            IEnumerable<int> scoreQuery =
                from score in scores
                where score > 90
                select score;

            //execute the query.
            foreach (var i in scoreQuery)
            {
                Console.WriteLine(i); // if you set with " " in the console is going to appear in a colum otherwise is 
                //gonna appear in a column 
            }


        }
    }
}
