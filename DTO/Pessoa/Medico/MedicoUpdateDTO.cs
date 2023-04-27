using m01_labMedicine.Models.Enum;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
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

        [JsonConverter(typeof(EspecializacaoClinicaConverter))]
        public EspecializacaoClinicaEnum EspecializacaoClinica { get; set; }

        [JsonConverter(typeof(SituacaoConverter))]
        public SituacaoEnum SituacaoSistema { get; set; }
        public int TotalAtendimentos { get; set; }
    }
}