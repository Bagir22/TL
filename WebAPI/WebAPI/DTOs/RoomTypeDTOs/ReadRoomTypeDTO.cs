using System.ComponentModel.DataAnnotations;
using Domain.Entities;

namespace WebAPI.DTOs.RoomTypeDTOs;

public class ReadRoomTypeDto
{
    [Required]
    [MaxLength(50)]
    public string Name { get; set; }

    [Required]
    [Range(0.01, double.MaxValue)]
    public decimal DailyPrice { get; set; }

    [Required]
    [MaxLength(3)]
    public string CurrencyType { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int MinPersonCount { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int MaxPersonCount { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int RoomsCount { get; set; }

    public List<string> Services { get; set; } = new();
    public List<string> Amenities { get; set; } = new();
}