using System.ComponentModel.DataAnnotations;
using static m01_labMedicine.Core.Validation.CustomValidation;

namespace m01_labMedicine.DTO.Pessoa.Medico
{
    public class MedicoRequestDTO : PessoaDTO
    {
        [Required(ErrorMessage = "O preenchimento do campo Institui��o de Ensino � obrigat�rio")]
        [StringLength(maximumLength: 250)]
        public string InstituicaoEnsino { get; set; }
        [Required(ErrorMessage = "O preenchimento do campo CRMUF � obrigat�rio")]
        [StringLength(maximumLength: 20)]
        public string CRMUF { get; set; }
        [CheckEspecializacaoClinica(AllowEspecializacoes = "Cl�nico Geral,Anestesista,Dermatologia,Ginecologia,Neurologia,Pediatria,Psiquiatria,Ortopedia")]
        public string EspecializacaoClinica { get; set; }        
        [CheckSituacao(AllowSituacoes = "Ativo,Inativo")]
        public string SituacaoSistema { get; set; }
        public int TotalAtendimentos { get; set; }
    }
}