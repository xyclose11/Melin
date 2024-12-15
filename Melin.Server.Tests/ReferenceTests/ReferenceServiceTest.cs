using Melin.Server.Models;
using Melin.Server.Models.References;
using Melin.Server.Models.Repository;
using Melin.Server.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Moq;

namespace Melin.Server.Tests.ReferenceTests;

public class ReferenceServiceTest
{
    private readonly ReferenceService _referenceService;

    public ReferenceServiceTest()
    {
        IMemoryCache cache = new MemoryCache(new MemoryCacheOptions());
        
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
        
        
        var r = new Mock<IReferenceRepository>();
        var referenceRepositoryMock = r.Object;
        referenceRepositoryMock.Add(reference1);
        _referenceService = new ReferenceService(referenceRepositoryMock, cache);
    }
    
    
    [Fact]
    public async Task GetReferenceByIdAsync_Should_ReturnError_WhenGivenInvalidEmail()
    {
        // Arrange
        var response = await _referenceService.ReferenceExistsAsync(0);
        // Act
        
        // Assert
        Assert.True(response);
    }
}