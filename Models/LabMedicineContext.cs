//using m01_labMedicine.Validation;
using m01_labMedicine.Validation;
using Microsoft.EntityFrameworkCore;

namespace m01_labMedicine.Model
{
    public class LabMedicineContext : DbContext
    {
        public LabMedicineContext(DbContextOptions<LabMedicineContext> options) : base(options)
        {             
            //Todo: Configurar migration
            //ToDo: Executar os comandos do migration
            //ToDo: Inserir, Atualizar, Deletar e Selecionar
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            
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

        }
        public DbSet<PacienteModel> Paciente { get; set; }
        public DbSet<MedicoModel> Medico { get; set; }
        public DbSet<EnfermeiroModel> Enfermeiro { get; set; }
        public DbSet<AtendimentoMedicoPacienteModel> AtendimentoMedicoPaciente { get; set; }
    }
}