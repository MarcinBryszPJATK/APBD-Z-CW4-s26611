namespace WebApplication1.DTOs;

public class PrescriptionCreateDto
{
    public int IdDoctor { get; set; }
    public PatientDto Patient { get; set; } = null!;
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    
    public List<MedicamentDto> Medicaments { get; set; } = new();
}