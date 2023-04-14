using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace m01_labMedicine.Model
{
    [Table("Paciente")]
    public class PacienteModel : PessoaModel
    {        
        [NotNull]
        [MaxLength(250)]
        public string ContatoEmergencia { get; set; }

        [AllowNull]
        public List<string> Alergias { get; set; }

        [AllowNull]        
        public List<string> CuidadosEspecificos { get; set; }

        [AllowNull]
        public string Convenio{ get; set; }

        [NotNull]
        public string StatusAtendimento { get; set; }
    }
}
