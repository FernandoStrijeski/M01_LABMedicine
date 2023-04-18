using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace m01_labMedicine.Model
{
    [Table("Enfermeiro")]
    public class EnfermeiroModel : Pessoa
    {        
        [NotNull]
        [MaxLength(250)]
        public string InstituicaoEnsinoFormacaoo { get; set; }

        [NotNull]
        public string CofenUF { get; set; }
    }
}
