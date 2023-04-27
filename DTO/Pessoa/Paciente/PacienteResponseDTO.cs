using m01_labMedicine.Models.Enum;
using System.Text.Json.Serialization;

namespace m01_labMedicine.DTO.Pessoa.Paciente
{
    public class PacienteResponseDTO : PessoaDTO
    {
        public string ContatoEmergencia { get; set; }
        public List<string> Alergias { get; set; }
        public List<string> CuidadosEspecificos { get; set; }
        public string Convenio { get; set; }
        public StatusAtendimentoEnum StatusAtendimento { get; set; }

        [JsonPropertyName("identificador")]
        public int Codigo { get; set; }
        public int Atendimentos { get; set; }
    }
}