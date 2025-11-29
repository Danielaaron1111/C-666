using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using RenoSystem.BLL;



#region Additional Namespaces

using RenoSystem.ENTITIES;// for job entity
#endregion

namespace RenoSystem.BLL
{
    public class JobServices // must be public to blazor use it this!!!!!!!
    {
        //dabase conextion variable (provided by dependemcy injection no ready yet)
        private readonly RenosContext _context; //THIS STORE MY DATABASE CONNECTION FOR THAT REASON IS PRIVATE

        //this is an internal constrcutor only other classes can acces it 
        // the web app cannot access it directly need dependency injection
        internal JobServices(RenosContext context) // IS INTERNATL because contains the context 
        {
            _context = context; // also this constructor will create 
        }

        // method to return all jobs 
        
        public List<Job> Job_GetList()
        {
            // i do have to get all jobs from the dtabase 
            // include is like a join inner join in sql it gets the related client data 
            // orderby sort alphabetically by description 
            //ToList executes the query and return a list collection 
            IEnumerable<Job> info = _context.Jobs
                .Include(j => j.Client) // join client data 
                .OrderBy(j => j.Description); // sort by description 
                //.ToList(); // execute the query and return a list 
                //question here do i use the return or is that unstructured code as don say? 
                //i will check westwind 
                return info.ToList(); // execute the query and return a list
        }
        
        
        
        
        //public IEnumerable<Job> Job_GetList() //return a list of job objects 
        //{
        //    return _context.Jobs
        //        .Include(j => j.Client) //give me the jobs from the DB
        //        .OrderBy(j => j.Description) // load client data 
        //        .ToList(); // conver all in a list 
        //}







    }
}
//D) BLL Services

//You will need to create each of the services below.

//- Each service class will need an appropriate internal constructor that requires an instance of the `RenosContext` as a parameter.
//- Save the parameter value into a private readonly variable.As you code, you will need to resolve references to needed namespaces holding your context class and entity class.
//-Once you have created the class, register the class in your extension method via AddTransient.
//- **If a method has a string parameter, ensure one was passed and, if not, throw an `ArgumentNullException`.**

//**JobServices class**

//Create a new service class called `JobServices` in your BLL folder. This class will contain one service method to return the complete list of Jobs. This query list will be sorted alphabetically by Description.  Remember to register your service class.

//In this class, create a public method called `Job_GetList()` that:

//-Has no parameters
//- You will need to include the associated Client instance for each job.
//- Returns an ordered collection (by Description) of all records of the Job entity.

//**SupplyServices**

//Create a new service class called `SupplyServices` in your BLL folder. This class will contain two service methods:  to return the page list of supplies for a specified job ordered by Material and another to return the total number of materails for a specified job. Each method will receive a filtering parameter for a specified job. One method will also receive the data necessary to implement paging and return only the records needed for the current page that will be displayed. Remember to register your service class.

//Create a public method called `GetByJobId(int, int, int)` that:

//-Has one parameter for the data and 2 parameters to handle paging
//- Returns a collection of Supply records, ordered alphabetically by Material, where the JobId equals the parameter value for the current page to be displayed.
//- The data parameter value will be an integer job id value.

//Create a public method called `GetJobSupplyCount(int)` that:

//-Has one parameter for the data
//- Returns the total count of records where the JobId equals the parameter.

//After creating these classes, rebuild your Application Class Library. You should get a successful build. parameter, ensure one was passed and, if not, throw an `ArgumentNullException`.**