using m01_labMedicine.DTO.Pessoa.Medico;
using m01_labMedicine.DTO.Pessoa.Paciente;
using Microsoft.Build.Framework;

namespace m01_labMedicine.DTO.Atendimento
{
    public class AtendimentoMedicoResponseDTO
    {
        public MedicoResponseDTO Medico { get; set; }
        public PacienteResponseDTO Paciente { get; set; }
        public string DescricaoAtendimento { get; set; }
    }
}
