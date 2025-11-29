using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RenoSystem.BLL;

using RenoSystem.ENTITIES;
#endregion

namespace RenoSystem
{
    public static class RenoSystemExtensions
    {
        //this class can hold an set of extension methods
        //this sample creates an extension method that will add services
        //  to IServiceCollection
        //this method will be called from an separate project to
        //  gain data from the RENOS database
        //In this demo, the call will be done in the RenoSystem Program.cs file
        //this method will do 2 things:
        //  a) register the context connection string
        //  b) register ALL services that I create within the BLL class(es)
        public static void RenosSystemExtensionServices(this IServiceCollection services,
                Action<DbContextOptionsBuilder> options)
        {
            //handle the connection string
            //add my context class to the services (IServiceCollection)
            //we wish it to use the string I send in on each creation of the
            //      instance of my DbContext class
            //this is always step 1 
            services.AddDbContext<RenosContext>(options);

            //to register a service class you will use the IServiceCollection method
            //  .AddTransient<T> where T is the service class name
            //for any outside user coding that requires access to one or more services
            //  you MUST register the service class, hence all public methods within the class
            //  will be available
            
            //step 2 register each service class
            services.AddTransient<JobServices>((serviceProvider) =>
            {
                //get the dabase connection 
                var context = serviceProvider.GetService<RenosContext>();
                //create and return a new jobServices class with the connection
                return new JobServices(context);
            });

            services.AddTransient<SupplyServices>((serviceProvider) =>
            {
                var context = serviceProvider.GetService<RenosContext>();

                return new SupplyServices(context);
            });

            //services.AddTransient<RegionServices>((serviceProvider) =>
            //{
            //    var context = serviceProvider.GetService<RenosContext>();
            //    return new RegionServices(context);
            //});

        }
    }
}
