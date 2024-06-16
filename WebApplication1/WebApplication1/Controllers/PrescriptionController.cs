using System.Transactions;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTOs;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PrescriptionController : ControllerBase
{
    private readonly IDbService _dbService;
    public PrescriptionController(IDbService dbService)
    {
        _dbService = dbService;
    }
    
    [HttpPost("{prescriptionId}")]
    public async Task<IActionResult> AddPrescription(int prescriptionId, NewPrescriptionDTO newPrescription)
    {
        foreach (var med in newPrescription.Medications)
        {
            if (!await _dbService.DoesMedicationExist(med.MedicationId))
            {
                return NotFound($"Medication with given id - {med.MedicationId} does not exist");
            }
        }

        if (!await _dbService.IsMaxTenMeds(newPrescription))
        {
            return BadRequest($"To many medications - {newPrescription.Medications.Count}");
        }

        if (!await _dbService.IsDueDateCorrect(newPrescription))
        {
            return BadRequest("DueDate is before Date of prescription");
        }
        
        Prescription prescription;
        var medications = new List<PrescriptionMedicament>();
        using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            if (!await _dbService.DoesPatientExist(newPrescription.Patient))
            {
                await _dbService.AddPatient(newPrescription.Patient);
            }
            var patient = _dbService.GetPatientByNameSurnameDob(newPrescription.Patient);

            var doctor = _dbService.GetDoctorById(newPrescription.IdDoctor);

            prescription = new Prescription
            {
                Date = newPrescription.Date,
                DueDate = newPrescription.DueDate,
                IdPatient = patient.Id,
                IdDoctor = doctor.Id
            };
            await _dbService.AddPrescription(prescription);
            foreach (var medicament in newPrescription.Medications)
            {
                medications.Add(new PrescriptionMedicament
                    {
                        IdMedicament = medicament.MedicationId,
                        IdPrescription = prescription.Id,
                        Dose = medicament.Dose,
                        Details = medicament.Details
                    });
            }

            await _dbService.AddPrescriptionMedicament(medications);
            scope.Complete();
        }
        return Created("api/prescriptrions", new Prescription
        {
            Id = prescription.Id,
            IdPatient = prescription.IdPatient,
            IdDoctor = prescription.IdDoctor,
            Date = prescription.Date,
            DueDate = prescription.DueDate,
            PrescriptionMedicaments = medications
        });
    }

}