using System.Text.Json.Serialization;

namespace m01_labMedicine.DTO.Pessoa.Medico
{
    public class EnfermeiroResponseDTO : PessoaDTO
    {
        public string InstituicaoEnsino { get; set; }
        public string CofenUF { get; set; }

        [JsonPropertyName("identificador")]
        public int Codigo { get; set; }
        public int Atendimentos { get; set; }
    }
}