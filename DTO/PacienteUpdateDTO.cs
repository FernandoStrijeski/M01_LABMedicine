using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace m01_labMedicine.DTO
{
    public class PacienteUpdateDTO : PessoaUpdateDTO
    {
        public string ContatoEmergencia { get; set; }
        public List<string> Alergias { get; set; }
        public List<string> CuidadosEspecificos { get; set; }
        public string Convenio { get; set; }
        public string StatusAtendimento { get; set; }
    }
}