using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs;

public class PatientDto
{
    public int IdPatient  { get; set; }
    [MaxLength(100)]
    public string FirstName { get; set; } = null!;
    [MaxLength(100)]
    public string LastName { get; set; } = null!;
    
    public DateTime Birthdate { get; set; }
    
    public ICollection<PrescriptionDto> Prescriptions { get; set; } = null!;
}