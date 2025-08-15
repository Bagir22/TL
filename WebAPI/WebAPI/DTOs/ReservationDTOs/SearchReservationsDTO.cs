using System.ComponentModel.DataAnnotations;

namespace WebAPI.DTOs;

public class SearchReservationsDTO
{
    [Required]
    [MaxLength(100)]
    public string City { get; set; }

    [Required]
    [RegularExpression(@"^\d{4}-\d{2}-\d{2}$")]
    public string ArrivalDate { get; set; }

    [Required]
    [RegularExpression(@"^\d{4}-\d{2}-\d{2}$")]
    public string DepartureDate { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int Guests { get; set; }
}