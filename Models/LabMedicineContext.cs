using m01_labMedicine.Core.Extension;
using m01_labMedicine.Core.Validation;
using Microsoft.EntityFrameworkCore;

namespace m01_labMedicine.Model
{
    public class LabMedicineContext : DbContext
    {
        public LabMedicineContext(DbContextOptions<LabMedicineContext> options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PacienteModel>()
            .HasIndex(p => p.CPF)
            .IsUnique()
            .Metadata.IsUnique = true;

            modelBuilder.Entity<MedicoModel>()
            .HasIndex(p => p.CPF)
            .IsUnique()
            .Metadata.IsUnique = true;

            modelBuilder.Entity<EnfermeiroModel>()
            .HasIndex(p => p.CPF)
            .IsUnique();

            modelBuilder.Entity<PacienteModel>()                                
                .Property(x => x.Alergias)
                .HasConversion(c => string.Join(',', c),
                               c => c.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList())
                .Metadata.SetValueComparer(new ListValueComparer());

            modelBuilder.Entity<PacienteModel>()
                .Property(x => x.CuidadosEspecificos)
                .HasConversion(c => string.Join(',', c),
                               c => c.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList())
                .Metadata.SetValueComparer(new ListValueComparer());

            modelBuilder.Entity<AtendimentoMedicoModel>()
                .HasOne(p => p.Paciente)
                .WithMany()
                .HasForeignKey(p => p.PacienteId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<AtendimentoMedicoModel>()
               .HasOne(m => m.Medico)
               .WithMany()
               .HasForeignKey(m => m.MedicoId)
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.CargaInicial();
        }
        public DbSet<PacienteModel> Paciente { get; set; }
        public DbSet<MedicoModel> Medico { get; set; }
        public DbSet<EnfermeiroModel> Enfermeiro { get; set; }
        public DbSet<AtendimentoMedicoModel> AtendimentoMedico { get; set; }
    }
}