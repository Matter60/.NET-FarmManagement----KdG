using System.Security.Claims;
using FarmManagement.BL;
using FarmManagement.BL.Domain;
using FarmManagement.UI.Web.Controllers;
using FarmManagement.UI.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace FarmManagement.UI.MVC.Tests.UnitTests;

public class FarmControllerTests
{
    private readonly Mock<IManager> _mock;
    private readonly Mock<UserManager<IdentityUser>> _mockUserManager;
    private readonly FarmController _ctr;

    public FarmControllerTests()
    {
        _mock = new Mock<IManager>();
        _mockUserManager = GetMockUserManager<IdentityUser>();
        _ctr = new FarmController(_mock.Object, _mockUserManager.Object);
    }

   

    #region Add POST

    [Fact]
    public void Add_Post_ReturnsAddViewWithValidationMessages_GivenInvalidViewModelData()
    {
        // Arrange
        string invalidName = ""; // invalid: Required
        var vm = new NewFarmViewModel()
        {
            Name = invalidName, Location = "Belgium", EstablishedYear = 2000
        };

        _ctr.ModelState.AddModelError("Name", "The Name field is required");

        // Act
        var result = _ctr.Add(vm);

        // Assert
        Assert.IsType<ViewResult>(result);
        var viewResult = (ViewResult)result;
        Assert.Equal("Add", viewResult.ViewName ?? nameof(FarmController.Add));

        /* mock-verification */
        _mock.Verify(
            mgr => mgr.AddFarm(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(),
                It.IsAny<double?>(), It.IsAny<string>()),
            Times.Never);
    }

    [Fact]
    public void Add_Post_ReturnsRedirectToDetails_GivenValidViewModelData()
    {
        // Arrange
        var vm = new NewFarmViewModel()
        {
            Name = "Sunny Farm", Location = "Belgium",
            EstablishedYear = 2000, SizeInHectares = 50
        };

        var username = "testuser@kdg.be";

        // authorized user
        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.Name, username),
        }, "mockAuthType"));
        _ctr.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };

        // mock-setup
        _mockUserManager.Setup(um => um.GetUserName(user)).Returns(username);
        
        var newFarmId = 42;
        var returnedFarm = new Farm()
        {
            Id = newFarmId, Name = vm.Name, Location = vm.Location,
            EstablishedYear = vm.EstablishedYear, SizeInHectares = vm.SizeInHectares,
            Maintainer = new IdentityUser { UserName = username }
        };
        _mock.Setup(mgr => mgr.AddFarm(vm.Name, vm.Location, vm.EstablishedYear, vm.SizeInHectares, username))
            .Returns(returnedFarm)
            .Verifiable(Times.Exactly(1));

        // Act
        var result = _ctr.Add(vm);

        // Assert
        Assert.IsType<RedirectToActionResult>(result);
        var redirectResult = (RedirectToActionResult)result;
        Assert.Equal("Details", redirectResult.ActionName);
        Assert.Equal(newFarmId, redirectResult.RouteValues?["id"]);

        /* mock-verification */
        _mock.VerifyAll();
    }

    #endregion


     // creating a mock of UserManager
    private Mock<UserManager<TUser>> GetMockUserManager<TUser>()
        where TUser : class
    {
        var userManagerMock = new Mock<UserManager<TUser>>(
            new Mock<IUserStore<TUser>>().Object,
            new Mock<IOptions<IdentityOptions>>().Object,
            new Mock<IPasswordHasher<TUser>>().Object,
            new IUserValidator<TUser>[0],
            new IPasswordValidator<TUser>[0],
            new Mock<ILookupNormalizer>().Object,
            new Mock<IdentityErrorDescriber>().Object,
            new Mock<IServiceProvider>().Object,
            new Mock<ILogger<UserManager<TUser>>>().Object);
        return userManagerMock;
    }
}
