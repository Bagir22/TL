using AutoMapper;
using Domain.Entities;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DTOs.PropertyDTOs;
using WebAPI.Exceptions;
using ValidationException = WebAPI.Exceptions.ValidationException;

namespace WebAPI.Controllers;

[ApiController]
[Route( "/api/properties" )]
public class PropertyController : ControllerBase
{
    private readonly IPropertyService _propertyService;
    private readonly IMapper _mapper;

    public PropertyController( IPropertyService propertyService, IMapper mapper )
    {
        _propertyService = propertyService;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> CreateProperty( [FromBody] ReadPropertyDto dto )
    {
        if ( !ModelState.IsValid )
        {
            IEnumerable<string> errors = ModelState.Values
                .SelectMany( me => me.Errors )
                .Select( e => e.ErrorMessage );

            throw new ValidationException( string.Join( "; ", errors ) );
        }

        Property? property = _mapper.Map<Property>( dto );

        Property? createdProperty = await _propertyService.CreatePropertyAsync( property );
        CreatedPropertyDto? createdDto = _mapper.Map<CreatedPropertyDto>( createdProperty );

        return CreatedAtAction( nameof( GetPropertyById ), new
        {
            id = createdDto.Id
        }, createdDto );
    }

    [HttpGet( "{id:guid}" )]
    public async Task<IActionResult> GetPropertyById( Guid id )
    {
        Property? property = await _propertyService.GetPropertyByIdAsync( id );
        if ( property == null )
        {
            throw new NotFoundException( $"Property with id {id} not found" );
        }

        CreatedPropertyDto dto = _mapper.Map<CreatedPropertyDto>( property );
        
        return Ok( dto );
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProperties()
    {
        IEnumerable<Property> properties = await _propertyService.GetAllPropertiesAsync();
        IEnumerable<CreatedPropertyDto>? dtos = _mapper.Map<IEnumerable<CreatedPropertyDto>>( properties );

        return Ok( dtos );
    }

    [HttpPut( "{id:guid}" )]
    public async Task<IActionResult> UpdateProperty( Guid id, [FromBody] ReadPropertyDto dto )
    {
        if ( !ModelState.IsValid )
        {
            IEnumerable<string> errors = ModelState.Values
                .SelectMany( me => me.Errors )
                .Select( e => e.ErrorMessage );
            throw new ValidationException( string.Join( "; ", errors ) );
        }

        Property updatedProperty = _mapper.Map<Property>( dto );

        Property? property = await _propertyService.UpdatePropertyAsync( id, updatedProperty );
        if ( property == null )
        {
            throw new NotFoundException( $"Property with id {id} not found" );
        }

        CreatedPropertyDto updatedDto = _mapper.Map<CreatedPropertyDto>( property );

        return Ok( updatedDto );
    }

    [HttpDelete( "{id:guid}" )]
    public async Task<IActionResult> DeleteProperty( Guid id )
    {
        bool deleted = await _propertyService.DeletePropertyAsync( id );
        if ( !deleted )
        {
            throw new NotFoundException( $"Property with id {id} not found" );
        }

        return Ok();
    }
}