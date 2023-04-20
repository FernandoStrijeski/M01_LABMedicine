using m01_labMedicine.DTO.Pessoa;
using m01_labMedicine.DTO.Pessoa.Paciente;
using m01_labMedicine.Model;
using Microsoft.AspNetCore.Mvc;

namespace m01_labMedicine.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PacienteController : ControllerBase
    {
        private readonly LabMedicineContext _atendimentoMedicoContext;

        //Construtor com parametro (Injetado)   
        public PacienteController(LabMedicineContext atendimentoMedicoContext) => _atendimentoMedicoContext = atendimentoMedicoContext;


        [HttpPost("/api/pacientes/")]
        public ActionResult<PacienteResponseDTO> Post([FromBody] PacienteRequestDTO pacienteDTO)
        {
            try
            {

                PacienteModel pacienteModel = new()
                {
                    NomeCompleto = pacienteDTO.Nome,
                    Genero = pacienteDTO.Genero,
                    DataNascimento = pacienteDTO.DataNascimento,
                    CPF = pacienteDTO.CPF,
                    Telefone = pacienteDTO.Telefone,
                    ContatoEmergencia = pacienteDTO.ContatoEmergencia,
                    Alergias = pacienteDTO.Alergias,
                    CuidadosEspecificos = pacienteDTO.CuidadosEspecificos,
                    Convenio = pacienteDTO.Convenio,
                    StatusAtendimento = pacienteDTO.StatusAtendimento
                };

                //Verificar se existe o Paciente no banco de dados
                var pacienteModelDb = _atendimentoMedicoContext.Paciente.Where(x => x.CPF == pacienteDTO.CPF).FirstOrDefault();
                if (pacienteModelDb != null)
                    return Conflict($"Paciente com o CPF informado já cadastrado [{pacienteModelDb.NomeCompleto}]!");

                //Add na lista do DBSet Paciente
                _atendimentoMedicoContext.Paciente.Add(pacienteModel);

                //Salvar no banco de dados
                _atendimentoMedicoContext.SaveChanges();

                PacienteResponseDTO pacienteResponseDTO = new()
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
                };

                return Created("", pacienteResponseDTO);

            }
            catch (Exception)
            {
                return BadRequest("Dados inválidos!");
            }
        }

        [HttpPut("/api/pacientes/{identificador}")]
        public ActionResult<PacienteResponseDTO> Put([FromRoute] int identificador, PacienteUpdateDTO pacienteUpdateDTO)
        {
            try
            {
                //Verificar se existe o paciente no banco de dados
                var pacienteModel = _atendimentoMedicoContext.Paciente.Where(x => x.Id == identificador).FirstOrDefault();

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
                    _atendimentoMedicoContext.Paciente.Attach(pacienteModel);

                    //Salvar no banco de dados
                    _atendimentoMedicoContext.SaveChanges();

                    PacienteResponseDTO pacienteResponseDTO = new()
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
                    };

                    return Ok(pacienteResponseDTO);
                }
                else
                    return NotFound("Paciente não encontrado com o identificador informado.");

            }
            catch (Exception)
            {
                return BadRequest("Dados inválidos!");
            }
        }

        [HttpDelete("/api/pacientes/{identificador}")]
        public ActionResult Delete([FromRoute] int identificador)
        {
            //Verificar se existe no banco de dados
            var pacienteModel = _atendimentoMedicoContext.Paciente.Find(identificador);

            if (pacienteModel != null)
            {
                _atendimentoMedicoContext.Paciente.Remove(pacienteModel);
                _atendimentoMedicoContext.SaveChanges();

                return NoContent();
            }
            else
                return NotFound("Paciente não encontrado com o identificador informado.");
        }

        //Devolve todos os registros ou pelo status opcional
        [HttpGet("/api/pacientes")]
        public ActionResult<List<PacienteResponseDTO>> Get([FromQuery] PacienteStatusRequestDTO status)
        {
            List<PacienteResponseDTO> lista = new();
            IQueryable<PacienteModel> pacientesInnerJoin;

            if (status.StatusAtendimento != null)
            {
                pacientesInnerJoin = _atendimentoMedicoContext.Paciente.Where(x => x.StatusAtendimento == status.StatusAtendimento);
            }
            else
                pacientesInnerJoin = _atendimentoMedicoContext.Paciente;

            if (pacientesInnerJoin.Count() > 0)
            {
                foreach (var paciente in pacientesInnerJoin)
                {
                    PacienteResponseDTO pacienteGet = new()
                    {
                        Codigo = paciente.Id,
                        Nome = paciente.NomeCompleto,
                        Genero = paciente.Genero,
                        DataNascimento = paciente.DataNascimento,
                        CPF = paciente.CPF,
                        Telefone = paciente.Telefone,
                        ContatoEmergencia = paciente.ContatoEmergencia,
                        Alergias = paciente.Alergias,
                        CuidadosEspecificos = paciente.CuidadosEspecificos,
                        Convenio = paciente.Convenio,
                        StatusAtendimento = paciente.StatusAtendimento,
                        Atendimentos = paciente.TotalAtendimentos
                    };

                    lista.Add(pacienteGet);
                }

                return Ok(lista);
            }
            else
            {
                if (status.StatusAtendimento != "")
                    return NotFound("Nenhum paciente encontrado para o status informado.");
                else
                    return NotFound("Nenhum paciente cadastrado.");
            }
        }

        //Devolve por id
        [HttpGet("/api/pacientes/{identificador}")]
        public ActionResult<PacienteResponseDTO> GetPorId([FromRoute] int identificador)
        {
            PacienteModel pacienteInnerJoin = _atendimentoMedicoContext.Paciente.Where(w => w.Id == identificador).FirstOrDefault();
            if (pacienteInnerJoin == null)
                return NotFound("Paciente não encontrado para o identificador informado.");

            PacienteResponseDTO pacienteResponseDTO = new()
            {
                Codigo = pacienteInnerJoin.Id,
                Nome = pacienteInnerJoin.NomeCompleto,
                Genero = pacienteInnerJoin.Genero,
                DataNascimento = pacienteInnerJoin.DataNascimento,
                CPF = pacienteInnerJoin.CPF,
                Telefone = pacienteInnerJoin.Telefone,
                ContatoEmergencia = pacienteInnerJoin.ContatoEmergencia,
                Alergias = pacienteInnerJoin.Alergias,
                CuidadosEspecificos = pacienteInnerJoin.CuidadosEspecificos,
                Convenio = pacienteInnerJoin.Convenio,
                StatusAtendimento = pacienteInnerJoin.StatusAtendimento,
                Atendimentos = pacienteInnerJoin.TotalAtendimentos
            };

            return Ok(pacienteInnerJoin);
        }
    }
}