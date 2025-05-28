using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs;

public class MedicamentDto
{
    public int IdMedicament { get; set; }

    [MaxLength(100)]
    public string Details { get; set; } = null!;
}