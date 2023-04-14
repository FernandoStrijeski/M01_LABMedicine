using m01_labMedicine.DTO;
using m01_labMedicine.Model;
using Microsoft.AspNetCore.Mvc;

namespace m01_labMedicine.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PacienteController : ControllerBase
    {
        private readonly AtendimentoMedicoContext atendimentoMedicoContext;

        //Construtor com parametro (Injetado)   
        public PacienteController(AtendimentoMedicoContext atendimentoMedicoContext)
        {
            this.atendimentoMedicoContext = atendimentoMedicoContext;
        }

        [HttpPost("/api/pacientes/")]
        public ActionResult<PacienteResponseDTO> Post([FromBody] PacienteDTO pacienteDTO)
        {
            PacienteModel pacienteModel = new PacienteModel();
            pacienteModel.NomeCompleto = pacienteDTO.Nome;
            pacienteModel.Genero = pacienteDTO.Genero;
            pacienteModel.DataNascimento = pacienteDTO.DataNascimento;
            pacienteModel.CPF = pacienteDTO.CPF;
            pacienteModel.Telefone = pacienteDTO.Telefone;
            pacienteModel.ContatoEmergencia = pacienteDTO.ContatoEmergencia;
            pacienteModel.Alergias = pacienteDTO.Alergias;
            pacienteModel.CuidadosEspecificos = pacienteDTO.CuidadosEspecificos;
            pacienteModel.Convenio = pacienteDTO.Convenio;
            pacienteModel.StatusAtendimento = pacienteDTO.StatusAtendimento;


            //Verificar se existe o Paciente no banco de dados
            var pacienteModelDb = atendimentoMedicoContext.Paciente.Where(x => x.CPF == pacienteDTO.CPF).FirstOrDefault();
            if(pacienteModelDb != null)
                return Conflict($"Paciente com o CPF informado já cadastrado [{pacienteModelDb.NomeCompleto}]!");

            //Add na lista do DBSet Paciente
            atendimentoMedicoContext.Paciente.Add(pacienteModel);

            //Salvar no banco de dados
            atendimentoMedicoContext.SaveChanges();

            PacienteResponseDTO pacienteResponseDTO = new PacienteResponseDTO();
            pacienteResponseDTO.Codigo = pacienteModel.Id;
            pacienteResponseDTO.Nome = pacienteModel.NomeCompleto;


            //-----------------------------------------------------------------------------------------------------------------------------------------------------
            //completar os campos que faltam incluindo ID E TOTAL DE ATENDIMENTOS
            //-----------------------------------------------------------------------------------------------------------------------------------------------------
            return Created("", pacienteResponseDTO);
        }

        [HttpPut("/api/pacientes/{identificador}")]
        public ActionResult<PacienteResponseDTO> Put([FromRoute] int identificador, PacienteDTO pacienteUpdateDTO)
        {
            //Verificar se existe o paciente no banco de dados
            var pacienteModel = atendimentoMedicoContext.Paciente.Where(x => x.Id == identificador).FirstOrDefault();

            if (pacienteModel != null)
            {
                pacienteModel.NomeCompleto = pacienteUpdateDTO.Nome;
                pacienteModel.DataNascimento = pacienteUpdateDTO.DataNascimento;

                //-----------------------------------------------------------------------------------------------------------------------------------------------------
                //completar os campos que faltam incluindo ID E TOTAL DE ATENDIMENTOS
                //-----------------------------------------------------------------------------------------------------------------------------------------------------

                //Add na lista do DBSet Paciente
                atendimentoMedicoContext.Paciente.Attach(pacienteModel);

                //Salvar no banco de dados
                atendimentoMedicoContext.SaveChanges();

                PacienteResponseDTO pacienteResponseDTO = new PacienteResponseDTO();
                pacienteResponseDTO.Codigo = pacienteModel.Id;
                //-----------------------------------------------------------------------------------------------------------------------------------------------------
                //completar os campos que faltam incluindo ID E TOTAL DE ATENDIMENTOS
                //-----------------------------------------------------------------------------------------------------------------------------------------------------
                return Ok(pacienteResponseDTO);
            }
            else
            {
                return NotFound("Paciente não encontrado com o identificador informado.");
            }
        }

        [HttpDelete("/api/pacientes/{identificador}")]
        public ActionResult Delete([FromRoute] int id)
        {
            //Verificar se existe no banco de dados
            var PacienteModel = atendimentoMedicoContext.Paciente.Find(id);

            if (PacienteModel != null)
            {
                atendimentoMedicoContext.Paciente.Remove(PacienteModel);
                atendimentoMedicoContext.SaveChanges();

                return Ok();
            }
            else
            {
                //nao encontrado
                return NotFound("Paciente não encontrado com o identificador informado.");
            }
        }


        //Devolve todos os registros
        [HttpGet("/api/pacientes/{identificador?}")]
        public ActionResult<List<PacienteResponseDTO>> Get(int? identificador = 0)
        {
            List<PacienteResponseDTO> lista = new List<PacienteResponseDTO>();
            IQueryable<PacienteModel> pacientesInnerJoin;

            if (identificador > 0)            
                pacientesInnerJoin = atendimentoMedicoContext.Paciente.Where(x => x.Id == identificador);            
            else           
                pacientesInnerJoin = atendimentoMedicoContext.Paciente;            
            
            if (pacientesInnerJoin.Count() > 0)
            {
                foreach (var paciente in pacientesInnerJoin)
                {
                    PacienteResponseDTO pacienteGet = new PacienteResponseDTO();

                    pacienteGet.Codigo = paciente.Id;
                    pacienteGet.Nome = paciente.NomeCompleto;
                    pacienteGet.Genero = paciente.Genero;
                    pacienteGet.DataNascimento = paciente.DataNascimento;
                    pacienteGet.CPF = paciente.CPF;
                    pacienteGet.Telefone = paciente.Telefone;
                    pacienteGet.ContatoEmergencia = paciente.ContatoEmergencia;
                    pacienteGet.Alergias = paciente.Alergias;
                    pacienteGet.CuidadosEspecificos = paciente.CuidadosEspecificos;
                    pacienteGet.Convenio = paciente.Convenio;
                    pacienteGet.StatusAtendimento = paciente.StatusAtendimento;

                    lista.Add(pacienteGet);
                }

                return Ok(lista);
            }
            else
            {
                return NotFound("Nenhum paciente cadastrado.");
            }
        }

        //Devolve por id
        [HttpGet("/api/pacientes/{id}")]
        public ActionResult<PacienteResponseDTO> Get([FromRoute] int id)
        {            
            PacienteModel pacientesInnerJoin = atendimentoMedicoContext.Paciente.Where(w => w.Id == id).FirstOrDefault();
            if (pacientesInnerJoin == null)
            {
                return NotFound("Paciente não encontrado para o identificador informado.");
            }

            PacienteResponseDTO pacienteGetDto = new PacienteResponseDTO();
            pacienteGetDto.Codigo = pacientesInnerJoin.Id;

            //-----------------------------------------------------------------------------------------------------------------------------------------------------
            //completar os campos que faltam incluindo ID E TOTAL DE ATENDIMENTOS
            //-----------------------------------------------------------------------------------------------------------------------------------------------------

            return Ok(pacienteGetDto);
        }


    }
}