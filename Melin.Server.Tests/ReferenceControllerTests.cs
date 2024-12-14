using System.Net;
using System.Net.Http.Headers;
using Melin.Server.Models;
using Melin.Server.Models.References;
using Melin.Server.Models.Repository;
using Melin.Server.Services;
using Melin.Server.Tests.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Melin.Server.Tests;

// TODO CREATE A TEST DATABASE TO TEST API ENDPOINTS WITH ACTUAL USER DATA
/// <summary>
/// Heavily inspired by Microsoft's Official ASP.NET Core 8.0 Documentation on
/// Integration Testing, which can be found here: https://learn.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-8.0#basic-tests-with-the-default-webapplicationfactory
/// </summary>
public class ReferenceControllerTests : IClassFixture<WebApplicationFactory<Program>> {
    private readonly WebApplicationFactory<Program> _factory;
    private readonly ReferenceService _referenceService;
    public ReferenceControllerTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
        
        // setup mock References for Melin Test User
        var reference1 = new Book
        {
            Id = 0,
            Title = "TEST_TITLE",
            DatePublished = DateTime.UtcNow,
            OwnerEmail = "MelinTestUser@test.com",
            Creators = new List<Creator>
            {
                new()
                {
                    Id = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    ReferenceId = 0,
                    Types = CreatorTypes.Author
                },
                new()
                {
                    Id = 2,
                    FirstName = "Mary",
                    LastName = "Lee",
                    ReferenceId = 0,
                    Types = CreatorTypes.Editor
                }
            },
            Language = Language.English,
            PageAmount = 312,
            Place = "New York, USA",
            Tags = new List<Tag>
            {
                new()
                {
                    Id = 3,
                    Text = "NYC",
                    Description = "Novels Written in New York City",
                    CreatedBy = "MelinTestUser@test.com"
                },
                new()
                {
                    Id = 4,
                    Text = "Vintage",
                    Description = "Novels 75+ years old",
                    CreatedBy = "MelinTestUser@test.com"
                },
                new()
                {
                    Id = 5,
                    Text = "Action",
                    Description = "",
                    CreatedBy = "MelinTestUser@test.com"
                },
                new()
                {
                    Id = 6,
                    Text = "Historic",
                    Description = "Historic Novels",
                    CreatedBy = "MelinTestUser@test.com"
                }
            }
        };
        
        // in-memory database
        var options = new DbContextOptionsBuilder<ReferenceContext>()
            .UseInMemoryDatabase(databaseName: "MelinTestDatabase")
            .EnableSensitiveDataLogging()
            .Options;

        var context = new ReferenceContext(options);

        context.Reference.Add(reference1);
        
        context.SaveChanges();

        var cache = new MemoryCache(new MemoryCacheOptions());

        var repository = new ReferenceRepository(context, cache);

        _referenceService = new ReferenceService(repository, cache);
    }

    [Fact]
    public async void GetAllReferences_Returns_OkResult()
    {
        HttpClient client = _factory.CreateClient();
        var res = await client.GetAsync("/Reference/references");

        res.EnsureSuccessStatusCode();
    }
    
    [Fact]
    public async void Get_SecurePage_ReturnsStatusUnauthorized()
    {
        var client = _factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });

        var response = await client.GetAsync("/get-owned-tags");
        
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async void Get_SecurePageIsReturnedForAuthenticationUser()
    {
        var client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddAuthentication(defaultScheme: "TestScheme")
                        .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>(
                            "TestScheme", options => { });
                });
            })
            .CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });

        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue(scheme: "TestScheme");
        
        // Act
        var response = await client.GetAsync("/Reference/get-single-reference?refId=0");
        
        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}