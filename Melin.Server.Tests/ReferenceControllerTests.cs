using System.Net;
using System.Net.Http.Headers;
using Melin.Server.Models.References;
using Melin.Server.Tests.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace Melin.Server.Tests;

/// <summary>
/// Heavily inspired by Microsoft's Official ASP.NET Core 8.0 Documentation on
/// Integration Testing, which can be found here: https://learn.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-8.0#basic-tests-with-the-default-webapplicationfactory
/// </summary>
public class ReferenceControllerTests : IClassFixture<WebApplicationFactory<Program>> {
    private readonly WebApplicationFactory<Program> _factory;

    public ReferenceControllerTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
        
        // setup mock References for Melin Test User
        var reference1 = new Book
        {
            Id = 0,
            Title = "TEST_TITLE",
            DatePublished = DateTime.UtcNow,
            OwnerEmail = "MelinTestUser@test.com"
        };
    }

    [Fact]
    public async void GetAllReferences_Returns_OkResult()
    {
        HttpClient client = _factory.CreateClient();
        var res = await client.GetAsync("/Reference/references");

        res.EnsureSuccessStatusCode();
    }

    [Fact]
    public async void Get_SecurePageRedirectsUnauthenticatedUserToLoginPage()
    {
        var client = _factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });

        var response = await client.GetAsync("/library");
        
        Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
        Assert.StartsWith("http://localhost/login", response.Headers.Location.OriginalString);
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
        var response = await client.GetAsync("/Reference/get-single-reference");
        
        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}