using System.Text;
using Melin.Server;
using Melin.Server.Data;
using Melin.Server.Interfaces;
using Melin.Server.Models;
using Melin.Server.Models.Context;
using Melin.Server.Models.Repository;
using Melin.Server.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

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

app.Run();
