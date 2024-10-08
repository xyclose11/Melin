using System.Text;
using Melin.Server;
using Melin.Server.Data;
using Melin.Server.Models;
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

// Add services to the container.
builder.Services.AddDbContext<Database>(options =>
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
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

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



builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy => {
            policy.WithOrigins("https://localhost:5000", "http://localhost:5000", "https://slider.valpo.edu", "http://localhost");
            policy.AllowAnyHeader();
            policy.AllowAnyMethod();
            policy.AllowCredentials();
        }
    );

    options.AddPolicy("MelinReactClient",
        corsBuilder =>
        {
            corsBuilder.WithOrigins("https://localhost:5173", "http://localhost:5173");
            corsBuilder.AllowAnyHeader();
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



builder.Services.AddIdentityApiEndpoints<IdentityUser>()
    .AddEntityFrameworkStores<DataContext>();
builder.Services.AddAuthorization();

builder.Services.Configure<IdentityOptions> (options => {

});

builder.Services.ConfigureApplicationCookie(options => {
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(15);

    options.LoginPath = "/login";
    options.AccessDeniedPath = "/accessdenied";
    options.SlidingExpiration = true;
    options.Cookie.Name = "MELIN_AUTH_COOKIE";
    options.Cookie.Domain = "slider.valpo.edu";
    options.Cookie.SameSite = SameSiteMode.Strict;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
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

app.UseAuthorization();
app.UseAuthentication();

if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
} else {
    app.UseHsts();
}

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.UseCors();
app.UseCors("MelinReactClient");
app.UseCors("MelinBackend");

app.Run();
