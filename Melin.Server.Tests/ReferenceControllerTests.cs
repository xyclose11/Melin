using Melin.Server.Models;
using Melin.Server.Services;
using Microsoft.AspNetCore.Identity;

namespace Melin.Server.Tests;


public class ReferenceControllerTests {
    private readonly ApiService _apiService;
    private readonly ReferenceContext _referenceContext;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly TagService _tagService;
    
    [Fact]
    public void GetReferences_Returns_OkResult()
    {
        // var referenceRepositoryMock = new Mock<IReferenceService>();
        // referenceRepositoryMock.Setup(repo => repo.GetAllReferences())
        //     .Returns(new List<Reference>());
        //
        // var result = referenceRepositoryMock.Object.GetAllReferences();
        //
        // Assert.IsType<List<Reference>>(result);
    }
}