using System.Linq.Expressions;
using Melin.Server.Filter;
using Melin.Server.Models;
using Melin.Server.Models.Repository;
using Melin.Server.Wrappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using Task = System.Threading.Tasks.Task;

namespace Melin.Server.Tests.ReferenceTests;

public class ReferenceRepositoryTests
{
    private readonly ReferenceRepository _repository;
    private readonly ReferenceContext _context;
    private readonly IMemoryCache _cache;
    public ReferenceRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<ReferenceContext>()
            .UseInMemoryDatabase(databaseName: "MelinTestDatabase")
            .Options;
        
        _context = new ReferenceContext(options);
        
        _context.Reference.Add(new Reference { Id = 1, OwnerEmail = "test@example.com", Title = "Reference 1" });
        _context.Reference.Add(new Reference { Id = 2, OwnerEmail = "test@example.com", Title = "Reference 2" });
        _context.Reference.Add(new Reference { Id = 3, OwnerEmail = "otheruser@example.com", Title = "Reference 3" });
        _context.Reference.Add(new Reference { Id = 4, OwnerEmail = "test@example.com", Title = "DELETE ME!" });
        _context.Reference.Add(new Reference { Id = 5, OwnerEmail = "test@example.com", Title = "I Have Creators", Creators = new List<Creator>
        {
            new Creator
            {
                FirstName = "John",
                LastName = "Snow",
                ReferenceId = 5
            },
            new Creator
            {
                FirstName = "Ned",
                LastName = "Stark",
                ReferenceId = 5
            },
            new Creator
            {
                FirstName = "Jamie",
                LastName = "Lannister",
                ReferenceId = 5
            }
        }});
        _context.Reference.Add(new Reference { Id = 6, OwnerEmail = "test@example.com", Title = "I have many tags, and groups", Tags = new List<Tag>
        {
            new Tag
            {
                Id = 1,
                Text = "ComputerScience",
                Description = "This tag describes computer science"
            },
            new Tag
            {
                Id = 2,
                Text = "THIS SHOULD ERROR DUE TO SPACES",
                Description = "This tag describes computer science"
            },
            new Tag
            {
                Id = 3,
                Text = "THIS_SHOULD_NOT_ERROR",
                Description = "This tag describes computer science"
            },
            new Tag
            {
                Id = 4,
                Text = "Cool",
                Description = "This tag describes computer science"
            },
            new Tag
            {
                Id = 5,
                Text = "Nature",
                Description = "This tag describes computer science"
            },
            new Tag
            {
                Id = 6,
                Text = "Computers",
                Description = "This tag describes computer science"
            },
        },
            Groups = new List<Group>
            {
                new Group
                {
                    Id = 1,
                    Name = "Research Paper"
                },
                new Group
                {
                    Id = 2,
                    Name = "Other"
                }
            }
        });


        _context.SaveChanges();

        _cache = new MemoryCache(new MemoryCacheOptions());

        _repository = new ReferenceRepository(_context, _cache);
    }

    [Fact]
    public async Task Get_Owned_References_Returns_Paginated_References()
    {
        // Arrange
        var filter = new PaginationFilter(1, 2);
        var userEmail = "test@example.com";

        // Act
        var result = await _repository.GetOwnedPaginatedReferencesAsync(filter, userEmail);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(3, result.Data.Count);
        Assert.True(result.Success);
        Assert.IsType<Result<List<Reference>>>(result);
    }

    [Fact]
    public async Task Delete_Single_Reference_Returns_True()
    {
        var userEmail = "test@example.com";
        var referenceId = 4;

        var result = await _repository.DeleteAsync(userEmail, referenceId);
        var deletedReference = await _context.Reference.FindAsync(referenceId);

        Assert.True(result);
        Assert.Null(deletedReference);
    }

    [Fact]
    public async Task Attempt_Get_Reference_Unauthorized_Returns_Fail()
    {
        var userEmail = "test@example.com";
        var unauthorizedReferenceId = 3;
        
        var result = await _repository.GetReferenceByIdAsync(userEmail, unauthorizedReferenceId);
        
        Assert.False(result.Success);
        Assert.Equal("Reference not found.", result.ErrorMessage);
    }

    [Fact]
    public async Task Get_Reference_All_Details_Includes_Tags_Groups()
    {
        var userEmail = "test@example.com";
        var referenceId = 6;
    }

    [Fact]
    public async Task Delete_Tag_From_Reference()
    {
        
    }
    
    [Fact]
    public async Task Get_Reference__Includes_Creators()
    {
        var userEmail = "test@example.com";
        var referenceId = 5;

        var creatorToBeContained = new Creator
        {
            Id = 1,
            FirstName = "Ned",
            LastName = "Stark",
            ReferenceId = 5            
        };

        var res = await _repository.GetReferenceByIdAsync(userEmail, referenceId);
        
        Assert.True(res.Success);
        Assert.NotNull(res.Data);
        Assert.Distinct(res.Data.Creators);
        Assert.Contains(res.Data.Creators, c => c.FirstName == creatorToBeContained.FirstName);
    }

    

}
