using m01_labMedicine.Models.Enum;
using System.Text.Json.Serialization;

namespace m01_labMedicine.DTO.Pessoa.Medico
{
    public class MedicoResponseDTO : PessoaDTO
    {
        public string InstituicaoEnsinoFormacao { get; set; }
        public string CrmUF { get; set; }
        public EspecializacaoClinicaEnum EspecializacaoClinica { get; set; }
        public SituacaoEnum EstadoSistema { get; set; }

        [JsonPropertyName("identificador")]
        public int Codigo { get; set; }
        public int Atendimentos { get; set; }
    }
}