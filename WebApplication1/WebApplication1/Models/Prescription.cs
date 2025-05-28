using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models;

[Table("Perscription")]
[PrimaryKey(nameof(IdPersciption))]
public class Prescription
{
    public int IdPersciption { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    
    public int IdPatient  { get; set; }
    public int IdDoctor   { get; set; }
    
    [ForeignKey(nameof(IdPatient))]
    public virtual Patient Patient { get; set; } = null!;
    [ForeignKey(nameof(IdDoctor))]
    public virtual Doctor Doctor { get; set; } = null!;
    
    public virtual ICollection<PrescriptionMedicament> PrescriptionMedicaments { get; set; } = null!;
}