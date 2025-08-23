using Domain;
using Domain.Entities;
using Domain.Repositories;
using Domain.Services;

namespace Infrastructure.Services;

public class RoomTypeService : IRoomTypeService
{
    private readonly IRoomTypeRepository _roomTypeRepository;
    private readonly IPropertyRepository _propertyRepository;
    private readonly ICurrencyRepository _currencyRepository;
    private readonly IServiceService _serviceService;
    private readonly IAmenityService _amenityService;
    private readonly IUnitOfWork _unitOfWork;

    public RoomTypeService(
        IRoomTypeRepository roomTypeRepository,
        IPropertyRepository propertyRepository,
        ICurrencyRepository currencyRepository,
        IServiceService serviceService,
        IAmenityService amenityService,
        IUnitOfWork unitOfWork
    )
    {
        _roomTypeRepository = roomTypeRepository;
        _propertyRepository = propertyRepository;
        _currencyRepository = currencyRepository;
        _serviceService = serviceService;
        _amenityService = amenityService;
        _unitOfWork = unitOfWork;
    }

    public async Task<RoomType?> CreateRoomTypeAsync(
        Guid propertyId,
        RoomType roomType,
        string currencyType,
        List<string> services,
        List<string> amenities
    )
    {
        await CheckPropertyExistsAsync( propertyId );

        roomType.CurrencyId = await GetCurrencyIdAsync( currencyType );
        roomType.Id = Guid.NewGuid();
        roomType.PropertyId = propertyId;

        await AddServicesToRoomTypeAsync( roomType, services );
        await AddAmenitiesToRoomTypeAsync( roomType, amenities );

        await _roomTypeRepository.CreateRoomTypeAsync( roomType );
        await _unitOfWork.CommitAsync();

        return roomType;
    }

    public async Task<RoomType?> GetRoomTypeByIdAsync( Guid id )
    {
        return await _roomTypeRepository.GetRoomTypeByIdAsync( id );
    }

    public async Task<IEnumerable<RoomType?>> GetAllRoomTypesAsync()
    {
        return await _roomTypeRepository.GetAllRoomTypesAsync();
    }

    public async Task<IEnumerable<RoomType?>> GetRoomTypesByPropertyIdAsync( Guid propertyId )
    {
        return await _roomTypeRepository.GetRoomTypesByPropertyIdAsync( propertyId );
    }

    public async Task<RoomType?> UpdateRoomTypeAsync(
        RoomType updatedRoomType,
        string currencyType,
        List<string> services,
        List<string> amenities
    )
    {
        RoomType roomType = await GetRoomTypeAsync( updatedRoomType.Id );

        updatedRoomType.CurrencyId = await GetCurrencyIdAsync( currencyType );

        updatedRoomType.RoomTypeServices.Clear();
        await AddServicesToRoomTypeAsync( updatedRoomType, services );

        updatedRoomType.RoomTypeAmenities.Clear();
        await AddAmenitiesToRoomTypeAsync( updatedRoomType, amenities );

        await _roomTypeRepository.UpdateRoomTypeAsync( updatedRoomType );
        await _unitOfWork.CommitAsync();

        return await _roomTypeRepository.GetRoomTypeByIdAsync( updatedRoomType.Id );
    }

    public async Task<bool> DeleteRoomTypeAsync( Guid id )
    {
        RoomType? roomType = await _roomTypeRepository.GetRoomTypeByIdAsync( id );
        if ( roomType == null )
        {
            return false;
        }

        await _roomTypeRepository.DeleteRoomTypeAsync( id );
        await _unitOfWork.CommitAsync();

        return true;
    }

    private async Task<RoomType> GetRoomTypeAsync( Guid roomTypeId )
    {
        RoomType? roomType = await _roomTypeRepository.GetRoomTypeByIdAsync( roomTypeId );
        if ( roomType == null )
        {
            throw new Exception( $"Room type with id {roomTypeId} not found" );
        }

        return roomType;
    }

    private async Task<int> GetCurrencyIdAsync( string currencyType )
    {
        Currency? currency = await _currencyRepository.GetCurrencyByTypeAsync( currencyType );
        if ( currency == null )
        {
            throw new Exception( $"Currency with type '{currencyType}' not found" );
        }

        return currency.Id;
    }

    private async Task AddServicesToRoomTypeAsync( RoomType roomType, List<string> services )
    {
        foreach ( string serviceName in services.Distinct() )
        {
            Service? service = await _serviceService.GetServiceByNameAsync( serviceName );
            if ( service == null )
            {
                service = new Service
                {
                    Name = serviceName
                };
                await _serviceService.CreateServiceAsync( service );
            }

            roomType.RoomTypeServices.Add( new Domain.Entities.RoomTypeService
            {
                Service = service
            } );
        }
    }

    private async Task AddAmenitiesToRoomTypeAsync( RoomType roomType, List<string> amenities )
    {
        foreach ( string amenityName in amenities.Distinct() )
        {
            Amenity? amenity = await _amenityService.GetAmenityByNameAsync( amenityName );
            if ( amenity == null )
            {
                amenity = new Amenity
                {
                    Name = amenityName
                };
                await _amenityService.CreateAmenityAsync( amenity );
            }

            roomType.RoomTypeAmenities.Add( new RoomTypeAmenity
            {
                Amenity = amenity
            } );
        }
    }

    private async Task CheckPropertyExistsAsync( Guid propertyId )
    {
        Property? property = await _propertyRepository.GetPropertyByIdAsync( propertyId );
        if ( property == null )
        {
            throw new Exception( $"Property with id {propertyId} not found" );
        }
    }
}