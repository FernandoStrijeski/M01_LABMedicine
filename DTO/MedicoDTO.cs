using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace m01_labMedicine.DTO
{
    public class MedicoDTO : PessoaDTO
    {
        public string InstituicaoEnsino { get; set; }
        public string CRMUF { get; set; }        
        public string EspecializacaoClinica { get; set; }        
        public string SituacaoSistema { get; set; }        
        public int TotalAtendimentos { get; set; }
    }
}