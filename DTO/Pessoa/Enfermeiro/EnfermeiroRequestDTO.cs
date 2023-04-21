using System.ComponentModel.DataAnnotations;

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