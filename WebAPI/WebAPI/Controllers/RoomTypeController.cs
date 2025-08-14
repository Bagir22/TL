using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Domain.Entities;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DTOs.RoomTypeDTOs;
using WebAPI.Exceptions;

namespace WebAPI.Controllers;

[ApiController]
[Route( "/api/roomtypes" )]
public class RoomTypeController : ControllerBase
{
    private readonly IRoomTypeService _roomTypeService;
    private readonly IMapper _mapper;

    public RoomTypeController( IRoomTypeService roomTypeService, IMapper mapper )
    {
        _roomTypeService = roomTypeService;
        _mapper = mapper;
    }

    [HttpPost( "properties/{propertyId:guid}/roomtypes" )]
    public async Task<IActionResult> CreateRoomType( Guid propertyId, [FromBody] ReadRoomTypeDto dto )
    {
        if ( !ModelState.IsValid )
        {
            IEnumerable<string> errors = ModelState.Values.SelectMany( v => v.Errors ).Select( e => e.ErrorMessage );
            
            throw new HttpResponseException( 400, string.Join( "; ", errors ) );
        }

        RoomType roomType = new()
        {
            Name = dto.Name,
            DailyPrice = dto.DailyPrice,
            MinPersonCount = dto.MinPersonCount,
            MaxPersonCount = dto.MaxPersonCount,
            RoomsCount = dto.RoomsCount
        };
        
        try
        {
            RoomType? createdRoomType = await _roomTypeService.CreateRoomTypeAsync( propertyId, roomType,
                dto.CurrencyType, dto.Services, dto.Amenities );
            CreatedRoomTypeDto? createdRoomTypeDto = _mapper.Map<CreatedRoomTypeDto>( createdRoomType );
            
            return Ok( createdRoomTypeDto );
        }
        catch ( ValidationException ex )
        {
            throw new HttpResponseException( 400, ex.Message );
        }
    }

    [HttpGet( "{id:guid}" )]
    public async Task<IActionResult> GetRoomTypeById( Guid id )
    {
        RoomType? roomType = await _roomTypeService.GetRoomTypeByIdAsync( id );
        if ( roomType == null )
        {
            throw new HttpResponseException( 404, $"Room type with id {id} not found" );
        }

        CreatedRoomTypeDto? dto = _mapper.Map<CreatedRoomTypeDto>( roomType );
        
        return Ok( dto );
    }

    [HttpGet]
    public async Task<IActionResult> GetAllRoomTypes()
    {
        IEnumerable<RoomType?> roomTypes = await _roomTypeService.GetAllRoomTypesAsync();
        IEnumerable<CreatedRoomTypeDto>? dtos = _mapper.Map<IEnumerable<CreatedRoomTypeDto>>( roomTypes );
        
        return Ok( dtos );
    }

    [HttpGet( "{propertyId}/roomtypes" )]
    public async Task<IActionResult> GetRoomTypesByPropertyId( Guid propertyId )
    {
        IEnumerable<RoomType?> roomTypes = await _roomTypeService.GetRoomTypesByPropertyIdAsync( propertyId );
        IEnumerable<CreatedRoomTypeDto>? dtos = _mapper.Map<IEnumerable<CreatedRoomTypeDto>>( roomTypes );
        
        return Ok( dtos );
    }

    [HttpPut( "{id:guid}" )]
    public async Task<IActionResult> UpdateRoomType( Guid id, [FromBody] ReadRoomTypeDto dto )
    {
        if ( !ModelState.IsValid )
        {
            IEnumerable<string> errors = ModelState.Values.SelectMany( v => v.Errors ).Select( e => e.ErrorMessage );
            
            throw new HttpResponseException( 400, string.Join( "; ", errors ) );
        }

        RoomType roomType = new()
        {
            Id = id,
            Name = dto.Name,
            DailyPrice = dto.DailyPrice,
            MinPersonCount = dto.MinPersonCount,
            MaxPersonCount = dto.MaxPersonCount,
            RoomsCount = dto.RoomsCount
        };
        
        try
        {
            RoomType? updatedRoomType =
                await _roomTypeService.UpdateRoomTypeAsync( roomType, dto.CurrencyType, dto.Services, dto.Amenities );
            CreatedRoomTypeDto? createdRoomTypeDto = _mapper.Map<CreatedRoomTypeDto>( updatedRoomType );

            return Ok( createdRoomTypeDto );
        }
        catch ( ValidationException ex )
        {
            throw new HttpResponseException( 400, ex.Message );
        }
    }

    [HttpDelete( "{id:guid}" )]
    public async Task<IActionResult> DeleteRoomType( Guid id )
    {
        bool deleted = await _roomTypeService.DeleteRoomTypeAsync( id );
        if ( !deleted )
        {
            throw new HttpResponseException( 404, $"Room type with id {id} not found" );
        }

        return Ok();
    }
}