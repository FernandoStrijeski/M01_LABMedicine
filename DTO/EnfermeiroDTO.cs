using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace m01_labMedicine.DTO
{
    public class EnfermeiroDTO : PessoaDTO
    {
        [Required]
        public string InstituicaoEnsino { get; set; }
        [Required]
        public string CofenUF { get; set; }
    }
}