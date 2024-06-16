using System.ComponentModel.DataAnnotations;
using WebApplication1.Models;

namespace WebApplication1.DTOs;

public class NewPrescriptionDTO
{
    [Required]
    public DateTime Date { get; set; }
    [Required]
    public DateTime DueDate { get; set; }
    [Required]
    public NewPrescriptionPatientDTO Patient { get; set; }
    [Required]
    public List<NewPrescriptionMedicamentDTO> Medications { get; set; } = new List<NewPrescriptionMedicamentDTO>();
    [Required]
    public int IdDoctor { get; set; }
}

public class NewPrescriptionMedicamentDTO
{
    [Required]
    public int MedicationId { get; set; }
    [Required]
    [MaxLength(100)]
    public string Details { set; get; }

    public int? Dose { get; set; } = null;

}
public class NewPrescriptionPatientDTO
{
    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; }
    [Required]
    [MaxLength(100)]
    public string LastName { get; set; }
    [Required]
    public DateTime BirthDate { get; set; }
}