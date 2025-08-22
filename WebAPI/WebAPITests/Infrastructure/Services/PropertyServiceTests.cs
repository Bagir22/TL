using Domain;
using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Services;
using Moq;

namespace WebAPITests.Infrastructure.Services;

[TestFixture]
public class PropertyServiceTests
{
    private Mock<IPropertyRepository> _propertyRepoMock;
    private Mock<IUnitOfWork> _unitOfWorkMock;
    private PropertyService _sut; 

    [SetUp]
    public void Setup()
    {
        _propertyRepoMock = new Mock<IPropertyRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _sut = new PropertyService( _propertyRepoMock.Object, _unitOfWorkMock.Object );
    }
    
    [Test]
    public async Task GetPropertyByIdAsync_when_property_exist_return_property()
    {
        // Arrange
        Property property = new() { Id = Guid.NewGuid(), Name = "Test Hotel", Country = "Russia", City = "Kazan", Address = "Street" };

        _propertyRepoMock.Setup(r => r.GetPropertyByIdAsync(property.Id))
            .ReturnsAsync(property);

        // Act
        Property? result = await _sut.GetPropertyByIdAsync(property.Id);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Id, Is.EqualTo(property.Id));
        Assert.That(result.Name, Is.EqualTo("Test Hotel"));
        Assert.That(result.Country, Is.EqualTo("Russia"));
        Assert.That(result.City, Is.EqualTo("Kazan"));
        Assert.That(result.Address, Is.EqualTo("Street"));
    }
    
    [Test]
    public async Task GetPropertyByIdAsync_when_property_doesnt_exist_return_null()
    {
        // Arange
        Guid id = Guid.NewGuid();
        
        _propertyRepoMock.Setup(r => r.GetPropertyByIdAsync(id))
            .ReturnsAsync((Property?)null);

        // Act
        Property? result = await _sut.GetPropertyByIdAsync(id);

        // Assert
        Assert.That(result, Is.Null);
    }
    
    [Test]
    public async Task GetAllPropertiesAsync_when_properties_exist_return_all_properties()
    {
        // Arrange
        List<Property> properties = new()
        {
            new() { Id = Guid.NewGuid(), Name = "Hotel First", Country = "Russia", City = "Kazan", Address = "Street" },
            new() { Id = Guid.NewGuid(), Name = "Hotel Second", Country = "Russia", City = "Kazan", Address = "Street" }
        };

        _propertyRepoMock.Setup(r => r.GetAllPropertiesAsync())
            .ReturnsAsync(properties);

        // Act
        IEnumerable<Property> result = await _sut.GetAllPropertiesAsync();

        // Assert
        IEnumerable<Property> enumerable = result.ToList();
        Assert.That(enumerable, Is.Not.Null);
        Assert.That(enumerable.Count(), Is.EqualTo(2));
        Assert.That(enumerable, Has.Exactly(1).Matches<Property>(p => p.Name == "Hotel First"));
        Assert.That(enumerable, Has.Exactly(1).Matches<Property>(p => p.Name == "Hotel Second"));
    }

    [Test]
    public async Task GetAllPropertiesAsync_when_properties_doesnt_exist_return_empty_list()
    {
        // Arrange
        _propertyRepoMock.Setup(r => r.GetAllPropertiesAsync())
            .ReturnsAsync(new List<Property>());

        // Act
        IEnumerable<Property> result = await _sut.GetAllPropertiesAsync();

        // Assert
        IEnumerable<Property> enumerable = result.ToList();
        Assert.That(enumerable, Is.Not.Null);
        Assert.That(enumerable, Is.Empty);
    }
    
    [Test]
    public void UpdatePropertyAsync_when_property_not_exist_throws_exception()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        Property updatedProperty = new() { Id = id, Name = "Test Hotel", Country = "Russia", City = "Kazan", Address = "Street", };

        _propertyRepoMock.Setup(r => r.GetPropertyByIdAsync(id))
            .ReturnsAsync((Property?)null);

        // Act and Assert
        Assert.ThrowsAsync<Exception>(async () =>
            await _sut.UpdatePropertyAsync(id, updatedProperty));
    }

    [Test]
    public async Task UpdatePropertyAsync_when_property_exists_updates_fields()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        Property existingProperty = new()
        {
            Id = id,
            Name = "Old Name",
            Country = "Old Country",
            City = "Old City",
            Address = "Old Address",
            Latitude = 20,
            Longitude = 50
        };

        Property updatedProperty = new()
        {
            Name = "New Name",
            Country = "New Country",
            City = "New City",
            Address = "New Address",
            Latitude = 30,
            Longitude = 60
        };

        _propertyRepoMock.Setup( r => r.GetPropertyByIdAsync( id ) )
            .ReturnsAsync( existingProperty );

        // Act
        Property? result = await _sut.UpdatePropertyAsync( id, updatedProperty );

        // Assert
        Assert.That( result, Is.Not.Null );
        Assert.That( result.Name, Is.EqualTo( "New Name" ) );
        Assert.That( result.Country, Is.EqualTo( "New Country" ) );
        Assert.That( result.City, Is.EqualTo( "New City" ) );
        Assert.That( result.Address, Is.EqualTo( "New Address" ) );
        Assert.That( result.Latitude, Is.EqualTo( 30 ) );
        Assert.That( result.Longitude, Is.EqualTo( 60 ) );
    }
    
    [Test]
    public async Task DeletePropertyAsync_when_property_doesnt_exist_return_false()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        _propertyRepoMock.Setup(r => r.GetPropertyByIdAsync(id))
            .ReturnsAsync((Property?)null);

        // Act
        bool result = await _sut.DeletePropertyAsync(id);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public async Task DeletePropertyAsync_when_property_exists_return_true()
    {
        // Arrange
        Property property = new() { Id = Guid.NewGuid(), Name = "Test Hotel", Country = "Russia", City = "Kazan", Address = "Street", };
        _propertyRepoMock.Setup(r => r.GetPropertyByIdAsync(property.Id))
            .ReturnsAsync(property);

        // Act
        bool result = await _sut.DeletePropertyAsync(property.Id);

        // Assert
        Assert.That(result, Is.True);
    }
}