using System.ComponentModel.DataAnnotations;
using Melin.Server.Controllers;
using Melin.Server.Models;
using Melin.Server.Models.Repository;
using Microsoft.AspNetCore.Mvc.Testing;
using Moq;
using Task = Melin.Server.Models.Task;

namespace Melin.Server.Tests;

public class ReferenceControllerTests {
    [Fact]
    public async Task GET_retrieves_book_by_ID()
    {
        var mockRepo = new Mock<IReferenceRepository>();
        mockRepo.Setup(repo => repo.GetById(4))
            .Returns(GetReferences());
        var controller = new ReferenceController(mockRepo.Object);

        var res = await controller.GetReferences();

    }
    
    private List<Reference> GetReferences()
    {
        var sessions = new List<Reference>();
        sessions.Add(new Reference()
        {
            Id = 1,
            Title = "Test One",
            ShortTitle = "T1"
        });
        sessions.Add(new Reference()
        {
            Id = 2,
            Title = "Test Two",
            ShortTitle = "T2"
        });
        return sessions;
    }
}