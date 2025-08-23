using System.ComponentModel.DataAnnotations;

namespace WebAPI.DTOs.PropertyDTOs;

public class ReadPropertyDto
{
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
    [Range(-89.99, 89.99)]
    public decimal Latitude { get; set; }
    
    [Required]
    [Range(-179.99, 179.99)]
    public decimal Longitude { get; set; }
}