using AutoMapper;
using Domain.Entities;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DTOs;

namespace WebAPI.Controllers;

[ApiController]
[Route("api")]
public class RoomTypeController : ControllerBase
{
    private readonly IRoomTypeService _roomTypeService;
    private readonly IMapper _mapper;

    public RoomTypeController(IRoomTypeService roomTypeService, IMapper mapper)
    {
        _roomTypeService = roomTypeService;
        _mapper = mapper;
    }

    [HttpGet("properties/{propertyId:guid}/roomtypes")]
    public async Task<IActionResult> GetRoomTypesByProperty(Guid propertyId)
    {
        IEnumerable<RoomType> roomTypes = await _roomTypeService.GetRoomTypesByPropertyIdAsync(propertyId);
        
        IEnumerable<ReadPropertyDto>? dtos = _mapper.Map<IEnumerable<ReadPropertyDto>>(roomTypes);
        
        return Ok(dtos);
    }
    
    [HttpGet("roomtypes/")]
    public async Task<IActionResult> GetAllRoomTypes()
    {
        IEnumerable<RoomType> roomTypes = await _roomTypeService.GetAllRoomTypesAsync();
        
        IEnumerable<CreatedRoomTypeDto>? dtos = _mapper.Map<IEnumerable<CreatedRoomTypeDto>>(roomTypes);
        
        return Ok(dtos);
    }

    [HttpGet("roomtypes/{id:guid}")]
    public async Task<IActionResult> GetRoomTypeById(Guid id)
    {
        RoomType? roomType = await _roomTypeService.GetRoomTypeByIdAsync(id);
        if (roomType == null)
            return NotFound();

        CreatedRoomTypeDto? dto = _mapper.Map<CreatedRoomTypeDto>(roomType);
        
        return Ok(dto);
    }

    [HttpPost("properties/{propertyId:guid}/roomtypes")]
    public async Task<IActionResult> CreateRoomType(Guid propertyId, [FromBody] ReadRoomTypeDto dto)
    {
        RoomType? roomType = _mapper.Map<RoomType>(dto);
        
        RoomType createdRoomType = await _roomTypeService.CreateRoomTypeAsync(propertyId, roomType);

        CreatedRoomTypeDto? createdDto = _mapper.Map<CreatedRoomTypeDto>(createdRoomType);
        
        return CreatedAtAction(nameof(GetRoomTypeById), new { id = createdDto.Id }, createdDto);
    }

    [HttpPut("roomtypes/{id:guid}")]
    public async Task<IActionResult> UpdateRoomType(Guid id, [FromBody] ReadRoomTypeDto dto)
    {
        RoomType? roomType = _mapper.Map<RoomType>(dto);
        RoomType? updated = await _roomTypeService.UpdateRoomTypeAsync(id, roomType);

        if (updated == null)
            return NotFound();

        CreatedRoomTypeDto? updatedDto = _mapper.Map<CreatedRoomTypeDto>(updated);
        
        return Ok(updatedDto);
    }

    [HttpDelete("roomtypes/{id:guid}")]
    public async Task<IActionResult> DeleteRoomType(Guid id)
    {
        bool deleted = await _roomTypeService.DeleteRoomTypeAsync(id);
        if ( !deleted )
        {
            return NotFound();
        }

        return NoContent();
    }
}
