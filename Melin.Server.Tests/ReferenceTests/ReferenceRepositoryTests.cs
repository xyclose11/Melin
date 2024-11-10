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
        Assert.IsType<Result<List<Reference>>>(result);
    }

    [Fact]
    public async Task Delete_Single_Reference_Returns_True()
    {
        var userEmail = "test@example.com";

        var result = await _repository.DeleteAsync(userEmail, 4);

        Assert.True(result);
    }

    [Fact]
    public async Task Attempt_Get_Reference_Unauthorized_Returns_Fail()
    {
        var userEmail = "test@example.com";


        var result = await _repository.GetReferenceByIdAsync(userEmail, 3);
        
        Assert.True(!result.Success);
    }
    

}
