using System.Net;
using FarmManagement.DAL.EF;
using FarmManagement.UI.MVC.Tests.IntegrationTests.Config;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FarmManagement.UI.MVC.Tests.IntegrationTests;

public class FarmControllerTests : IClassFixture<ExtendedWebApplicationFactory<Program>>
{
    private readonly ExtendedWebApplicationFactory<Program> _factory;

    private const string AdminEmail = "lars@kdg.be";
    private const string AdminPassword = "Password1!";

    public FarmControllerTests(ExtendedWebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    #region Add POST
    [Fact]
    public void Add_AsNotLoggedInUser_ReturnRedirectToLogin()
    {
        // Arrange
        var client = _factory.CreateClient(new WebApplicationFactoryClientOptions()
        {
            AllowAutoRedirect = false
        });

        var url = "/Farm/Add";

        // Act
        var response = client.PostAsync(url, null).Result;

        // Assert
        Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
        Assert.Equal("/Identity/Account/Login", response.Headers.Location?.AbsolutePath);
    }

    [Fact]
    public void Add_AsAuthorizedUser_ReturnsBadRequest_GivenInvalidCsrf()
    {
        // Arrange
         var client = _factory.CreateAuthenticatedClient(AdminEmail, AdminPassword,
            options: new WebApplicationFactoryClientOptions()
            {
                AllowAutoRedirect = false
            });
         
         var url = "/Farm/Add";
         
        var invalidCsrf = "INVALID_CSRF_TOKEN";
        var data = new Dictionary<string, string>
        {
            { "Name", "Large barn" },
            { "Location", "Belgium" },
            { "EstablishedYear", DateTime.Now.Year.ToString() },
            { "SizeInHectares", "10" },
            { "__RequestVerificationToken", invalidCsrf }
        };

        // Act
        var response = client.PostAsync(url, new FormUrlEncodedContent(data)).Result;

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public void Add_AsAuthorizedUser_ShowsValidationMessages_GivenInvalidData_WithValidCsrf()
    {
        // Arrange
        var client = _factory.CreateAuthenticatedClient(AdminEmail, AdminPassword,
            options: new WebApplicationFactoryClientOptions { AllowAutoRedirect = false });

        var url = "/Farm/Add";
        var csrf = client.GetCsrfToken(url);

        var invalidFutureYear = DateTime.Now.Year + 1;

        var name ="Large barn";
        var location = "Belgium";
        var sizeInHectares = "10";
        var data = new Dictionary<string, string>
        {
            { "Name", name },
            { "Location", location },
            { "EstablishedYear", invalidFutureYear.ToString() }, // invalid by IValidatableObject
            { "SizeInHectares", sizeInHectares },
            { "__RequestVerificationToken", csrf }
        };

        

        // Act
        var response = client.PostAsync(url, new FormUrlEncodedContent(data)).Result;
        var body = response.Content.ReadAsStringAsync().Result;

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Contains("Established year cannot be in the future", body, StringComparison.OrdinalIgnoreCase);

        using var scope = _factory.CreateDbContextScope<FarmManagementDbContext>();
        var ctx = scope.DbContext;
        
        var createdFarm = ctx.Farms.SingleOrDefault(f => f.Name == name &&  f.EstablishedYear == invalidFutureYear && f.SizeInHectares == 10 && f.Location == location);
        Assert.Null(createdFarm);
    }

    [Fact]
    public void Add_AsAuthorizedUser_StoresDataAndReturnRedirectToDetails_GivenValidData_WithValidCsrf()
    {
        // Arrange
        using var client = _factory.CreateAuthenticatedClient(AdminEmail, AdminPassword,
            options: new WebApplicationFactoryClientOptions { AllowAutoRedirect = false });

        var url = "/Farm/Add";
        var csrf = client.GetCsrfToken(url);

        var uniqueName = "Test Farm 123455555";
        var location = "Netherlands";
        
        var data = new Dictionary<string, string>
        {
            { "Name", uniqueName },
            { "Location", location },
            { "EstablishedYear", DateTime.Now.Year.ToString() },
            { "SizeInHectares", "25.5" },
            { "__RequestVerificationToken", csrf }
        };

        // Act
        var response = client.PostAsync(url, new FormUrlEncodedContent(data)).Result;

        // Assert
        Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
        Assert.Matches("^.*/Farm/Details/\\d+$", response.Headers.Location?.OriginalString);

        var scope = _factory.CreateDbContextScope<FarmManagementDbContext>();
        var ctx = scope.DbContext;
        
        var created = ctx.Farms
            .SingleOrDefault(f => f.Name == uniqueName && f.EstablishedYear == DateTime.Now.Year &&  f.Location ==  location);
        Assert.NotNull(created);
       
    }
    #endregion
}

