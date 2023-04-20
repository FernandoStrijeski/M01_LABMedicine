using Microsoft.Build.Framework;

namespace m01_labMedicine.DTO.Atendimento
{
    public class AtendimentoMedicoRequestDTO
    {
        [Required]
        public int IdMedico { get; set; }

        [Required]
        public int IdPaciente { get; set; }

        public string DescricaoAtendimento { get; set; }
    }
}
