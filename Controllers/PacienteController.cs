using m01_labMedicine.DTO;
using m01_labMedicine.Model;
using m01_labMedicine.Services;
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
            pacienteResponseDTO.Genero = pacienteModel.Genero;
            pacienteResponseDTO.DataNascimento = pacienteModel.DataNascimento;
            pacienteResponseDTO.CPF = pacienteModel.CPF;
            pacienteResponseDTO.Telefone = pacienteModel.Telefone;
            pacienteResponseDTO.ContatoEmergencia = pacienteModel.ContatoEmergencia;
            pacienteResponseDTO.Alergias = pacienteModel.Alergias;
            pacienteResponseDTO.CuidadosEspecificos = pacienteModel.CuidadosEspecificos;
            pacienteResponseDTO.Convenio = pacienteModel.Convenio;
            pacienteResponseDTO.StatusAtendimento = pacienteModel.StatusAtendimento;
            pacienteResponseDTO.atendimentos = pacienteModel.TotalAtendimentos;

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
                pacienteModel.Genero = pacienteUpdateDTO.Genero;
                pacienteModel.DataNascimento = pacienteUpdateDTO.DataNascimento;
                pacienteModel.Telefone = pacienteUpdateDTO.Telefone;
                pacienteModel.ContatoEmergencia = pacienteUpdateDTO.ContatoEmergencia;
                pacienteModel.Alergias = pacienteUpdateDTO.Alergias;
                pacienteModel.CuidadosEspecificos = pacienteUpdateDTO.CuidadosEspecificos;
                pacienteModel.Convenio = pacienteUpdateDTO.Convenio;
                pacienteModel.StatusAtendimento = pacienteUpdateDTO.StatusAtendimento;
               
                //Add na lista do DBSet Paciente
                atendimentoMedicoContext.Paciente.Attach(pacienteModel);

                //Salvar no banco de dados
                atendimentoMedicoContext.SaveChanges();

                PacienteResponseDTO pacienteResponseDTO = new PacienteResponseDTO();
                pacienteResponseDTO.Codigo = pacienteModel.Id;
                pacienteResponseDTO.Nome = pacienteModel.NomeCompleto;
                pacienteResponseDTO.Genero = pacienteModel.Genero;
                pacienteResponseDTO.DataNascimento = pacienteModel.DataNascimento;
                pacienteResponseDTO.CPF = pacienteModel.CPF;
                pacienteResponseDTO.Telefone = pacienteModel.Telefone;
                pacienteResponseDTO.ContatoEmergencia = pacienteModel.ContatoEmergencia;
                pacienteResponseDTO.Alergias = pacienteModel.Alergias;
                pacienteResponseDTO.CuidadosEspecificos = pacienteModel.CuidadosEspecificos;
                pacienteResponseDTO.Convenio = pacienteModel.Convenio;
                pacienteResponseDTO.StatusAtendimento = pacienteModel.StatusAtendimento;
                pacienteResponseDTO.atendimentos = pacienteModel.TotalAtendimentos;

                return Ok(pacienteResponseDTO);
            }
            else
            {
                return NotFound("Paciente não encontrado com o identificador informado.");
            }
        }

        [HttpDelete("/api/pacientes/{identificador}")]
        public ActionResult Delete([FromRoute] int identificador)
        {
            //Verificar se existe no banco de dados
            var PacienteModel = atendimentoMedicoContext.Paciente.Find(identificador);

            if (PacienteModel != null)
            {
                atendimentoMedicoContext.Paciente.Remove(PacienteModel);
                atendimentoMedicoContext.SaveChanges();

                return NoContent();
            }
            else
            {
                //nao encontrado
                return NotFound("Paciente não encontrado com o identificador informado.");
            }
        }

        //Devolve todos os registros ou pelo status opcional
        [HttpGet("/api/pacientes")]
        public ActionResult<List<PacienteResponseDTO>> Get(string status = "")
        {
            List<PacienteResponseDTO> lista = new();
            IQueryable<PacienteModel> pacientesInnerJoin;

            if (status != "")
            {
                List<string> lstStatus = new(new string[] { "AGUARDANDO_ATENDIMENTO", "EM_ATENDIMENTO", "ATENDIDO", "NAO_ATENDIDO" });
                if(!lstStatus.Contains(status.ToUpper()))
                    return BadRequest("O status informado não existe.");

                pacientesInnerJoin = atendimentoMedicoContext.Paciente.Where(x => x.StatusAtendimento == status);            
            }            
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
                    pacienteGet.atendimentos = paciente.TotalAtendimentos;

                    lista.Add(pacienteGet);
                }

                return Ok(lista);
            }
            else
            {
                if (status != "")
                    return NotFound("Nenhum paciente encontrado para o status informado.");
                else
                    return NotFound("Nenhum paciente cadastrado.");
            }
        }

        //Devolve por id
        [HttpGet("/api/pacientes/{identificador}")]
        public ActionResult<PacienteResponseDTO> GetPorId([FromRoute] int identificador)
        {
            PacienteModel pacienteInnerJoin = atendimentoMedicoContext.Paciente.Where(w => w.Id == identificador).FirstOrDefault();
            if (pacienteInnerJoin == null)
            {
                return NotFound("Paciente não encontrado para o identificador informado.");
            }

            PacienteResponseDTO pacienteResponseDTO = new PacienteResponseDTO();
            pacienteResponseDTO.Codigo = pacienteInnerJoin.Id;
            pacienteResponseDTO.Nome = pacienteInnerJoin.NomeCompleto;
            pacienteResponseDTO.Genero = pacienteInnerJoin.Genero;
            pacienteResponseDTO.DataNascimento = pacienteInnerJoin.DataNascimento;
            pacienteResponseDTO.CPF = pacienteInnerJoin.CPF;
            pacienteResponseDTO.Telefone = pacienteInnerJoin.Telefone;
            pacienteResponseDTO.ContatoEmergencia = pacienteInnerJoin.ContatoEmergencia;
            pacienteResponseDTO.Alergias = pacienteInnerJoin.Alergias;
            pacienteResponseDTO.CuidadosEspecificos = pacienteInnerJoin.CuidadosEspecificos;
            pacienteResponseDTO.Convenio = pacienteInnerJoin.Convenio;
            pacienteResponseDTO.StatusAtendimento = pacienteInnerJoin.StatusAtendimento;
            pacienteResponseDTO.atendimentos = pacienteInnerJoin.TotalAtendimentos;

            return Ok(pacienteInnerJoin);
        }
    }
}