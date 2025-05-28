namespace WebApplication1.DTOs;

public class PrescriptionDto
{
    public int IdPrescription { get; set; }
    public DateTime Date      { get; set; }
    public DateTime DueDate   { get; set; }

    public DoctorDto Doctor { get; set; } = null!;

    public List<PrescriptionMedicamentDto> Medicaments { get; set; } = new();
}