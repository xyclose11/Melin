using System.Linq.Expressions;
using Melin.Server.Filter;
using Melin.Server.Models;
using Melin.Server.Models.Repository;
using Microsoft.EntityFrameworkCore;
using Moq;
using Task = System.Threading.Tasks.Task;

namespace Melin.Server.Tests.ReferenceTests;

public class ReferenceRepositoryTests
{
    private readonly ReferenceRepository _repository;
    private readonly ReferenceContext _context;

    public ReferenceRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<ReferenceContext>()
            .UseInMemoryDatabase(databaseName: "MelinTestDatabase")
            .Options;
        
        _context = new ReferenceContext(options);

        _context.Reference.Add(new Reference { Id = 1, OwnerEmail = "test@example.com", Title = "Reference 1" });
        _context.Reference.Add(new Reference { Id = 2, OwnerEmail = "test@example.com", Title = "Reference 2" });
        _context.Reference.Add(new Reference { Id = 3, OwnerEmail = "otheruser@example.com", Title = "Reference 3" });
        _context.SaveChanges();

        _repository = new ReferenceRepository(_context);
    }

    [Fact]
    public async Task GetOwnedReferences_Returns_Paginated_References()
    {
        // Arrange
        var filter = new PaginationFilter(1, 2);
        var userEmail = "test@example.com";

        // Act
        var result = await _repository.GetOwnedReferencesAsync(filter, userEmail);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
    }
    
    [Fact]
    public async void 
}
