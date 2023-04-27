using m01_labMedicine.Models.Enum;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using static m01_labMedicine.Core.Validation.CustomValidation;

namespace m01_labMedicine.DTO.Pessoa.Paciente
{
    public class PacienteRequestDTO : PessoaDTO
    {
        public string ContatoEmergencia { get; set; }
        public List<string> Alergias { get; set; }
        public List<string> CuidadosEspecificos { get; set; }
        public string Convenio { get; set; }

        [JsonConverter(typeof(StatusAtendimentoConverter))]
        public StatusAtendimentoEnum StatusAtendimento { get; set; }
    }
}