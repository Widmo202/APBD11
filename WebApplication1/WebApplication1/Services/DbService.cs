using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.DTOs;
using WebApplication1.Models;

namespace WebApplication1.Services;

public class DbService : IDbService
{
    private readonly DatabaseContext _context;

    public DbService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<bool> DoesMedicationExist(int medicationId)
    {
        return await _context.Medicaments.AnyAsync(m => m.Id == medicationId);
    }

    public async Task<bool> DoesPatientExist(NewPrescriptionPatientDTO patient)
    {
        return await _context.Patients.AnyAsync(p =>
            p.FirstName.Equals(patient.FirstName) && p.LastName.Equals(patient.LastName) &&
            p.Birthdate.Equals(patient.BirthDate));
    }

    public async Task<bool> IsMaxTenMeds(NewPrescriptionDTO prescriptionDto)
    {
        return prescriptionDto.Medications.Count <= 10;
    }

    public async Task<bool> IsDueDateCorrect(NewPrescriptionDTO prescriptionDto)
    {
        return prescriptionDto.DueDate >= prescriptionDto.Date;
    }

    public async Task<Patient?> GetPatientByNameSurnameDob(NewPrescriptionPatientDTO patient)
    {
        return await _context.Patients.FirstOrDefaultAsync(p =>p.FirstName.Equals(patient.FirstName) && p.LastName.Equals(patient.LastName) && p.Birthdate.Equals(patient.BirthDate));
    }

    public async Task<Medicament?> GetMedicationById(int medicationId)
    {
        return await _context.Medicaments.FirstOrDefaultAsync(m => m.Id == medicationId);
    }

    public async Task AddPatient(NewPrescriptionPatientDTO patient)
    {
        await _context.AddRangeAsync(new Patient
        {
            FirstName = patient.FirstName,
            LastName = patient.LastName,
            Birthdate = patient.BirthDate
        });
        await _context.SaveChangesAsync();
    }

    public async Task<Doctor?> GetDoctorById(int doctorId)
    {
        return await _context.Doctors.FirstOrDefaultAsync(d => d.Id == doctorId);
    }

    public async Task AddPrescription(Prescription prescription)
    {
        await _context.AddRangeAsync(prescription);
        await _context.SaveChangesAsync();
    }

    public async Task AddPrescriptionMedicament(IEnumerable<PrescriptionMedicament> prescriptionMedicament)
    {
        await _context.AddRangeAsync(prescriptionMedicament);
        await _context.SaveChangesAsync();
    }
}