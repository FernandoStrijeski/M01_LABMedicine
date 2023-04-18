using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using m01_labMedicine.DTO.Pessoa;
using static m01_labMedicine.Validation.CustomValidation;

namespace m01_labMedicine.DTO.Pessoa.Paciente
{
    public class PacienteStatusRequestDTO
    {
        [checkStatusAtendimento(AllowStatusNull = true, AllowStatus = "AGUARDANDO_ATENDIMENTO,EM_ATENDIMENTO,ATENDIDO,NAO_ATENDIDO")]
        public string StatusAtendimento { get; set; }
    }
}