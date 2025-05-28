using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models;

[Table("Doctor")]
public class Doctor
{
    [Key]
    public int IdDoctor { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    
    public virtual ICollection<Prescription> Prescriptions { get; set; } = null!;
}