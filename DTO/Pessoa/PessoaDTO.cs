using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static m01_labMedicine.Core.Validation.CustomValidation;

namespace m01_labMedicine.DTO.Pessoa
{
    public abstract class PessoaDTO
    {
        [Required]
        [StringLength(maximumLength: 100)]
        public string Nome { get; set; }

        [StringLength(maximumLength: 30)]
        public string Genero { get; set; }

        [Required]
        public DateTime DataNascimento { get; set; }
        [Required]
        [CheckCPF]
        public string CPF { get; set; }

        [AllowNull]
        [CheckTelefone]
        public string Telefone { get; set; }
    }
}