using m01_labMedicine.Models.Enum;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using static m01_labMedicine.Core.Validation.CustomValidation;

namespace m01_labMedicine.DTO.Pessoa.Paciente
{
    public class PacienteStatusRequestDTO
    {        
        [JsonConverter(typeof(StatusAtendimentoConverter))]
        public StatusAtendimentoEnum? StatusAtendimento { get; set; }
    }
}