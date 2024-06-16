using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data;

public class DatabaseContext : DbContext
{
    protected DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions options) : base(options)
    {
    }
    
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<Medicament> Medicaments { get; set; }
    public DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Patient>().HasData(new List<Patient>
        {
            new Patient
            {
                Id = 1,
                FirstName = "Jan",
                LastName = "Kowalski",
                Birthdate = DateTime.Parse("2002-01-01")
            },
            new Patient
            {
                Id = 2,
                FirstName = "Jan",
                LastName = "Nowak",
                Birthdate = DateTime.Parse("2000-02-13")
            },
            new Patient
            {
                Id = 3,
                FirstName = "Halina",
                LastName = "Kowalska",
                Birthdate = DateTime.Parse("2004-12-01")
            },
            new Patient
            {
                Id = 4,
                FirstName = "Heniek",
                LastName = "Paczula",
                Birthdate = DateTime.Parse("2010-09-30")
            }
        });

        modelBuilder.Entity<Doctor>().HasData(new List<Doctor>
        {
            new Doctor
            {
                Id = 1,
                FirstName = "Hannibal",
                LastName = "Lecter",
                Email = "fbi@fbi.com"
            },
            new Doctor
            {
                Id = 2,
                FirstName = "Marianna",
                LastName = "Leczydlo",
                Email = "nfz@nfz.com"
            },
            new Doctor
            {
                Id = 3,
                FirstName = "Dorany",
                LastName = "Przyloz",
                Email = "lekarski@mniszek.pl"
            }
        });

        modelBuilder.Entity<Medicament>().HasData(new List<Medicament>
        {
            new Medicament
            {
                Id = 1,
                Name = "Pilotex",
                Description = "Better vision",
                Type = "A"
            },
            new Medicament
            {
                Id = 2,
                Name = "Hepaslimin",
                Description = "Losing weight",
                Type = "B"
            },
            new Medicament
            {
                Id = 3,
                Name = "Nurofen",
                Description = "Painkiller",
                Type = "A"
            }
        });

        modelBuilder.Entity<Prescription>().HasData(new List<Prescription>
        {
            new Prescription
            { 
                Id = 1,
                IdDoctor = 1,
                IdPatient = 1,
                Date = DateTime.Parse("2024-03-02"),
                DueDate = DateTime.Parse("2024-04-10")
            },
            new Prescription
            { 
                Id = 2,
                IdDoctor = 2,
                IdPatient = 1,
                Date = DateTime.Parse("2024-01-01"),
                DueDate = DateTime.Parse("2024-05-12")
            },
            new Prescription
            { 
                Id = 3,
                IdDoctor = 2,
                IdPatient = 2,
                Date = DateTime.Parse("2024-02-01"),
                DueDate = DateTime.Parse("2024-03-10")
            }
            
        });
        modelBuilder.Entity<PrescriptionMedicament>().HasData(new List<PrescriptionMedicament>
        {
            new PrescriptionMedicament
            {
                IdMedicament = 1,
                IdPrescription = 1,
                Details = "lorem ipsum...",
                Dose = null
            },
            new PrescriptionMedicament
            {
                IdMedicament = 2,
                IdPrescription = 1,
                Details = "lorem ipsum...",
                Dose = 1
            },
            new PrescriptionMedicament
            {
                IdMedicament = 3,
                IdPrescription = 1,
                Details = "lorem ipsum...",
                Dose = 3
            },
            new PrescriptionMedicament
            {
                IdMedicament = 1,
                IdPrescription = 2,
                Details = "lorem ipsum...",
                Dose = 4
            },
            new PrescriptionMedicament
            {
                IdMedicament = 2,
                IdPrescription = 3,
                Details = "lorem ipsum...",
                Dose = 2
            },
            new PrescriptionMedicament
            {
                IdMedicament = 3,
                IdPrescription = 2,
                Details = "lorem ipsum...",
                Dose = null
            }
            
        });

    }
    
}