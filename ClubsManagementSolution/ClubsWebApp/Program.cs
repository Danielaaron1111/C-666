using Microsoft.EntityFrameworkCore;
using ClubsWebApp.Components;
using ClubsSystem;

var builder = WebApplication.CreateBuilder(args);

// ================================================================================
// STEP 1: Retrieve the connection string from appsettings.json
// ================================================================================
// The connection string key must match the key in appsettings.json ("ClubsDB")
string connectionstring = builder.Configuration.GetConnectionString("ClubsDB");

// ================================================================================
// STEP 2: Register services using the extension method from ClubsSystem
// ================================================================================
// This call registers:
// - The ClubsContext DbContext
// - All BLL service classes (ClubServices, EmployeeServices)
builder.Services.ClubsSystemExtensionServices(options =>
    options.UseSqlServer(connectionstring));

// ================================================================================
// STEP 3: Add Razor Components services
// ================================================================================
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // Note: HTTPS redirection is turned off as per typical assignment requirements
    // app.UseHsts();
}

// app.UseHttpsRedirection(); // Commented out as per assignment requirements

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
