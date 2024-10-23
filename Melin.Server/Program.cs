using System.Text;
using Melin.Server;
using Melin.Server.Data;
using Melin.Server.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ReferenceContext>(options =>
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

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy => {
            policy.WithOrigins("https://localhost:5173", "http://localhost:5173", "https://localhost:5000", "http://localhost:5000", "https://slider.valpo.edu", "http://localhost");
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

builder.Services.Configure<IdentityOptions>(options =>
{
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
    options.LoginPath = "/login";
    options.LogoutPath = "/api/auth/logout";
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.SameSite = SameSiteMode.None;
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


app.MapIdentityApi<IdentityUser>();

app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.MapControllers();

app.MapFallbackToFile("/index.html");


app.Run();
