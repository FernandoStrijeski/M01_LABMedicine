using m01_labMedicine.Core.Exceptions;
using m01_labMedicine.DTO.Pessoa.Enfermeiro;
using m01_labMedicine.DTO.Pessoa.Medico;
using m01_labMedicine.Services.Enfermeiro;
using Microsoft.AspNetCore.Mvc;

namespace m01_labMedicine.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnfermeiroController : ControllerBase
    {
        private readonly IEnfermeiroService _enfermeiroService;

        //Construtor com parametro (Injetado)   
        public EnfermeiroController(IEnfermeiroService enfermeiroService) => _enfermeiroService = enfermeiroService;
        
        [HttpPost("/api/enfermeiros/")]
        public ActionResult<EnfermeiroResponseDTO> Post([FromBody] EnfermeiroRequestDTO enfermeiroRequestDTO)
        {
            try
            {
                EnfermeiroResponseDTO enfermeiroResponseDTO = _enfermeiroService.Insere(enfermeiroRequestDTO);
                return Created("", enfermeiroResponseDTO);
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

        [HttpPut("/api/enfermeiros/{identificador}")]
        public ActionResult<EnfermeiroResponseDTO> Put([FromRoute] int identificador, EnfermeiroUpdateDTO enfermeiroUpdateDTO)
        {
            try
            {
                EnfermeiroResponseDTO enfermeiroResponseDTO = _enfermeiroService.Atualiza(identificador, enfermeiroUpdateDTO);
                return Ok(enfermeiroResponseDTO);
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

        //Devolve todos os registros
        [HttpGet("/api/enfermeiros")]
        public ActionResult<List<EnfermeiroResponseDTO>> Get()
        {
            try
            {
                List<EnfermeiroResponseDTO> enfermeirosResponseDTO = _enfermeiroService.BuscaEnfermeiros();
                return Ok(enfermeirosResponseDTO);
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
        [HttpGet("/api/enfermeiros/{identificador}")]
        public ActionResult<EnfermeiroResponseDTO> GetPorId([FromRoute] int identificador)
        {
            try
            {
                EnfermeiroResponseDTO enfermeiroResponseDTO = _enfermeiroService.BuscaEnfermeiro(identificador);
                return Ok(enfermeiroResponseDTO);
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

        [HttpDelete("/api/enfermeiros/{identificador}")]
        public ActionResult Delete([FromRoute] int identificador)
        {
            try
            {
                _enfermeiroService.Remove(identificador);
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
    }
}