using RenoSystem.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RenoSystem.ENTITIES;



#region Additional Namespaces
//using RenoSystem.DAL;

#endregion
namespace RenoSystem.BLL
{
    public  class SupplyServices
    {

        //again database connection variable got it by dependency injection 
        //( i must repeat this many times )
        private readonly RenosContext _context;


        //constructor to receive the context connection
        internal SupplyServices(RenosContext context)
        {
            _context = context;
        }
        public List<Supply> GetByJobId(int jobId, int currentpagenumber, int itemperpage)
        {
            // Validation (WestWind pattern)
            //demo hackers validation don demo : 
            if (jobId <= 0)
            {
                throw new ArgumentException($"Job ID {jobId} is invalid. Must be greater than 0.");
            }
            


            // ad references to from microsoft docs to implement paging
            // in class was not seen anthing about paging :(
            IEnumerable<Supply> info = _context.Supplies
                                                .Where(x => x.JobId == jobId) //only supplies for this job
                                                .OrderBy(x => x.Material); // sort alphabecitcally by material 


                int recordsSkipped = (currentpagenumber - 1) * itemperpage; //formula 



                                                //.Skip(skip) // skip previous pages
                                                //.Take(pageSize); //take only records for the current page 


            return info.Skip(recordsSkipped).Take(itemperpage).ToList();
        }

        //get the total count of suplies for a specific job
        //is used to calculate how many page are needed 
        // according to the instructions on the bottom:
        //shoul have one parameter for the data jobId
        //return the total count of records where jobId equals the parameter 
        //let say if a job has 55 supplies and page size is 10 

        // then i will need 6 pages to display all supplies for that job
        public int GetJobSupplyCount(int jobId)
        {
            return _context.Supplies
                           .Where(x => x.JobId == jobId)
                           .Count();
        }


    }
}


//**SupplyServices * *

//Create a new service class called `SupplyServices`
//in your BLL folder. This class will contain two service methods:
//to return the page list of supplies for a specified job ordered by Material
//and another to return the total number of materails for a specified job.
//Each method will receive a filtering parameter for a specified job.
//One method will also receive the data necessary to implement paging and
//return only the records needed for the current page that will be displayed.
//Remember to register your service class.

//Create a public method called `GetByJobId(int, int, int)` that:

//-Has one parameter for the data and 2 parameters to handle paging
//- Returns a collection of Supply records, ordered alphabetically by Material, where the JobId equals the parameter value for the current page to be displayed.
//- The data parameter value will be an integer job id value.

//Create a public method called `GetJobSupplyCount(int)` that:

//-Has one parameter for the data
//- Returns the total count of records where the JobId equals the parameter.

//After creating these classes, rebuild your Application Class Library. You should get a successful build.