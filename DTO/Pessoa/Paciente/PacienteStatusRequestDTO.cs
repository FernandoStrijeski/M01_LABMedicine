using static m01_labMedicine.Core.Validation.CustomValidation;

namespace m01_labMedicine.DTO.Pessoa.Paciente
{
    public class PacienteStatusRequestDTO
    {
        [CheckStatusAtendimento(AllowStatusNull = true, AllowStatus = "AGUARDANDO_ATENDIMENTO,EM_ATENDIMENTO,ATENDIDO,NAO_ATENDIDO")]
        public string StatusAtendimento { get; set; }
    }
}