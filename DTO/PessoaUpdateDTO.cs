using Microsoft.Build.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static m01_labMedicine.Validation.CustomValidation;

namespace m01_labMedicine.DTO
{
    public abstract class PessoaUpdateDTO
    {
        [Required]
        public string Nome { get; set; }
        public string Genero { get; set; }

        [Required]
        public DateTime DataNascimento { get; set; }

        [AllowNull]
        [checkTelefone]
        public string Telefone { get; set; }
    }
}