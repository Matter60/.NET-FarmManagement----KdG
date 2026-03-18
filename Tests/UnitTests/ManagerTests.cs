using System.ComponentModel.DataAnnotations;
using FarmManagement.BL;
using FarmManagement.BL.Domain;
using FarmManagement.DAL;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace FarmManagement.UI.MVC.Tests.UnitTests;

public class ManagerTests
{
    private readonly Mock<IRepository> _mock;
    private readonly IManager _mgr;

    public ManagerTests()
    {
        _mock = new Mock<IRepository>();
        _mgr = new Manager(_mock.Object);
    }

    #region AddAnimal

    [Fact]
    public void AddAnimal_DoesNotThrowException_GivenValidAnimal()
    {
        // Arrange
        var species = "German";
        var lifespan = 20;
        var averageWeight = 600.0;
        var type = AnimalType.Cow;

        _mock.Setup(repo => repo.CreateAnimal(It.IsAny<Animal>()))
            .Verifiable(Times.Exactly(1));

        // Act
        var exception = Record.Exception(() => _mgr.AddAnimal(species, lifespan, averageWeight, type));

        // Assert
        Assert.Null(exception);

        /* mock-verification */
        _mock.VerifyAll();
    }

    [Fact]
    public void AddAnimal_ThrowsValidationException_GivenInvalidAnimal()
    {
        // Arrange
        var invalidSpecies = ""; // invalid: req + minlength = 2
        var lifespan = 20;
        var averageWeight = 600.0;
        var type = AnimalType.Cow;

        // Act
        var exception = Record.Exception(() => _mgr.AddAnimal(invalidSpecies, lifespan, averageWeight, type));

        // Assert
        Assert.IsType<ValidationException>(exception);

        /* mock-verification */
        _mock.Verify(repo => repo.CreateAnimal(It.IsAny<Animal>()), Times.Never);
    }

    #endregion

    #region ChangeFarm

    [Fact]
    public void ChangeFarm_DoesNotThrowException_GivenValidFarm()
    {
        // Arrange
        var validFarm = new Farm
        {
            Id = 1,
            Name = "Sunny Farm",
            Location = "Belgium",
            EstablishedYear = 2000,
            SizeInHectares = 50,
            Maintainer = new IdentityUser { UserName = "testuser@kdg.be" }
        };

        _mock.Setup(repo => repo.UpdateFarm(validFarm))
            .Verifiable(Times.Exactly(1));

        // Act
        var exception = Record.Exception(() => _mgr.ChangeFarm(validFarm));

        // Assert
        Assert.Null(exception);

        /* mock-verification */
        _mock.VerifyAll();
    }

    [Fact]
    public void ChangeFarm_ThrowsValidationException_GivenInvalidFarm()
    {
        // Arrange
        var futureYear = DateTime.Now.Year + 1; // invalid: can't be future
        var invalidFarm = new Farm
        {
            Id = 1,
            Name = "Future Farm",
            Location = "Belgium",
            EstablishedYear = futureYear,
            SizeInHectares = 30,
            Maintainer = new IdentityUser { UserName = "testuser@kdg.be" }
        };

        // Act
        var exception = Record.Exception(() => _mgr.ChangeFarm(invalidFarm));

        // Assert
        Assert.IsType<ValidationException>(exception);
        Assert.Contains("Established year cannot be in the future", exception.Message, StringComparison.OrdinalIgnoreCase);

        /* mock-verification */
        _mock.Verify(repo => repo.UpdateFarm(It.IsAny<Farm>()), Times.Never);
    }

    #endregion
}
