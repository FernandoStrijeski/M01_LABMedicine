using m01_labMedicine.DTO.Atendimento;
using m01_labMedicine.DTO.Pessoa.Medico;
using m01_labMedicine.DTO.Pessoa.Paciente;
using m01_labMedicine.Model;
using Microsoft.AspNetCore.Mvc;

namespace m01_labMedicine.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AtendimentosController : ControllerBase
    {
        private readonly LabMedicineContext atendimentoMedicoContext;

        //Construtor com parametro (Injetado)   
        public AtendimentosController(LabMedicineContext atendimentoMedicoContext) => this.atendimentoMedicoContext = atendimentoMedicoContext;

        [HttpPut("/api/atendimentos")]
        public ActionResult<AtendimentoResponseDTO> Put([FromBody] AtendimentoRequestDTO atendimentoUpdateDTO)
        {
            try
            {
                //Verificar se existe o medico no banco de dados
                var medicoModel = atendimentoMedicoContext.Medico.Where(x => x.Id == atendimentoUpdateDTO.IdMedico).FirstOrDefault();
                if (medicoModel == null)
                    return NotFound("Médico não encontrado no sistema para o identificador informado! Revise.");

                //Verificar se existe o paciente no banco de dados
                var pacienteModel = atendimentoMedicoContext.Paciente.Where(x => x.Id == atendimentoUpdateDTO.IdPaciente).FirstOrDefault();
                if (pacienteModel == null)
                    return NotFound("Paciente não encontrado no sistema para o identificador informado! Revise.");

                medicoModel.TotalAtendimentosRealizados += 1;

                //Add na lista do DBSet Medico
                atendimentoMedicoContext.Medico.Attach(medicoModel);

                pacienteModel.TotalAtendimentos += 1;
                pacienteModel.StatusAtendimento = "ATENDIDO";

                //Add na lista do DBSet Paciente
                atendimentoMedicoContext.Paciente.Attach(pacienteModel);

                //Salvar no banco de dados
                atendimentoMedicoContext.SaveChanges();

                AtendimentoResponseDTO atendimentoResponseDTO = new()
                {
                    Medico = new MedicoResponseDTO
                    {
                        Codigo = medicoModel.Id,
                        Nome = medicoModel.NomeCompleto,
                        Genero = medicoModel.Genero,
                        DataNascimento = medicoModel.DataNascimento,
                        CPF = medicoModel.CPF,
                        Telefone = medicoModel.Telefone,
                        InstituicaoEnsinoFormacao = medicoModel.InstituicaoEnsinoFormacao,
                        CrmUF = medicoModel.CrmUF,
                        EspecializacaoClinica = medicoModel.EspecializacaoClinica,
                        EstadoSistema = medicoModel.EstadoSistema,
                        atendimentos = medicoModel.TotalAtendimentosRealizados
                    },
                    Paciente = new PacienteResponseDTO
                    {
                        Codigo = pacienteModel.Id,
                        Nome = pacienteModel.NomeCompleto,
                        Genero = pacienteModel.Genero,
                        DataNascimento = pacienteModel.DataNascimento,
                        CPF = pacienteModel.CPF,
                        Telefone = pacienteModel.Telefone,
                        ContatoEmergencia = pacienteModel.ContatoEmergencia,
                        Alergias = pacienteModel.Alergias,
                        CuidadosEspecificos = pacienteModel.CuidadosEspecificos,
                        Convenio = pacienteModel.Convenio,
                        StatusAtendimento = pacienteModel.StatusAtendimento,
                        atendimentos = pacienteModel.TotalAtendimentos
                    }
                };

                return Ok(atendimentoResponseDTO);
            }
            catch (Exception)
            {
                return BadRequest("Dados inválidos!");
            }
        }
    }
}