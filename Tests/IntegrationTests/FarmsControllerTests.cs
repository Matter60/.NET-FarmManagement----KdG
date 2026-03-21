using System.Net;
using System.Net.Http.Json;
using FarmManagement.DAL.EF;
using FarmManagement.UI.MVC.Tests.IntegrationTests.Config;
using FarmManagement.UI.Web.Models.Dto;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;

namespace FarmManagement.UI.MVC.Tests.IntegrationTests;

public class FarmsControllerTests : IClassFixture<ExtendedWebApplicationFactory<Program>>
{
    private readonly ExtendedWebApplicationFactory<Program> _factory;

    private const string AdminEmail = "lars@kdg.be";
    private const string AdminPassword = "Password1!";

    private const string UserEmail = "chef@kdg.be";
    private const string UserPassword = "Password1!";

    private const string TestFarmName = "Sunny Meadow Farm";

    public FarmsControllerTests(ExtendedWebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    #region Put (custom owner/admin authorization)
    [Fact]
    public void Put_AsNotLoggedInUser_Returns401Unauthorized()
    {
        // Arrange
        using var client = _factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });

        int farmId;
        using var scope = _factory.CreateDbContextScope<FarmManagementDbContext>();
        farmId = scope.DbContext.Farms.Single(f => f.Name == TestFarmName).Id;
        

        // Act
        var response = client.PutAsJsonAsync($"/api/Farms/{farmId}", new UpdateFarmDto { SizeInHectares = 99 }).Result;

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public void Put_AsUnauthorizedUser_Returns403Forbidden()
    {
        // Arrange
        using var client = _factory.CreateAuthenticatedClient(UserEmail, UserPassword,
            options: new WebApplicationFactoryClientOptions { AllowAutoRedirect = false });

        int farmId;
        var scope = _factory.CreateDbContextScope<FarmManagementDbContext>();
        
            farmId = scope.DbContext.Farms
                .Include(f => f.Maintainer)
                .Single(f => f.Name ==TestFarmName).Id;
        

        // Act
        var response = client.PutAsJsonAsync($"/api/Farms/{farmId}", new UpdateFarmDto { SizeInHectares = 123.45 }).Result;

        // Assert
        Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
    }

    [Fact]
    public void Put_AsAuthorizedUser_Returns204NoContent_GivenOwner()
    {
        // Arrange
        using var client = _factory.CreateAuthenticatedClient(AdminEmail, AdminPassword,
            options: new WebApplicationFactoryClientOptions { AllowAutoRedirect = false });

        int farmId;
        double? originalSize;
        using var scope = _factory.CreateDbContextScope<FarmManagementDbContext>();
        
        var farm = scope.DbContext.Farms
            .Include(f => f.Maintainer)
            .Single(f => f.Name == TestFarmName);
        farmId = farm.Id;
        originalSize = farm.SizeInHectares;
        

        var newSize = originalSize  + 3.33;

        // Act
        var response = client.PutAsJsonAsync($"/api/Farms/{farmId}", new UpdateFarmDto { SizeInHectares = newSize }).Result;

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        
        scope.DbContext.ChangeTracker.Clear();
        
        var updated = scope.DbContext.Farms.Single(f => f.Id == farmId);
        Assert.NotNull(updated);
        Assert.Equal( newSize,  updated.SizeInHectares);
    }

    [Fact]
    public void Put_AsAuthorizedUser_Returns404NotFound_GivenInvalidId()
    {
        // Arrange
        using var client = _factory.CreateAuthenticatedClient(AdminEmail, AdminPassword,
            options: new WebApplicationFactoryClientOptions { AllowAutoRedirect = false });

        var invalidId = -1;

        // Act
        var response = client.PutAsJsonAsync($"/api/Farms/{invalidId}", new UpdateFarmDto { SizeInHectares = 55.5 }).Result;

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
    #endregion
}

