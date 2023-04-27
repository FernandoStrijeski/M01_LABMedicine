using m01_labMedicine.Models.Enum;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using static m01_labMedicine.Core.Validation.CustomValidation;

namespace m01_labMedicine.DTO.Pessoa
{
    public abstract class PessoaDTO
    {
        [Required(ErrorMessage = "O Campo Nome é obrigatório")]
        [StringLength(maximumLength: 100)]
        public string Nome { get; set; }

        [JsonConverter(typeof(GeneroConverter))]
        public GeneroEnum Genero { get; set; }

        [Required(ErrorMessage = "O Campo Data de nascimento é obrigatório")]
        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }
        [CheckCPF]
        public string CPF { get; set; }

        [AllowNull]
        [CheckTelefone]
        public string Telefone { get; set; }
    }
}