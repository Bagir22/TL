using System.ComponentModel.DataAnnotations;

namespace WebAPI.DTOs.GuestDTOs;

public class GuestDTO
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
    
    [Required]
    [Phone]
    public string PhoneNumber { get; set; }
}