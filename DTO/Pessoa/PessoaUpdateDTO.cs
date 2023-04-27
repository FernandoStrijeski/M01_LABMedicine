using m01_labMedicine.Models.Enum;
using Microsoft.Build.Framework;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using static m01_labMedicine.Core.Validation.CustomValidation;

namespace m01_labMedicine.DTO.Pessoa
{
    public abstract class PessoaUpdateDTO
    {
        [Required]
        public string Nome { get; set; }

        [JsonConverter(typeof(GeneroConverter))]
        public GeneroEnum Genero { get; set; }
        public DateTime DataNascimento { get; set; }

        [AllowNull]
        [CheckTelefone]
        public string Telefone { get; set; }
    }
}