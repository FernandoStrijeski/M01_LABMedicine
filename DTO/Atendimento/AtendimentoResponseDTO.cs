using m01_labMedicine.DTO.Pessoa.Medico;
using m01_labMedicine.DTO.Pessoa.Paciente;
using Microsoft.Build.Framework;

namespace m01_labMedicine.DTO.Atendimento
{
    public class AtendimentoResponseDTO
    {
        [Required]
        public MedicoResponseDTO Medico { get; set; }

        [Required]
        public PacienteResponseDTO Paciente { get; set; }
    }
}
