using BikeSparePartsShop.AuthorizationHandlers;
using BikeSparePartsShop.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


const string CORSAllowSpecificOrigins = "_CORSAllowed";

//Create the app builder
var builder = WebApplication.CreateBuilder(args);

//Add services to the builder.
builder.Services.AddCors(options => options.AddPolicy(name: CORSAllowSpecificOrigins,
                                                      policy => policy.WithOrigins("http://localhost:7004", "http://localhost:3000")));

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
                                                    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddRazorPages();
//the default identity of the user
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

// All 3 handlers need to the registered with the service container in program.cs: 
builder.Services.AddSingleton<IAuthorizationHandler, IsInRoleHandler>();
builder.Services.AddSingleton<IAuthorizationHandler, HasClaimHandler>();
builder.Services.AddSingleton<IAuthorizationHandler, ViewRolesHandler>();

//Authorization within a Razor Pages application is provided by a number of services, including an IAuthorizationService. 
//These must be added to the service container at application startup.
//A convenience method, AddAuthorization takes care of adding all the required services: builder.Services.AddAuthorization();

builder.Services.AddAuthorization(options =>
{
    //View Roles Policy
    options.AddPolicy("ViewRolesPolicy", policyBuilder => policyBuilder.RequireAssertion( context =>
    {
        var canViewRoles = context.User.IsInRole("Staff") && context.User.HasClaim("Permission", "View Roles");
        //if they have a claim of type "Joining Date" and the value is less than 6 months ago, and
        //they have Permission and View Roles they can view roles
        // We use the FindFirst method to access a claim and obtain its value(if there is one) and convert it to a DateTime
        var joiningDateClaim = context.User.FindFirst(c => c.Type == "Joining Date")?.Value;
        if (joiningDateClaim != null)
            return canViewRoles && DateTime.Parse(joiningDateClaim) > DateTime.MinValue &&
                                                            DateTime.Parse(joiningDateClaim) < DateTime.Now.AddMonths(-6);
        else 
            return false;
    }));


    //Can View Claims Policy
    options.AddPolicy("ViewClaimsPolicy", policyBuilder => policyBuilder.RequireAssertion(context =>
    {
        var canViewClaims = context.User.IsInRole("Staff") && context.User.HasClaim("Permission", "View Claims");
        var joiningDateClaim = context.User.FindFirst(c => c.Type == "Joining Date")?.Value;
        if (joiningDateClaim != null)
            return canViewClaims && DateTime.Parse(joiningDateClaim) > DateTime.MinValue &&
                                                            DateTime.Parse(joiningDateClaim) < DateTime.Now.AddMonths(-6);
        else 
            return false;


    }));

    //Delete Stock Policy
    options.AddPolicy("DeleteStock", policyBuilder => policyBuilder.RequireAssertion(context =>
    {
        return context.User.IsInRole("Staff") && context.User.HasClaim("Permission", "Delete Stock");
    }));

    //Edit Stock Policy
    options.AddPolicy("EditStock", policyBuilder => policyBuilder.RequireAssertion(context =>
    {
        return context.User.IsInRole("Staff") && context.User.HasClaim("Permission", "Edit Stock");
    }));

    //Over18 Add Stock Policy
    options.AddPolicy("Over18", policyBuilder => policyBuilder.RequireAssertion(context =>
    {
        var birthDate = context.User.FindFirst(c => c.Type == "Birth Date")?.Value;
        if (birthDate != null)
            return context.User.IsInRole("Staff") &&  (DateTime.Parse(birthDate).Year - DateTime.Now.Year) > 18;
        else
            return false;
    }));

    //Staff Admin Policy
    options.AddPolicy("StaffAdmin", policyBuilder => policyBuilder.RequireRole("Admin"));
});

//Having configured the policy we can apply it to the AuthorizeFolder method to ensure that
//only members of that policy can access the content: 
builder.Services.AddRazorPages(options => options.Conventions.AuthorizeAreaFolder("Identity","/RolesManager", "ViewRolesPolicy"));
builder.Services.AddRazorPages(options => options.Conventions.AuthorizeAreaFolder("Identity", "/ClaimsManager", "ViewClaimsPolicy"));

//Add Password properties to make it easier for debugging
builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings.
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
    options.SignIn.RequireConfirmedEmail = false;
    // Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;
    // User settings.
    options.User.AllowedUserNameCharacters =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = false;
});

builder.Services.ConfigureApplicationCookie(options =>
{
    // Cookie settings
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

    options.LoginPath = "/Identity/Account/Login";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
    options.SlidingExpiration = true;
});

//Add swagger services
builder.Services.AddSwaggerGen();

//Build the app
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days.
    // You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors(CORSAllowSpecificOrigins);
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();   //Authorization middleware is enabled by default in the web application template by the inclusion of app.UseAuthorization() in the Program class

app.MapControllerRoute(name: "default",
                       pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();


//Run the app
app.Run();

public partial class Program { }
