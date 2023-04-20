using m01_labMedicine.DTO.Pessoa.Paciente;
using m01_labMedicine.Extension;
using m01_labMedicine.Model;
using m01_labMedicine.Services;
using Microsoft.AspNetCore.Mvc;

namespace m01_labMedicine.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PacienteController : ControllerBase
    {
        private readonly LabMedicineContext _atendimentoMedicoContext;
        private readonly IPacienteService _pacienteService;

        //Construtor com parametro (Injetado)   
        public PacienteController(LabMedicineContext atendimentoMedicoContext, IPacienteService pacienteService)
        {
            _atendimentoMedicoContext = atendimentoMedicoContext;
            _pacienteService = pacienteService;
        }

        [HttpPost("/api/pacientes/")]
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

        [HttpPut("/api/pacientes/{identificador}")]
        public ActionResult<PacienteResponseDTO> Put([FromRoute] int identificador, PacienteUpdateDTO pacienteUpdateDTO)
        {
            try 
            {
                PacienteResponseDTO pacienteResponseDTO = _pacienteService.Atualiza(identificador, pacienteUpdateDTO);
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

        //Devolve todos os registros ou pelo status opcional
        [HttpGet("/api/pacientes")]
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

        //Devolve por id
        [HttpGet("/api/pacientes/{identificador}")]
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