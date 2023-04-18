using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace m01_labMedicine.DTO.Pessoa.Enfermeiro
{
    public class EnfermeiroRequestDTO : PessoaDTO
    {
        [Required]
        [StringLength(maximumLength: 250)]
        public string InstituicaoEnsino { get; set; }
        
        [Required]
        [StringLength(maximumLength: 20)]
        public string CofenUF { get; set; }
    }
}