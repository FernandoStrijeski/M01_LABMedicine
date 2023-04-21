using System.Text.Json.Serialization;

namespace m01_labMedicine.DTO.Pessoa.Medico
{
    public class MedicoResponseDTO : PessoaDTO
    {
        public string InstituicaoEnsinoFormacao { get; set; }
        public string CrmUF { get; set; }
        public string EspecializacaoClinica { get; set; }
        public string EstadoSistema { get; set; }

        [JsonPropertyName("identificador")]
        public int Codigo { get; set; }
        public int Atendimentos { get; set; }
    }
}