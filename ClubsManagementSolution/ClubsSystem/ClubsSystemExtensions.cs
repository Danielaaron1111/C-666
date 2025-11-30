using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

#region Additional Namespaces
using ClubsSystem.BLL;
using ClubsSystem.DAL;
#endregion

namespace ClubsSystem
{
    /// <summary>
    /// ClubsSystemExtensions - Extension methods for IServiceCollection
    /// This class encapsulates the registration of services and DbContext
    /// Pattern follows WestWind solution architecture
    ///
    /// This method will be called from the web application's Program.cs file
    /// to register all necessary services for dependency injection
    /// </summary>
    public static class ClubsSystemExtensions
    {
        /// <summary>
        /// ClubsSystemExtensionServices - Registers DbContext and BLL services
        /// </summary>
        /// <param name="services">The IServiceCollection to add services to</param>
        /// <param name="options">Action to configure DbContextOptions</param>
        public static void ClubsSystemExtensionServices(
            this IServiceCollection services,
            Action<DbContextOptionsBuilder> options)
        {
            // Step 1: Register the DbContext with the connection string
            // The context is registered as internal, making it accessible only
            // through the service classes
            services.AddDbContext<ClubsContext>(options);

            // Step 2: Register all BLL service classes using AddTransient
            // AddTransient creates a new instance each time the service is requested
            // This is appropriate for stateless service classes

            // Register ClubServices
            services.AddTransient<ClubServices>((serviceProvider) =>
            {
                // Obtain the context from the service provider
                var context = serviceProvider.GetService<ClubsContext>();

                // Create and return a new instance of the service
                return new ClubServices(context);
            });

            // Register EmployeeServices
            services.AddTransient<EmployeeServices>((serviceProvider) =>
            {
                var context = serviceProvider.GetService<ClubsContext>();
                return new EmployeeServices(context);
            });

            // Additional services can be registered here following the same pattern
        }
    }
}
