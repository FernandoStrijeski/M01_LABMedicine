using m01_labMedicine.DTO.Atendimento;
using m01_labMedicine.DTO.Pessoa.Medico;
using m01_labMedicine.DTO.Pessoa.Paciente;
using m01_labMedicine.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace m01_labMedicine.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AtendimentosController : ControllerBase
    {
        private readonly LabMedicineContext _atendimentoMedicoContext;

        //Construtor com parametro (Injetado)   
        public AtendimentosController(LabMedicineContext atendimentoMedicoContext) => _atendimentoMedicoContext = atendimentoMedicoContext;

        [HttpPut("/api/atendimentos")]
        public ActionResult<AtendimentoMedicoResponseDTO> Put([FromBody] AtendimentoMedicoRequestDTO atendimentoMeditoRequestDTO)
        {
            try
            {
                //Verificar se existe o medico no banco de dados
                var medicoModel = _atendimentoMedicoContext.Medico.Where(x => x.Id == atendimentoMeditoRequestDTO.IdMedico).FirstOrDefault();
                if (medicoModel == null)
                    return NotFound("Médico não encontrado no sistema para o identificador informado! Revise.");

                //Verificar se existe o paciente no banco de dados
                var pacienteModel = _atendimentoMedicoContext.Paciente.Where(x => x.Id == atendimentoMeditoRequestDTO.IdPaciente).FirstOrDefault();
                if (pacienteModel == null)
                    return NotFound("Paciente não encontrado no sistema para o identificador informado! Revise.");

                medicoModel.TotalAtendimentosRealizados += 1;

                //Add na lista do DBSet Medico
                _atendimentoMedicoContext.Medico.Attach(medicoModel);

                pacienteModel.TotalAtendimentos += 1;
                pacienteModel.StatusAtendimento = "ATENDIDO";

                //Add na lista do DBSet Paciente
                _atendimentoMedicoContext.Paciente.Attach(pacienteModel);

                AtendimentoMedicoModel atendimentoMedicoModel = new()
                {
                    MedicoId = medicoModel.Id,
                    Medico = medicoModel,
                    PacienteId = pacienteModel.Id,
                    Paciente = pacienteModel,
                    DescricaoAtendimento = atendimentoMeditoRequestDTO.DescricaoAtendimento
                };
                _atendimentoMedicoContext.AtendimentoMedico.Add(atendimentoMedicoModel);

                //Salvar no banco de dados
                _atendimentoMedicoContext.SaveChanges();

                AtendimentoMedicoResponseDTO atendimentoResponseDTO = new()
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
                        Atendimentos = medicoModel.TotalAtendimentosRealizados
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
                        Atendimentos = pacienteModel.TotalAtendimentos
                    }
                };

                return Ok(atendimentoResponseDTO);
            }
            catch (Exception)
            {
                return BadRequest("Dados inválidos!");
            }
        }

        //Devolve todos os registros ou pelo medicoId
        [HttpGet("/api/atendimentos")]
        public ActionResult<List<AtendimentoMedicoResponseDTO>> Get([FromQuery] int medicoId = 0)
        {
            List<AtendimentoMedicoResponseDTO> lista = new();
            IQueryable<AtendimentoMedicoModel> atendimentosMedicoInnerJoin;

            if (medicoId > 0)
                atendimentosMedicoInnerJoin = _atendimentoMedicoContext.AtendimentoMedico.Where(x => x.MedicoId == medicoId)
                    .Include(a => a.Paciente)
                    .Include(a => a.Medico);
            else
                atendimentosMedicoInnerJoin = _atendimentoMedicoContext.AtendimentoMedico
                    .Include(a => a.Paciente)
                    .Include(a => a.Medico);

            if (atendimentosMedicoInnerJoin.Count() > 0)
            {
                foreach (var atendimentoMedico in atendimentosMedicoInnerJoin)
                {                    
                    AtendimentoMedicoResponseDTO atendimentoMedicoResponseDTO = new()
                    {
                        Paciente = new()
                        {
                            Codigo = atendimentoMedico.PacienteId,
                            Nome = atendimentoMedico.Paciente.NomeCompleto,
                            Genero = atendimentoMedico.Paciente.Genero,
                            DataNascimento = atendimentoMedico.Paciente.DataNascimento,
                            CPF = atendimentoMedico.Paciente.CPF,
                            Telefone = atendimentoMedico.Paciente.Telefone,
                            ContatoEmergencia = atendimentoMedico.Paciente.ContatoEmergencia,
                            Alergias = atendimentoMedico.Paciente.Alergias,
                            CuidadosEspecificos = atendimentoMedico.Paciente.CuidadosEspecificos,
                            Convenio = atendimentoMedico.Paciente.Convenio,
                            StatusAtendimento = atendimentoMedico.Paciente.StatusAtendimento,
                            Atendimentos = atendimentoMedico.Paciente.TotalAtendimentos
                        },
                        Medico = new()
                        {
                            Codigo = atendimentoMedico.MedicoId,
                            Nome = atendimentoMedico.Medico.NomeCompleto,
                            Genero = atendimentoMedico.Medico.Genero,
                            DataNascimento = atendimentoMedico.Medico.DataNascimento,
                            CPF = atendimentoMedico.Medico.CPF,
                            Telefone = atendimentoMedico.Medico.Telefone,
                            CrmUF = atendimentoMedico.Medico.CrmUF,
                            InstituicaoEnsinoFormacao = atendimentoMedico.Medico.InstituicaoEnsinoFormacao,
                            EspecializacaoClinica = atendimentoMedico.Medico.EspecializacaoClinica,
                            EstadoSistema = atendimentoMedico.Medico.EstadoSistema,
                            Atendimentos = atendimentoMedico.Medico.TotalAtendimentosRealizados
                        },
                        DescricaoAtendimento = atendimentoMedico.DescricaoAtendimento
                    };

                    lista.Add(atendimentoMedicoResponseDTO);
                }
                return Ok(lista);
            }
            else
            {
                if (medicoId > 0)
                    return NotFound("Nenhum atendimento encontrado para o médico informado.");
                else
                    return NotFound("Nenhum atendimento realizado.");
            }
        }
    }
}