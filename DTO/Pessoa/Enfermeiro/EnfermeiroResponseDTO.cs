using m01_labMedicine.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace m01_labMedicine.DTO.Pessoa.Medico
{
    public class EnfermeiroResponseDTO : PessoaDTO
    {
        public string InstituicaoEnsino { get; set; }
        public string CofenUF { get; set; }

        [JsonPropertyName("identificador")]
        public int Codigo { get; set; }
        public int Atendimentos { get; set; }
    }
}