namespace Melin.Server.Tests;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
public class ReferenceContextTests
{
    private ReferenceContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder<MyDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        return new MyDbContext(options);
    }
    
    [Fact]
    public async Task AddProduct_ShouldAddProductToDatabase()
    {
        // Arrange
        var context = GetDbContext();
        var product = new Product { Name = "Test Product" };

        // Act
        context.Products.Add(product);
        await context.SaveChangesAsync();

        // Assert
        Assert.Equal(1, await context.Products.CountAsync());
        Assert.Equal("Test Product", (await context.Products.FirstAsync()).Name);
    }
}