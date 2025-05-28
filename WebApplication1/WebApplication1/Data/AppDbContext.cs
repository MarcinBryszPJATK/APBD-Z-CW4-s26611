using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data;

public class AppDbContext : DbContext
{
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Medicament> Medicaments { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; set; }
    
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        var patient = new Patient
        {
            IdPatient = 1,
            FirstName = "John",
            LastName = "Doe",
            Birthdate =  new DateTime(2025, 05, 28, 14, 30, 0)
                
        };

        var doctor = new Doctor
        {
            IdDoctor = 1,
            FirstName = "Jane",
            LastName = "Doe",
            Email = "test@test.com",
        };

        var medicament = new Medicament
        {
            IdMedicament = 1,
            Name = "Lek 1",
            Description = "Lek 1 opis",
            Type = "Bol"
        };

        var presciption = new Prescription
        {
            IdPersciption = 1,
            Date = new DateTime(2025, 05, 28, 14, 30, 0),
            DueDate = new DateTime(2025, 06, 28, 14, 30, 0),
            IdPatient = 1,
            IdDoctor = 1,
        };

        var presciptionMedicament = new PrescriptionMedicament
        {
            IdMedicament = 1,
            IdPrescription = 1,
            Dose = 5,
            Details = "Details "
        };
        
        modelBuilder.Entity<Patient>().HasData(patient);
        modelBuilder.Entity<Doctor>().HasData(doctor);
        modelBuilder.Entity<Medicament>().HasData(medicament);
        modelBuilder.Entity<Prescription>().HasData(presciption);
        modelBuilder.Entity<PrescriptionMedicament>().HasData(presciptionMedicament);
    }
}