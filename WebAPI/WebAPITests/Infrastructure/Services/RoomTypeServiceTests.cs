using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Domain.Entities;
using Domain.Repositories;
using Domain.Services;
using Infrastructure.Services;
using Moq;
using NUnit.Framework;
using RoomTypeService = Infrastructure.Services.RoomTypeService;

namespace WebAPITests.Infrastructure.Services
{
    [TestFixture]
    public class RoomTypeServiceTests
    {
        private Mock<IRoomTypeRepository> _roomTypeRepoMock;
        private Mock<IPropertyRepository> _propertyRepoMock;
        private Mock<ICurrencyRepository> _currencyRepoMock;
        private Mock<IServiceService> _serviceServiceMock;
        private Mock<IAmenityService> _amenityServiceMock;
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private RoomTypeService _sut;

        [SetUp]
        public void Setup()
        {
            _roomTypeRepoMock = new Mock<IRoomTypeRepository>();
            _propertyRepoMock = new Mock<IPropertyRepository>();
            _currencyRepoMock = new Mock<ICurrencyRepository>();
            _serviceServiceMock = new Mock<IServiceService>();
            _amenityServiceMock = new Mock<IAmenityService>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();

            _sut = new RoomTypeService(
                _roomTypeRepoMock.Object,
                _propertyRepoMock.Object,
                _currencyRepoMock.Object,
                _serviceServiceMock.Object,
                _amenityServiceMock.Object,
                _unitOfWorkMock.Object
            );
        }

        [Test]
        public async Task GetRoomTypeByIdAsync_when_exist_return_RoomType()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            RoomType roomType = new() { Id = id, Name = "Room" };
            _roomTypeRepoMock.Setup(r => r.GetRoomTypeByIdAsync(id)).ReturnsAsync(roomType);

            // Act
            RoomType? result = await _sut.GetRoomTypeByIdAsync(id);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(id));
            Assert.That(result.Name, Is.EqualTo("Room"));
        }
        
        [Test]
        public async Task GetRoomTypeByIdAsync_when_doesnt_exist_return_null()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            _roomTypeRepoMock.Setup(r => r.GetRoomTypeByIdAsync(id)).ReturnsAsync((RoomType?)null);

            // Act
            RoomType? result = await _sut.GetRoomTypeByIdAsync(id);

            // Assert
            Assert.That(result, Is.Null);
        }
        
        [Test]
        public async Task GetAllRoomTypesAsync_when_RoomTypes_empty_return_empty_list()
        {
            // Arrange
            _roomTypeRepoMock.Setup(r => r.GetAllRoomTypesAsync()).ReturnsAsync(new List<RoomType?>());

            // Act
            IEnumerable<RoomType?> result = await _sut.GetAllRoomTypesAsync();

            // Assert
            Assert.That(result, Is.Empty);
        }
        
        [Test]
        public async Task GetAllRoomTypesAsync_when_RoomTypes_exist_return_all_RoomTypes()
        {
            // Arrange
            List<RoomType> roomTypes = new()
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    Name = "Room First",
                    Property = new() { Id = Guid.NewGuid(), Name = "Property First", Country = "Country", City = "City", Address = "Address" }
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    Name = "Room Second",
                    Property = new() { Id = Guid.NewGuid(), Name = "Property Second", Country = "Country", City = "City", Address = "Address" }
                }
            };

            _roomTypeRepoMock.Setup(r => r.GetAllRoomTypesAsync()).ReturnsAsync(roomTypes);

            // Act
            IEnumerable<RoomType?> result = await _sut.GetAllRoomTypesAsync();

            // Assert
            List<RoomType?> enumerable = result.ToList();
            Assert.That(enumerable.Count, Is.EqualTo(2));
            Assert.That(enumerable, Has.Exactly(1).Matches<RoomType>(r => r?.Name == "Room First"));
            Assert.That(enumerable, Has.Exactly(1).Matches<RoomType>(r => r?.Name == "Room Second"));
        }
        
        [Test]
        public async Task GetRoomTypesByPropertyIdAsync_when_RoomTypes_exist_return_RoomTypes_with_property()
        {
            // Arrange
            Guid propertyId = Guid.NewGuid();
            List<RoomType> roomTypes = new()
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    PropertyId = propertyId,
                    Name = "Room Type",
                    Property = new Property
                    {
                        Id = propertyId,
                        Name = "Property",
                        Country = "Country",
                        City = "City",
                        Address = "Address"
                    }
                }
            };

            _roomTypeRepoMock.Setup(r => r.GetRoomTypesByPropertyIdAsync(propertyId)).ReturnsAsync(roomTypes);

            // Act
            IEnumerable<RoomType?> result = await _sut.GetRoomTypesByPropertyIdAsync(propertyId);

            // Assert
            List<RoomType?> enumerable = result.ToList();
            Assert.That(enumerable.Count, Is.EqualTo(1));
            Assert.That(enumerable.First()?.PropertyId, Is.EqualTo(propertyId));
            Assert.That(enumerable.First()?.Name, Is.EqualTo("Room Type"));
        }
        
        [Test]
        public async Task DeleteRoomTypeAsync_when_RoomType_doesnt_exists_return_false()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            _roomTypeRepoMock.Setup(r => r.GetRoomTypeByIdAsync(id)).ReturnsAsync((RoomType?)null);

            // Act
            bool result = await _sut.DeleteRoomTypeAsync(id);

            // Assert
            Assert.That(result, Is.False);
        }
        
        [Test]
        public async Task UpdateRoomTypeAsync_when_RoomType_exist_updates_fields()
        {
            // Arrange
            Guid roomTypeId = Guid.NewGuid();

            Property property = new()
            {
                Id = Guid.NewGuid(),
                Name = "Property",
                Country = "Country",
                City = "City",
                Address = "Address"
            };

            RoomType existingRoomType = new()
            {
                Id = roomTypeId,
                Name = "Old Name",
                Property = property,
                RoomTypeServices = new List<Domain.Entities.RoomTypeService>(),
                RoomTypeAmenities = new List<RoomTypeAmenity>()
            };

            RoomType updatedRoomType = new()
            {
                Id = roomTypeId,
                Name = "New Name",
                Property = property,
                RoomTypeServices = new List<Domain.Entities.RoomTypeService>(),
                RoomTypeAmenities = new List<RoomTypeAmenity>()
            };

            _roomTypeRepoMock.SetupSequence(r => r.GetRoomTypeByIdAsync(roomTypeId))
                .ReturnsAsync(existingRoomType)
                .ReturnsAsync(updatedRoomType); ;

            _currencyRepoMock.Setup(c => c.GetCurrencyByTypeAsync("RUB"))
                .ReturnsAsync(new Currency { Id = 1, Type = "RUB" });

            // Act
            RoomType? result = await _sut.UpdateRoomTypeAsync(updatedRoomType, "RUB", new List<string>(), new List<string>());

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result?.Name, Is.EqualTo("New Name"));
            Assert.That(result?.CurrencyId, Is.EqualTo(1));
            Assert.That(result?.Property, Is.EqualTo(property));
        }
        
        [Test]
        public async Task DeleteRoomTypeAsync_when_exist_return_true()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            RoomType roomType = new() { Id = id, Name = "Room" };
            _roomTypeRepoMock.Setup(r => r.GetRoomTypeByIdAsync(id)).ReturnsAsync(roomType);

            // Act
            bool result = await _sut.DeleteRoomTypeAsync(id);

            // Assert
            Assert.That(result, Is.True);
        }
        
        [Test]
        public void CreateRoomTypeAsync_when_property_doesnt_exist_throws_exception()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            Guid propertyId = Guid.NewGuid();
            RoomType roomType = new() { Id = id, Name = "Room" };

            _propertyRepoMock.Setup(p => p.GetPropertyByIdAsync(propertyId))
                .ReturnsAsync((Property?)null);

            // Act and Assert
            Assert.ThrowsAsync<Exception>(async () =>
                await _sut.CreateRoomTypeAsync(propertyId, roomType, "RUB", [ ], [ ] ));
        }
        
        [Test]
        public void CreateRoomTypeAsync_when_currency_doesnt_exist_throws_exception()
        {
            // Arrange
            Guid propertyId = Guid.NewGuid();
            RoomType roomType = new() { Name = "Room" };

            _propertyRepoMock.Setup(p => p.GetPropertyByIdAsync(propertyId))
                .ReturnsAsync(new Property { Id = propertyId, Name = "Test Hotel", Country = "Russia", City = "Kazan", Address = "Street" });
            _currencyRepoMock.Setup(c => c.GetCurrencyByTypeAsync("RUB")).ReturnsAsync((Currency?)null);

            // Act and Assert
            Assert.ThrowsAsync<Exception>(async () =>
                await _sut.CreateRoomTypeAsync(propertyId, roomType, "RUB", [ ], []));
        }
        
        [Test]
        public async Task CreateRoomTypeAsync_when_property_and_currency_exist_creates_RoomType()
        {
            // Arrange
            Property property = new()
            {
                Id = Guid.NewGuid(),
                Name = "Test Hotel",
                Country = "Russia",
                City = "Kazan",
                Address = "Street"
            };
            RoomType roomType = new() { Name = "Room" };
            Currency currency = new() { Id = 1, Type = "RUB" };
            List<string> services = new() { "Transfer" };
            List<string> amenities = new() { "SwimmingPool" };

            _propertyRepoMock.Setup(p => p.GetPropertyByIdAsync(property.Id))
                .ReturnsAsync(property);
            _currencyRepoMock.Setup(c => c.GetCurrencyByTypeAsync("RUB")).ReturnsAsync(currency);
            _serviceServiceMock.Setup(s => s.GetServiceByNameAsync(It.IsAny<string>())).ReturnsAsync((Service?)null);
            _amenityServiceMock.Setup(a => a.GetAmenityByNameAsync(It.IsAny<string>())).ReturnsAsync((Amenity?)null);

            // Act
            RoomType? result = await _sut.CreateRoomTypeAsync(property.Id, roomType, "RUB", services, amenities);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.Not.EqualTo(Guid.Empty));
            Assert.That(result.PropertyId, Is.EqualTo(property.Id));
            Assert.That(result.CurrencyId, Is.EqualTo(currency.Id));
            Assert.That(result.RoomTypeServices.Count, Is.EqualTo(1));
            Assert.That(result.RoomTypeAmenities.Count, Is.EqualTo(1));
        }
    }
}
