using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace m01_labMedicine.Model
{
    [Table("Atendimento_Medico_Paciente")]
    public class AtendimentoMedicoPacienteModel
    {
        [Key]
        [Column("Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("MedicoModel")]
        public int MedicoId { get; set; }
        public MedicoModel Medico { get; set; } = new MedicoModel();

        [ForeignKey("PacienteModel")]
        public int PacienteId { get; set; }
        public PacienteModel Paciente { get; set; } = new PacienteModel();
        
        [AllowNull]
        public string DescricaoAtendimento { get; set; }
    }
}
