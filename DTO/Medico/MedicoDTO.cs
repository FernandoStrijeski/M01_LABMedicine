using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static m01_labMedicine.Validation.CustomValidation;

namespace m01_labMedicine.DTO.Medico
{
    public class MedicoDTO : PessoaDTO
    {
        [Required]
        [StringLength(maximumLength: 250)]
        public string InstituicaoEnsino { get; set; }
        [Required]
        public string CRMUF { get; set; }
        [Required]
        [checkEspecializacaoClinica(AllowEspecializacoes = "Clínico Geral,Anestesista,Dermatologia,Ginecologia,Neurologia,Pediatria,Psiquiatria,Ortopedia")]
        public string EspecializacaoClinica { get; set; }
        [Required]
        [checkSituacao(AllowSituacoes = "Ativo,Inativo")]
        public string SituacaoSistema { get; set; }
        public int TotalAtendimentos { get; set; }
    }
}