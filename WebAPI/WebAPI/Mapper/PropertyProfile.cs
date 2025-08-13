using AutoMapper;
using Domain.Entities;
using WebAPI.DTOs;
using WebAPI.DTOs.PropertyDTOs;

namespace WebAPI.Mapper;

public class PropertyProfile : Profile
{
    public PropertyProfile()
    {
        CreateMap<CreatedPropertyDto, Property>();
        CreateMap<Property, CreatedPropertyDto>();
        CreateMap<ReadPropertyDto, Property>();
        CreateMap<Property, ReadPropertyDto>();
    }
}