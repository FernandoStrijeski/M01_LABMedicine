using m01_labMedicine.Core.Exceptions;
using m01_labMedicine.DTO.Pessoa.Medico;
using m01_labMedicine.DTO.Pessoa.Paciente;
using m01_labMedicine.Model;
using m01_labMedicine.Services.Paciente;
using Microsoft.AspNetCore.Mvc;

namespace m01_labMedicine.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PacienteController : ControllerBase
    {
        private readonly IPacienteService _pacienteService;

        //Construtor com parametro (Injetado)   
        public PacienteController(LabMedicineContext atendimentoMedicoContext, IPacienteService pacienteService) =>  _pacienteService = pacienteService;

        /// <summary>
        /// Inclui um paciente
        /// </summary>
        /// <response code="201">Criado com sucesso, retornando as informações do cadastro.</response>
        /// <response code="400">Erro na requisição devido preenchimento incorreto dos campos esperados.</response>
        /// <response code="409">Paciente já cadastrado.</response>
        [HttpPost("/api/pacientes/")]
        [ProducesResponseType(typeof(PacienteResponseDTO), StatusCodes.Status201Created)]
        public ActionResult<PacienteResponseDTO> Post([FromBody] PacienteRequestDTO pacienteDTO)
        {
            try
            {
                PacienteResponseDTO pacienteResponseDTO = _pacienteService.Insere(pacienteDTO);
                return Created("", pacienteResponseDTO);
            }
            catch (MyException ex)
            {
                return StatusCode(ex.ErrorCode, ex.Message);
            }
            catch (Exception)
            {
                return BadRequest($"Ocorreu um erro na requisição! Tente novamente mais tarde.");
            }
        }

        /// <summary>
        /// Altera um paciente
        /// </summary>
        /// <response code="200">Alterado com sucesso, retornando as informações do cadastro.</response>
        /// <response code="400">Erro na requisição devido preenchimento incorreto dos campos esperados.</response>
        /// <response code="404">Paciente não encontrado para o identificador informado.</response>
        [HttpPut("/api/pacientes/{identificador}")]
        [ProducesResponseType(typeof(PacienteResponseDTO), StatusCodes.Status200OK)]
        public ActionResult<PacienteResponseDTO> Put([FromRoute] int identificador, PacienteUpdateDTO pacienteUpdateDTO)
        {
            try 
            {
                PacienteResponseDTO pacienteResponseDTO = _pacienteService.Atualiza(identificador, pacienteUpdateDTO);
                return Ok(pacienteResponseDTO);
            }
            catch (MyException ex)
            {
                return StatusCode(ex.ErrorCode, ex.Message);
            }
            catch (Exception)
            {
                return BadRequest($"Ocorreu um erro na requisição! Tente novamente mais tarde.");
            }
        }

        /// <summary>
        /// Remove um paciente
        /// </summary>
        /// <response code="204">Paciente removido com sucesso.</response>
        /// <response code="400">Erro na requisição devido preenchimento incorreto dos campos esperados.</response>
        /// <response code="404">Paciente não encontrado para o identificador fornecido.</response>
        [HttpDelete("/api/pacientes/{identificador}")]
        public ActionResult Delete([FromRoute] int identificador)
        {
            try
            {
                _pacienteService.Remove(identificador);
                return NoContent();
            }
            catch (MyException ex)
            {
                return StatusCode(ex.ErrorCode, ex.Message);
            }
            catch (Exception)
            {
                return BadRequest($"Ocorreu um erro na requisição! Tente novamente mais tarde.");
            }
        }

        /// <summary>
        /// Lista os pacientes (status opcional)
        /// </summary>
        /// <response code="200">Retorna a lista com as informações do cadastro.</response>
        /// <response code="400">Erro na requisição devido preenchimento incorreto dos campos esperados.</response>
        /// <response code="404">Nenhum paciente encontrado.</response>
        [HttpGet("/api/pacientes")]
        [ProducesResponseType(typeof(List<PacienteResponseDTO>), StatusCodes.Status200OK)]
        public ActionResult<List<PacienteResponseDTO>> Get([FromQuery] PacienteStatusRequestDTO status)
        {
            try
            {
                List<PacienteResponseDTO> pacientesResponseDTO = _pacienteService.BuscaPacientes(status);
                return Ok(pacientesResponseDTO);
            }
            catch (MyException ex)
            {
                return StatusCode(ex.ErrorCode, ex.Message);
            }
            catch (Exception)
            {
                return BadRequest($"Ocorreu um erro na requisição! Tente novamente mais tarde.");
            }            
        }

        /// <summary>
        /// Busca um paciente
        /// </summary>
        /// <response code="200">Retorna as informações do cadastro.</response>
        /// <response code="400">Erro na requisição devido preenchimento incorreto dos campos esperados.</response>
        /// <response code="404">Paciente não encontrado para o identificador fornecido.</response>
        [HttpGet("/api/pacientes/{identificador}")]
        [ProducesResponseType(typeof(PacienteResponseDTO), StatusCodes.Status200OK)]
        public ActionResult<PacienteResponseDTO> GetPorId([FromRoute] int identificador)
        {
            try
            {
                PacienteResponseDTO pacienteResponseDTO = _pacienteService.BuscaPaciente(identificador);
                return Ok(pacienteResponseDTO);
            }
            catch (MyException ex)
            {
                return StatusCode(ex.ErrorCode, ex.Message);
            }
            catch (Exception)
            {
                return BadRequest($"Ocorreu um erro na requisição! Tente novamente mais tarde.");
            }

        }
    }
}