using System.Text;
using Melin.Server;
using Melin.Server.Data;
using Melin.Server.Interfaces;
using Melin.Server.JSONInputFormatter;
using Melin.Server.Models;
using Melin.Server.Models.Binders;
using Melin.Server.Models.Context;
using Melin.Server.Models.Repository;
using Melin.Server.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;
using Swashbuckle.AspNetCore.Filters;

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
builder.Host.ConfigureLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
    logging.AddDebug();
});

// Serilog
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File(new JsonFormatter(), "Logs/warningLog.json", restrictedToMinimumLevel: LogEventLevel.Warning)
    .WriteTo.File("Logs/all-.logs",
        rollingInterval: RollingInterval.Day)
    .MinimumLevel.Debug()
    .CreateLogger();

builder.Services.AddScoped<IReferenceService, ReferenceService>();

// Add services to the container.
builder.Services.AddDbContext<ReferenceContext>(options =>
{
    var config = builder.Configuration;
    var connectionString = config.GetConnectionString("MelinDatabase");
    options.UseNpgsql(connectionString);
});

builder.Services.AddScoped<TagService>();

builder.Services.AddDbContext<TagContext>(options =>
{
    var config = builder.Configuration;
    var connectionString = config.GetConnectionString("MelinDatabase");
    options.UseNpgsql(connectionString);
});

builder.Services.AddDbContext<DataContext>(options =>
{
    var config = builder.Configuration;
    var connectionString = config.GetConnectionString("MelinDatabase");
    options.UseNpgsql(connectionString);
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.Converters.Add(new ReferenceConverter());
        // Is this the best way to handle loops?
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    });


builder.Services.AddControllers(options =>
{
    options.InputFormatters.Insert(0, MelinJPIF.GetJsonPatchInputFormatter());
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

builder.Services.AddHttpClient<ApiService>();

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
    
    // ENSURE THAT YOU CHANGE THIS PASSWORD ON NEW PROJECT CREATION
    const string password = "Admin11!";
    
    // Checking if Admin account already exists to not create duplicate/overwrite account
    if (await userManager.FindByEmailAsync(email) == null)
    {
        var user = new IdentityUser
        {
            UserName = email,
            Email = email,
        };

        await userManager.CreateAsync(user, password);

        // Only add user to Admin role if the user does not exist
        await userManager.AddToRoleAsync(user, "Admin");
    }
}

app.Run();
