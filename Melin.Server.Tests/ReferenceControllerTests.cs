using System.ComponentModel.DataAnnotations;
using Melin.Server.Models;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Melin.Server.Tests;

public class ReferenceControllerTests {
    [Fact]
    public void GET_retrieves_book()
    {
        // Given
        await using var application = new WebApplicationFactory<Api.Startup>();
        using var client = application.CreateClient();
        // When

        var response = await client.GetAsync("/reference/books");
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    
        // Then
    }
}