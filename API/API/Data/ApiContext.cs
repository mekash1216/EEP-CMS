using API.Data;
using API.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace API.Data
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) : base(options) { }

        public DbSet<ICD10Codes2024> ICD10Codes2024 { get; set; }
        public DbSet<Assignment> Assignments { get; set; }

        public DbSet<Examiner> Examiners { get; set; }
        public DbSet<RegExaminer> RegExaminers { get; set; }

        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<PrescriptionItem> PrescriptionItems { get; set; }

        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Referral> Referrals { get; set; }
        public DbSet<PhysicalExamination> PhysicalExaminations { get; set; }
        public DbSet<PhysicalAssessment> PhysicalAssessments { get; set; }
        public DbSet<LaboratoryTestResult> LaboratoryTestResults { get; set; }
        public DbSet<LaboratoryTestResult> SubTests { get; set; }
        public DbSet<LaboratoryRequest> LaboratoryRequests { get; set; }
        public DbSet<Parasitology> Parasitologies { get; set; }
        public DbSet<Urinalysis> Urinalyses { get; set; }
        public DbSet<Hematology> Hematologies { get; set; }
        public DbSet<Biochemistry> Biochemistries { get; set; }
        public DbSet<Bacteriology> Bacteriologies { get; set; }
        public DbSet<Serology> Serologies { get; set; }
        public DbSet<OtherLab> OtherLabs { get; set; }
        public DbSet<Hormone> Hormones { get; set; }
        public DbSet<Coagulation> Coagulations { get; set; }
        public DbSet<CardiacMarker> CardiacMarkers { get; set; }
        public DbSet<CancerMarker> CancerMarkers { get; set; }
        public DbSet<Electrolyte> Electrolytes { get; set; }
        public DbSet<LabBloodFilmTest> BloodFilmTests { get; set; }
        public DbSet<UrineDipsticksTest> UrineDipsticksTests { get; set; }
        public DbSet<SickLeave> SickLeaves { get; set; }

        public DbSet<ClinicRequest> ClinicRequests { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<AccidentReport> AccidentReports { get; set; }
        public DbSet<Witness> Witnesses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ICD10Codes2024>(entity =>
            {
           
                entity.ToTable("ICD10Codes2024");
     
                entity.HasNoKey();  
                entity.Property(e => e.CODE).HasColumnName("CODE"); 
                entity.Property(e => e.ShortDescription).HasColumnName("SHORT DESCRIPTION");
               
            });

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Examiner>().ToTable("Examiners");
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Assignment>()
                .HasOne(a => a.Patient)
                .WithMany(p => p.Assignments)
                .HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<AccidentReport>()
        .HasMany(ar => ar.Witnesses)
        .WithOne(w => w.AccidentReport)
        .HasForeignKey(w => w.AccidentReportId);

            modelBuilder.Entity<SickLeave>()
          .HasOne(s => s.Patient)
          .WithMany(p => p.SickLeaves)
          .HasForeignKey(s => s.PatientId);



            modelBuilder.Entity<PrescriptionItem>()
                      .HasOne(pi => pi.Prescription)
                      .WithMany(p => p.PrescriptionItems)
                      .HasForeignKey(pi => pi.PrescriptionId);

            modelBuilder.Entity<PrescriptionItem>()
                .HasOne(pi => pi.Stock)
                .WithMany()
                .HasForeignKey(pi => pi.StockId);


            modelBuilder.Entity<LaboratoryRequest>()
          .HasOne(l => l.Parasitology)
          .WithMany() // Update if Parasitology has a collection of LaboratoryRequests
          .HasForeignKey(l => l.ParasitologyId)
          .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<LaboratoryRequest>()
                .HasOne(l => l.Hematology)
                .WithMany()
                .HasForeignKey(l => l.HematologyId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<LaboratoryRequest>()
                .HasOne(l => l.Biochemistry)
                .WithMany()
                .HasForeignKey(l => l.BiochemistryId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<LaboratoryRequest>()
                .HasOne(l => l.Bacteriology)
                .WithMany()
                .HasForeignKey(l => l.BacteriologyId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<LaboratoryRequest>()
                .HasOne(l => l.Serology)
                .WithMany()
                .HasForeignKey(l => l.SerologyId)
                .OnDelete(DeleteBehavior.SetNull);


            modelBuilder.Entity<LaboratoryTestResult>()
        .Property(t => t.Id)
        .ValueGeneratedOnAdd();
        }
    }
}

