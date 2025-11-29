using RenoTracker.Components;
using Microsoft.EntityFrameworkCore;
using RenoSystem.ENTITIES;
using RenoSystem;



var builder = WebApplication.CreateBuilder(args);

//retrieve the connection string from the appsetting.json
//the connection string will be passed to the class library using
//  the encapsulated extension method
//the connection string will be registered to get access to the database
string connectionstring = builder.Configuration.GetConnectionString("RenosDB");
//THIS ALWAYS HAVE TO MATCH THE JSON STUFF !

//get access to any extended services
//gain access to any available services that have been registered in IServiceCollections
//the technique used in this example has the registrations encapsulated
//  within the class library via an extension method
//technically, you could put all the setup that is in the extension method
//  here in this file
builder.Services.RenosSystemExtensionServices(options => options.UseSqlServer(connectionstring));

//builder.Services.AddDbContext<DbVersion>(options => options.UseSqlServer(connectionstring));

// Add services to the container
builder.Services.AddRazorComponents()
.AddInteractiveServerComponents(); // @REMEMBERR THIS ENABLE @COOODE IS SQUIGGLE NOW but it ill be fine 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();


app.Run();
