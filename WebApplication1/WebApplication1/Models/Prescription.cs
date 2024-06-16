using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models;

public class Prescription
{
    [Key]
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public int IdPatient { get; set; }
    public int IdDoctor { get; set; }
    
    public ICollection<PrescriptionMedicament> PrescriptionMedicaments = new HashSet<PrescriptionMedicament>();
    
    [ForeignKey(nameof(IdDoctor))]
    public Doctor Doctor { get; set; } = null;
    [ForeignKey(nameof(IdPatient))]
    public Patient Patient { get; set; } = null;

}