using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace m01_labMedicine.DTO.Paciente
{
    public class PacienteResponseDTO : PessoaDTO
    {
        public string ContatoEmergencia { get; set; }
        public List<string> Alergias { get; set; }
        public List<string> CuidadosEspecificos { get; set; }
        public string Convenio { get; set; }
        public string StatusAtendimento { get; set; }

        [JsonPropertyName("identificador")]
        public int Codigo { get; set; }
        public int atendimentos { get; set; }
    }
}