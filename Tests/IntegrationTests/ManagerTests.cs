using System.ComponentModel.DataAnnotations;
using FarmManagement.BL;
using FarmManagement.BL.Domain;
using FarmManagement.DAL.EF;
using FarmManagement.UI.MVC.Tests.IntegrationTests.Config;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FarmManagement.UI.MVC.Tests.IntegrationTests;

public class ManagerTests : IClassFixture<ExtendedWebApplicationFactory<Program>>
{
    private readonly ExtendedWebApplicationFactory<Program> _factory;

    public ManagerTests(ExtendedWebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    #region AddAnimal
    [Fact]
    public void AddAnimal_ThrowsValidationException_GivenInvalidData()
    {
        // Arrange
        using var scope = _factory.Services.CreateScope();
        var mgr = scope.ServiceProvider.GetService<IManager>();

        var invalidSpecies = ""; // invalid: string length must be 2 characters

        // Act
        var exception = Record.Exception(() =>
            mgr.AddAnimal(invalidSpecies, lifespan: 10, averageWeight: 500, type: AnimalType.Cow));

        // Assert
        Assert.IsType<ValidationException>(exception);

        using var ctx = scope.ServiceProvider.GetService<FarmManagementDbContext>();
        var animal = ctx.Animals.SingleOrDefault(a => a.Species == invalidSpecies);
        Assert.Null(animal);
    }

    [Fact]
    public void AddAnimal_PersistsAndReturnsAnimal_GivenValidData()
    {
        // Arrange
        using var scope = _factory.Services.CreateScope();
        var mgr = scope.ServiceProvider.GetService<IManager>();

        var species = "TestSpecies123445";

        // Act
        var animal = mgr.AddAnimal(species, lifespan: 7, averageWeight: 123.4, type: AnimalType.Goat);

        // Assert
        Assert.NotNull(animal);


        using var ctx = scope.ServiceProvider.GetService<FarmManagementDbContext>();
        var animalInDb = ctx.Animals.SingleOrDefault(a => a.Species == species);
        Assert.NotNull(animalInDb);
        Assert.Equal(species, animalInDb.Species);

    }
    #endregion

    #region GetFarm
    [Fact]
    public void GetFarm_ReturnsFarm_GivenValidId()
    {
        // Arrange
        using var scope = _factory.Services.CreateScope();
        var mgr = scope.ServiceProvider.GetService<IManager>();

        var farmId = 1;

        // Act
        var farm = mgr.GetFarm(farmId);

        // Assert
        Assert.NotNull(farm);
        Assert.IsType<Farm>(farm);
        Assert.Equal(farmId, farm.Id);
    }

    [Fact]
    public void GetFarm_ReturnsNull_GivenInvalidId()
    {
        // Arrange
        using var scope = _factory.Services.CreateScope();
        var mgr = scope.ServiceProvider.GetService<IManager>();

        var invalidId = -1;

        // Act
        var farm = mgr.GetFarm(invalidId);

        // Asserrt
        Assert.Null(farm);
    }

    #endregion
}