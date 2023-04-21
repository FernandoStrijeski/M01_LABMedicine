using System.ComponentModel.DataAnnotations;
using static m01_labMedicine.Core.Validation.CustomValidation;

namespace m01_labMedicine.DTO.Pessoa.Paciente
{
    public class PacienteRequestDTO : PessoaDTO
    {
        public string ContatoEmergencia { get; set; }
        public List<string> Alergias { get; set; }
        public List<string> CuidadosEspecificos { get; set; }
        public string Convenio { get; set; }
        [Required]
        [CheckStatusAtendimento(AllowStatus = "AGUARDANDO_ATENDIMENTO,EM_ATENDIMENTO,ATENDIDO,NAO_ATENDIDO")]
        public string StatusAtendimento { get; set; }
    }
}