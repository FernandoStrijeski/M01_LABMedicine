using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using m01_labMedicine.DTO.Pessoa;
using static m01_labMedicine.Validation.CustomValidation;

namespace m01_labMedicine.DTO.Pessoa.Medico
{
    public class EnfermeiroUpdateDTO : PessoaUpdateDTO
    {
        [Required]
        [StringLength(maximumLength: 250)]
        public string InstituicaoEnsino { get; set; }

        [Required]
        [StringLength(maximumLength: 20)]
        public string CofenUF { get; set; }
        public string SituacaoSistema { get; set; }
        public int TotalAtendimentos { get; set; }
    }
}