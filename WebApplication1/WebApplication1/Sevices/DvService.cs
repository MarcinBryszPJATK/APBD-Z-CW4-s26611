using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.DTOs;
using WebApplication1.Models;

namespace WebApplication1.Sevices;

public interface IDbService
{
    public Task<ICollection<PrescriptionCreateDto>> CreatePrescriptionAsync(PrescriptionCreateDto prescriptionDtop);
    Task<PatientDto> GetPatientDetailsAsync(int idPatient);
}
public class DbService(AppDbContext data) : IDbService
{
    public async Task<ICollection<PrescriptionCreateDto>> CreatePrescriptionAsync(PrescriptionCreateDto prescriptionDtop)
    {
            if (prescriptionDtop.DueDate < prescriptionDtop.Date)
                throw new ArgumentException("DueDate musi być większe lub równe Date.");
            if (prescriptionDtop.Medicaments.Count > 10)
                throw new ArgumentException("Recepta może zawierać maksymalnie 10 leków.");

            var doctor = await data.Doctors.FindAsync(prescriptionDtop.IdDoctor);
            if (doctor == null)
                throw new KeyNotFoundException($"Lekarz o Id={prescriptionDtop.IdDoctor} nie istnieje.");

            Patient patient = null!;
            if (prescriptionDtop.Patient.IdPatient > 0)
                patient = await data.Patients.FindAsync(prescriptionDtop.Patient.IdPatient);

            if (patient == null)
            {
                patient = new Patient
                {
                    FirstName = prescriptionDtop.Patient.FirstName,
                    LastName  = prescriptionDtop.Patient.LastName,
                    Birthdate = prescriptionDtop.Patient.Birthdate
                };
                data.Patients.Add(patient);
            }

            var medIds = prescriptionDtop.Medicaments.Select(m => m.IdMedicament).ToList();
            var meds   = await data.Medicaments
                                 .Where(m => medIds.Contains(m.IdMedicament))
                                 .ToListAsync();
            if (meds.Count != medIds.Count)
                throw new KeyNotFoundException("Jeden lub więcej leków nie istnieje.");

            var prescription = new Prescription()
            {
                Date      = prescriptionDtop.Date,
                DueDate   = prescriptionDtop.DueDate,
                Doctor    = doctor,
                Patient   = patient,
                PrescriptionMedicaments = prescriptionDtop.Medicaments
                    .Select(mdto => new PrescriptionMedicament
                    {
                        IdMedicament = mdto.IdMedicament,
                        Details      = mdto.Details
                    })
                    .ToList()
            };

            data.Prescriptions.Add(prescription);
            await data.SaveChangesAsync();

            var result = await data.Prescriptions
                .Include(p => p.Patient)
                .Include(p => p.PrescriptionMedicaments)
                    .ThenInclude(pm => pm.Medicament)
                .Select(p => new PrescriptionCreateDto
                {
                    IdDoctor    = p.IdDoctor,
                    Patient     = new PatientDto
                    {
                        IdPatient = p.Patient.IdPatient,
                        FirstName = p.Patient.FirstName,
                        LastName  = p.Patient.LastName,
                        Birthdate = p.Patient.Birthdate
                    },
                    Date        = p.Date,
                    DueDate     = p.DueDate,
                    Medicaments = p.PrescriptionMedicaments
                        .Select(pm => new MedicamentDto
                        {
                            IdMedicament = pm.IdMedicament,
                            Details      = pm.Details
                        })
                        .ToList()
                })
                .ToListAsync();

            return result;
    }
    
    public async Task<PatientDto> GetPatientDetailsAsync(int idPatient)
    {
        var patient = await data.Patients
            .Include(p => p.Prescriptions!)
            .ThenInclude(r => r.Doctor)
            .Include(p => p.Prescriptions!)
            .ThenInclude(r => r.PrescriptionMedicaments!)
            .ThenInclude(pm => pm.Medicament)
            .FirstOrDefaultAsync(p => p.IdPatient == idPatient);

        if (patient == null)
            throw new KeyNotFoundException($"Pacjent o Id={idPatient} nie istnieje.");

        var dto = new PatientDto()
        {
            IdPatient = patient.IdPatient,
            FirstName = patient.FirstName,
            LastName  = patient.LastName,
            Birthdate = patient.Birthdate,
            Prescriptions = patient.Prescriptions!
                .OrderBy(r => r.DueDate)
                .Select(r => new PrescriptionDto
                {
                    IdPrescription = r.IdPersciption,
                    Date            = r.Date,
                    DueDate         = r.DueDate,
                    Doctor = new DoctorDto
                    {
                        IdDoctor  = r.Doctor.IdDoctor,
                        FirstName = r.Doctor.FirstName,
                        LastName  = r.Doctor.LastName,
                        Email     = r.Doctor.Email
                    },
                    Medicaments = r.PrescriptionMedicaments!
                        .Select(pm => new PrescriptionMedicamentDto
                        {
                            IdMedicament = pm.IdMedicament,
                            Name         = pm.Medicament.Name,
                            Dose         = pm.Dose,
                            Description  = pm.Details
                        })
                        .ToList()
                })
                .ToList()
        };

        return dto;
    }
}