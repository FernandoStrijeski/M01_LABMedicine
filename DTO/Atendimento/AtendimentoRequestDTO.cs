using Microsoft.Build.Framework;

namespace m01_labMedicine.DTO.Atendimento
{
    public class AtendimentoRequestDTO
    {
        [Required]
        public int IdMedico { get; set; }

        [Required]
        public int IdPaciente { get; set; }
    }
}
