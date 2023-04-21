using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using m01_labMedicine.DTO.Pessoa;
using static m01_labMedicine.Core.Validation.CustomValidation;

namespace m01_labMedicine.DTO.Pessoa.Medico
{
    public class MedicoUpdateDTO : PessoaUpdateDTO
    {
        [Required]
        [StringLength(maximumLength: 250)]
        public string InstituicaoEnsino { get; set; }
        [Required]
        [StringLength(maximumLength: 20)]
        public string CRMUF { get; set; }
        [Required]
        [CheckEspecializacaoClinica(AllowEspecializacoes = "Clínico Geral,Anestesista,Dermatologia,Ginecologia,Neurologia,Pediatria,Psiquiatria,Ortopedia")]
        public string EspecializacaoClinica { get; set; }
        [Required]
        [CheckSituacao(AllowSituacoes = "Ativo,Inativo")]
        public string SituacaoSistema { get; set; }
        public int TotalAtendimentos { get; set; }
    }
}