using WebApplication1.DTOs;
using WebApplication1.Models;

namespace WebApplication1.Services;

public interface IDbService
{
    Task<bool> DoesMedicationExist(int medicationId);
    Task<bool> DoesPatientExist(NewPrescriptionPatientDTO patient);
    Task<bool> IsMaxTenMeds(NewPrescriptionDTO prescriptionDto);
    Task<bool> IsDueDateCorrect(NewPrescriptionDTO prescriptionDto);
    Task<Patient?> GetPatientByNameSurnameDob(NewPrescriptionPatientDTO patient);
    Task AddPatient(NewPrescriptionPatientDTO patient);

    Task<Medicament?> GetMedicationById(int medicationId);
    Task<Doctor?> GetDoctorById(int doctorId);

    Task AddPrescription(Prescription prescription);
    Task AddPrescriptionMedicament(IEnumerable<PrescriptionMedicament> prescriptionMedicament);
}