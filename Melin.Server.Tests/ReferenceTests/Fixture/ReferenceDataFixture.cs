using Melin.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace Melin.Server.Tests.ReferenceTests.Fixture;

public class ReferenceDataFixture : IDisposable
{
    public ReferenceContext ReferenceContext { get; private set; }

    public ReferenceDataFixture()
    {
        var options = new DbContextOptionsBuilder<ReferenceContext>()
            .UseInMemoryDatabase(databaseName: "MelinTestDatabase")
            .Options;
        
        ReferenceContext = new ReferenceContext(options);
        
        ReferenceContext.Reference.Add(new Reference { Id = 1, OwnerEmail = "test@example.com", Title = "Reference 1" });
        ReferenceContext.Reference.Add(new Reference { Id = 2, OwnerEmail = "test@example.com", Title = "Reference 2" });
        ReferenceContext.Reference.Add(new Reference { Id = 3, OwnerEmail = "otheruser@example.com", Title = "Reference 3" });
        ReferenceContext.Reference.Add(new Reference { Id = 4, OwnerEmail = "test@example.com", Title = "DELETE ME!" });

        ReferenceContext.SaveChanges();
    }

    public void Dispose()
    {
        ReferenceContext.Dispose();
    }
}