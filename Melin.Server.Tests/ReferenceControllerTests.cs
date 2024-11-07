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
    public async void GetReference_Returns_OkResult()
    {
        var referenceRepositoryMock = new Mock<IReferenceRepository>();
        referenceRepositoryMock.Setup(repo => repo.GetAll())
            .Returns(new List<Reference>());

        var referenceService = new ReferenceRepository(referenceRepositoryMock.Object);
        
        var controller = new ReferenceController(_apiService, _referenceContext, _userManager, _tagService, _unitOfWork);
        
        var result = await controller.GetSingleReference(1);

        Assert.IsType<Ok>(result);
    }
}