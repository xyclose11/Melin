using System.Text.Json.Serialization;
using Melin.Server;
using Melin.Server.Data;
using Melin.Server.Filter;
using Melin.Server.JSONInputFormatter;
using Melin.Server.Middleware;
using Melin.Server.Models;
using Melin.Server.Models.Binders;
using Melin.Server.Models.Context;
using Melin.Server.Models.Repository;
using Melin.Server.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Formatting.Json;

var builder = WebApplication.CreateBuilder(args);

// HTTP Logging
builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.All;
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;
    logging.CombineLogs = true;
});

// Logging
builder.Logging.ClearProviders().AddConsole().AddDebug();

// Serilog
// Ensuring that Debug logging is off for production to avoid verbose logs that could
// degrade service
var logLevel = builder.Environment.IsDevelopment() ? LogEventLevel.Debug : LogEventLevel.Warning;

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .Enrich.WithExceptionDetails()
    .WriteTo.Console()
    .WriteTo.File(new JsonFormatter(), "Logs/warningLog.json", restrictedToMinimumLevel: LogEventLevel.Warning)
    .WriteTo.File("Logs/all-.logs",
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}}",
        rollingInterval: RollingInterval.Day)
    .MinimumLevel.Is(logLevel)
    .CreateLogger();

// builder.Services.AddScoped<IReferenceService, ReferenceService>();
builder.Services.AddScoped<IGroupService, GroupService>();

var melinConnectionString = builder.Configuration.GetConnectionString("MelinDatabase");
// Add services to the container.
builder.Services.AddDbContext<ReferenceContext>(options =>
{
    options.UseNpgsql(melinConnectionString);
});

builder.Services.AddScoped<TagService>();

builder.Services.AddDbContext<TagContext>(options =>
{
    options.UseNpgsql(melinConnectionString);
});

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseNpgsql(melinConnectionString);
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {
    options.IncludeXmlComments(Path.Combine("obj/Debug/net8.0/Melin.Server.xml"));
});

builder.Services.AddProblemDetails();

if (!builder.Environment.IsDevelopment()) {
    builder.Services.AddHttpsRedirection(options => {
        options.HttpsPort = 443;
    });

}

if (builder.Environment.IsProduction()) {
    builder.Services.AddSpaStaticFiles(configuration => {
        configuration.RootPath = Path.Combine("wwwroot");
    });
}


builder.Services.AddMemoryCache();
builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddTransient<IReferenceService, ReferenceService>();
builder.Services.AddTransient<IReferenceRepository, ReferenceRepository>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy => {
            policy.WithOrigins("https://localhost:7120","https://localhost:5173", "http://localhost:5173", "https://localhost:5000", "http://localhost:5000", "https://slider.valpo.edu", "http://localhost");
            policy.AllowAnyHeader();
            policy.AllowAnyMethod();
            policy.AllowCredentials();
            policy.SetIsOriginAllowedToAllowWildcardSubdomains();
        }
    );

    options.AddPolicy("MelinReactClient",
        corsBuilder =>
        {
            corsBuilder.WithOrigins("https://localhost:5173", "http://localhost:5173");
            corsBuilder.AllowAnyHeader();
            corsBuilder.SetIsOriginAllowedToAllowWildcardSubdomains();
            corsBuilder.AllowAnyMethod();
            corsBuilder.AllowCredentials();
        });

        options.AddPolicy("MelinBackend",
        corsBuilder =>
        {
            corsBuilder.WithOrigins("https://localhost:5000", "http://localhost:5000");
            corsBuilder.AllowAnyHeader();
            corsBuilder.AllowAnyMethod();
            corsBuilder.AllowCredentials();
        });
});

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.Converters.Add(new StringEnumConverter());
        options.SerializerSettings.Converters.Add(new ReferenceConverter());
        options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        options.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.Objects;
    });

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddControllers(options =>
{
    options.InputFormatters.Insert(0, MelinJPIF.GetJsonPatchInputFormatter());
});

builder.Services.AddControllers(options =>
{
    options.Filters.Add<AuthorizationFilter>();
});




builder.Services.Configure<IdentityOptions>(options =>
{
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = true;
    options.Password.RequireDigit = true;
});

builder.Services.AddAuthentication()
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
        builder.Configuration.Bind("CookieSettings", options));
    
builder.Services.AddAuthorization();

builder.Services.AddIdentityApiEndpoints<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<DataContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromDays(3);
    options.SlidingExpiration = true;
    options.LoginPath = "/api/Auth/login";
    options.LogoutPath = "/api/Auth/logout";
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.SameSite = SameSiteMode.None;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

app.UseHttpLogging();
app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (builder.Environment.IsProduction())
{
    app.UseSpaStaticFiles();
}

app.UseSpa(spa => {});

app.MapIdentityApi<IdentityUser>();

app.UseCors();
app.UseCookiePolicy();
app.UseAuthentication();
app.UseAuthorization();

// Map Serilog config middleware after UseAuthentication to ensure User.Identity is initialized
app.UseMiddleware<SerilogConfigurationMiddleware>();

if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
} else {
    app.UseHsts();
}

app.MapControllers();

app.MapFallbackToFile("/index.html");


// Seeding Roles
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    var roles = new[] { "Admin", "Moderator", "User", "Guest" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}

// Create default Admin account *** IF CREATING NEW PROJECT ENSURE CREDENTIALS ARE CHANGED ***
using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

    const string email = "melinAdmin@admin.com";

    var tempAdminPassword = Environment.GetEnvironmentVariable("SINGLE_USE_ADMIN_PASSWORD");


    
    // Checking if Admin account already exists to not create duplicate/overwrite account
    if (await userManager.FindByEmailAsync(email) == null)
    {
        var user = new IdentityUser
        {
            UserName = email,
            Email = email,
        };

        if (tempAdminPassword == null)
        {
            Console.WriteLine("PLEASE SET TEMPORARY ADMIN PASSWORD IN appsettings.development.json");
            throw new NullReferenceException();
        }
        await userManager.CreateAsync(user, tempAdminPassword);

        // Only add user to Admin role if the user does not exist
        await userManager.AddToRoleAsync(user, "Admin");
    }
}

app.Run();
