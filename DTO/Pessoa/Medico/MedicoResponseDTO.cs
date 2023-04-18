using m01_labMedicine.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace m01_labMedicine.DTO.Pessoa.Medico
{
    public class MedicoResponseDTO : PessoaDTO
    {
        public string InstituicaoEnsinoFormacao { get; set; }
        public string CrmUF { get; set; }
        public string EspecializacaoClinica { get; set; }
        public string EstadoSistema { get; set; }

        [JsonPropertyName("identificador")]
        public int Codigo { get; set; }
        public int atendimentos { get; set; }
    }
}