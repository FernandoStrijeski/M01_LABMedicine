using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace m01_labMedicine.DTO
{
    public class EnfermeiroDTO : PessoaDTO
    {
        public string InstituicaoEnsino { get; set; }
        public string CofenUF { get; set; }
    }
}