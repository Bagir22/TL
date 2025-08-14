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
    private readonly IServiceRepository _serviceRepository;
    private readonly IAmenityRepository _amenityRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RoomTypeService(
        IRoomTypeRepository roomTypeRepository,
        IPropertyRepository propertyRepository,
        ICurrencyRepository currencyRepository,
        IServiceRepository serviceRepository,
        IAmenityRepository amenityRepository,
        IUnitOfWork unitOfWork
    )
    {
        _roomTypeRepository = roomTypeRepository;
        _propertyRepository = propertyRepository;
        _currencyRepository = currencyRepository;
        _serviceRepository = serviceRepository;
        _amenityRepository = amenityRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<RoomType?> CreateRoomTypeAsync(
        Guid propertyId,
        RoomType roomType,
        string currencyCode,
        List<string> services,
        List<string> amenities
    )
    {
        Property? property = await _propertyRepository.GetPropertyByIdAsync( propertyId );
        if ( property == null )
        {
            return null;
        }

        Currency? currency = await _currencyRepository.GetCurrencyByTypeAsync( currencyCode );
        if ( currency == null )
        {
            return null;
        }

        roomType.CurrencyId = currency.Id;

        foreach ( string serviceName in services.Distinct() )
        {
            Service? service = await _serviceRepository.GetServiceByNameAsync( serviceName );
            if ( service == null )
            {
                service = new Service
                {
                    Name = serviceName
                };
                await _serviceRepository.CreateServiceAsync( service );
            }

            roomType.RoomTypeServices.Add( new Domain.Entities.RoomTypeService
            {
                Service = service
            } );
        }

        foreach ( string amenityName in amenities.Distinct() )
        {
            Amenity? amenity = await _amenityRepository.GetAmenityByNameAsync( amenityName );
            if ( amenity == null )
            {
                amenity = new Amenity
                {
                    Name = amenityName
                };
                await _amenityRepository.CreateAmenityAsync( amenity );
            }

            roomType.RoomTypeAmenities.Add( new RoomTypeAmenity
            {
                Amenity = amenity
            } );
        }

        roomType.Id = Guid.NewGuid();
        roomType.PropertyId = propertyId;

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
        string currencyCode,
        List<string> services,
        List<string> amenities
    )
    {
        RoomType? roomType = await _roomTypeRepository.GetRoomTypeByIdAsync(updatedRoomType.Id);
        if (roomType == null)
            return null;
        
        Currency? currency = await _currencyRepository.GetCurrencyByTypeAsync(currencyCode);
        if (currency == null)
            return null;
        
        updatedRoomType.CurrencyId = currency.Id;
        
        await _roomTypeRepository.UpdateRoomTypeAsync(
            updatedRoomType,
            services,
            amenities
        );
        
        await _unitOfWork.CommitAsync();
        
        return await _roomTypeRepository.GetRoomTypeByIdAsync(updatedRoomType.Id);
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
}