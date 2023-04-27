using m01_labMedicine.Models.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace m01_labMedicine.Model
{
    [Table("Medico")]
    public class MedicoModel : Pessoa
    {        
        [NotNull]
        [MaxLength(250)]
        public string InstituicaoEnsinoFormacao { get; set; }

        [NotNull]
        public string CrmUF { get; set; }

        [NotNull]
        public EspecializacaoClinicaEnum EspecializacaoClinica { get; set; }

        [NotNull]
        public SituacaoEnum EstadoSistema { get; set; }

        [NotNull]
        public int TotalAtendimentosRealizados { get; set; }
    }
}
