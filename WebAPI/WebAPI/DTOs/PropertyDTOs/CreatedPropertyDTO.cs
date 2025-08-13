using System.ComponentModel.DataAnnotations;

namespace WebAPI.DTOs.PropertyDTOs;

public class CreatedPropertyDto
{
    [Required]
    public Guid Id { get; set; }
    
    [Required]
    [MaxLength(50)]
    public required string Name { get; set; }
    
    [Required]
    [MaxLength(50)]
    public required string Country { get; set; }
    
    [Required]
    [MaxLength(50)]
    public required string City { get; set; }
    
    [Required]
    [MaxLength(50)]
    public required string Address { get; set; }
    
    [Required]
    [Range(-90, 90)]
    public decimal Latitude { get; set; }
    
    [Required]
    [Range(-180, 180)]
    public decimal Longitude { get; set; }
}