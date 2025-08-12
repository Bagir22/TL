using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using Domain.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DTOs;

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
    public async Task<IActionResult> CreateProperty([FromBody] ReadPropertyDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        Property? property = _mapper.Map<Property>(dto);
        
        Property createdProperty = await _propertyService.CreatePropertyAsync(property);

        CreatedPropertyDto? createdDto = _mapper.Map<CreatedPropertyDto>(createdProperty);
        
        return CreatedAtAction(nameof(GetPropertyById), new { id = createdDto.Id }, createdDto);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetPropertyById(Guid id)
    {
        Property? property = await _propertyService.GetPropertyByIdAsync(id);
        if ( property == null )
        {
            return NotFound();
        }
        
        CreatedPropertyDto? dto = _mapper.Map<CreatedPropertyDto>(property);
        
        return Ok(dto);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllProperties()
    {
        IEnumerable<Property> properties = await _propertyService.GetAllPropertiesAsync();
        IEnumerable<CreatedPropertyDto>? dtos = _mapper.Map<IEnumerable<CreatedPropertyDto>>(properties);
        
        return Ok(dtos);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateProperty(Guid id, [FromBody] ReadPropertyDto dto)
    {
        Property? property = _mapper.Map<Property>(dto);
        Property? updated = await _propertyService.UpdatePropertyAsync(id, property);

        if ( updated == null )
        {
            return NotFound();
        }
        
        CreatedPropertyDto? updatedDto = _mapper.Map<CreatedPropertyDto>(updated);
        
        return Ok(updatedDto);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteProperty(Guid id)
    {
        bool deleted = await _propertyService.DeletePropertyAsync(id);
        if ( !deleted )
        {
            return NotFound();
        }
        
        return NoContent();
    }
}