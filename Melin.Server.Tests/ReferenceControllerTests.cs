using System.ComponentModel.DataAnnotations;
using Melin.Server.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Moq;
using Task = Melin.Server.Models.Task;

namespace Melin.Server.Tests;

public class ReferenceControllerTests {
    [Fact]
    public async Task GET_retrieves_book()
    {
        var mockRepo = new Mock<ReferenceContext>();
        mockRepo.Setup(repo => repo.Artworks)
            .Returns();
    }
}