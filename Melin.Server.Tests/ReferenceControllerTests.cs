using System.ComponentModel.DataAnnotations;
using Melin.Server.Controllers;
using Melin.Server.Filter;
using Melin.Server.Interfaces;
using Melin.Server.Models;
using Melin.Server.Models.Repository;
using Melin.Server.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Task = Melin.Server.Models.Task;

namespace Melin.Server.Tests;


public class ReferenceControllerTests {
    private readonly ApiService _apiService;
    private readonly ReferenceContext _referenceContext;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly TagService _tagService;
    private readonly IUnitOfWork _unitOfWork;
    
    [Fact]
    public void GetReferences_Returns_OkResult()
    {
        var referenceRepositoryMock = new Mock<IReferenceService>();
        referenceRepositoryMock.Setup(repo => repo.GetAllReferences())
            .Returns(new List<Reference>());

        var result = referenceRepositoryMock.Object.GetAllReferences();

        Assert.IsType<List<Reference>>(result);
    }
}