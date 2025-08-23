using AutoMapper;
using Domain.Entities;
using WebAPI.DTOs.PropertyDTOs;

namespace WebAPI.Mapper;

public class PropertyProfile : Profile
{
    public PropertyProfile()
    {
        CreateMap<Property, CreatedPropertyDto>();
        CreateMap<ReadPropertyDto, Property>();
    }
}