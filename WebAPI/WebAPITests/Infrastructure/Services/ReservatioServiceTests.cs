using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Services;
using Moq;
using NUnit.Framework;
using System.ComponentModel.DataAnnotations;
using Domain;

namespace WebAPITests.Infrastructure.Services
{
    [TestFixture]
    public class ReservationServiceTests
    {
        private Mock<IReservationRepository> _reservationRepoMock;
        private Mock<IRoomTypeRepository> _roomTypeRepoMock;
        private Mock<IGuestRepository> _guestRepoMock;
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private ReservationService _sut;

        [SetUp]
        public void Setup()
        {
            _reservationRepoMock = new Mock<IReservationRepository>();
            _roomTypeRepoMock = new Mock<IRoomTypeRepository>();
            _guestRepoMock = new Mock<IGuestRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _sut = new ReservationService(
                _reservationRepoMock.Object,
                _guestRepoMock.Object,
                _roomTypeRepoMock.Object,
                _unitOfWorkMock.Object
            );
        }

        [Test]
        public void SearchAvailableAsync_invalid_arrival_date_throws_validation_exception()
        {
            Assert.ThrowsAsync<ValidationException>(async () =>
                await _sut.SearchAvailableAsync("City", "2025-08-32", "2025-08-15", 1));
        }

        [Test]
        public void SearchAvailableAsync_departure_before_arrival_throws_validation_exception()
        {
            Assert.ThrowsAsync<ValidationException>(async () =>
                await _sut.SearchAvailableAsync("City", "2025-08-21", "2025-08-18", 1));
        }

        [Test]
        public async Task SearchAvailableAsync_valid_dates_return_list()
        {
            // Arrange
            string arrival = "2025-08-18";
            string departure = "2025-08-21";

            _reservationRepoMock.Setup(r => r.SearchAvailableAsync(
                    "City", It.IsAny<DateOnly>(), It.IsAny<DateOnly>(), 1))
                .ReturnsAsync(new List<(RoomType, int)>());

            // Act
            IEnumerable<(RoomType RoomType, int AvailableRooms)> result = await _sut.SearchAvailableAsync("City", arrival, departure, 1);
            
            // Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void CreateReservationAsync_departure_before_arrival_throws_validation_exception()
        {
            // Arrange
            Reservation reservation = new()
            {
                ArrivalDateUTC = new DateOnly(2025, 8, 22),
                DepartureDateUTC = new DateOnly(2025, 8, 18),
                RoomTypeId = Guid.NewGuid(),
                
                Property = new Property
                {
                    Id = Guid.NewGuid(),
                    Name = "Name",
                    Country = "Country",
                    City = "City",
                    Address = "Address",
                },
                RoomType = new RoomType
                {
                    Id = Guid.NewGuid(),
                    Name = "Room"
                },
                Currency = new Currency
                {
                    Id = 1,
                    Type = "RUB"
                }
            };

            // Act and Assert
            Assert.ThrowsAsync<ValidationException>(async () =>
                await _sut.CreateReservationAsync(reservation, new List<Guest>()));
        }

        [Test]
        public async Task CreateReservationAsync_RoomType_not_found_throws_exception()
        {
            // Arrange
            Reservation reservation = new()
            {
                ArrivalDateUTC = new DateOnly(2025, 8, 18),
                DepartureDateUTC = new DateOnly(2025, 8, 22),
                RoomTypeId = Guid.NewGuid(),
                
                Property = new Property
                {
                    Id = Guid.NewGuid(),
                    Name = "Name",
                    Country = "Country",
                    City = "City",
                    Address = "Address",
                },
                RoomType = new RoomType
                {
                    Id = Guid.NewGuid(),
                    Name = "Room"
                },
                Currency = new Currency
                {
                    Id = 1,
                    Type = "RUB"
                }
            };

            _roomTypeRepoMock.Setup(r => r.GetRoomTypeByIdAsync(reservation.RoomTypeId))
                .ReturnsAsync((RoomType?)null);

            // Act and Assert
            Assert.ThrowsAsync<Exception>(async () =>
                await _sut.CreateReservationAsync(reservation, new List<Guest>()));
        }
        
        [Test]
        public async Task CreateReservationAsync_when_room_available_creates_reservation()
        {
            // Arrange
            RoomType roomType = new()
            {
                Id = Guid.NewGuid(),
                Name = "Room",
                RoomsCount = 2,
                DailyPrice = 100,
                CurrencyId = 1,
                
                Property = new Property
                {
                    Id = Guid.NewGuid(),
                    Name = "Property",
                    Country = "Country",
                    City = "City",
                    Address = "Address"
                }
            };

            Reservation reservation = new()
            {
                RoomTypeId = roomType.Id,
                ArrivalDateUTC = new DateOnly(2025, 8, 18),
                DepartureDateUTC = new DateOnly(2025, 8, 22),
                Property = roomType.Property,
                RoomType = roomType,
                Currency = new Currency { Id = 1, Type = "RUB" }
            };

            _roomTypeRepoMock.Setup(r => r.GetRoomTypeByIdAsync(roomType.Id)).ReturnsAsync(roomType);
            _reservationRepoMock.Setup(r => r.GetBookedCountAsync(
                    roomType.Id, reservation.ArrivalDateUTC, reservation.ArrivalTime, reservation.DepartureDateUTC, reservation.DepartureTime))
                .ReturnsAsync(1);

            _guestRepoMock.Setup(g => g.GetByPhoneNumberAsync(It.IsAny<string>())).ReturnsAsync((Guest?)null);

            // Act
            Reservation result = await _sut.CreateReservationAsync(reservation, new List<Guest>());

            // Assert
            Assert.That(result.RoomType.Name, Is.EqualTo("Room"));
            Assert.That(result.Total, Is.EqualTo(400));
            Assert.That(result.CurrencyId, Is.EqualTo(1));
        }


        [Test]
        public async Task DeleteReservationAsync_when_reservation_doesnt_exist_return_false()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            _reservationRepoMock.Setup(r => r.GetReservationByIdAsync(id)).ReturnsAsync((Reservation?)null);

            // Act
            bool result = await _sut.DeleteReservationAsync(id);

            // Assert
            Assert.That(result, Is.False);
        }

        
        [Test]
        public async Task DeleteReservationAsync_when_reservation_exists_return_true()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            Reservation reservation = new()
            {
                Id = id,
                Property = new Property
                {
                    Id = Guid.NewGuid(),
                    Name = "Property",
                    Country = "Country",
                    City = "City",
                    Address = "Address"
                },
                RoomType = new RoomType { Id = Guid.NewGuid(), Name = "Room" },
                Currency = new Currency { Id = 1, Type = "RUB" }
            };

            _reservationRepoMock.Setup(r => r.GetReservationByIdAsync(id)).ReturnsAsync(reservation);

            // Act
            bool result = await _sut.DeleteReservationAsync(id);
            
            // Assert
            Assert.That(result, Is.True);
        }
    }
}
