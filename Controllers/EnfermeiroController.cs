using m01_labMedicine.Core.Exceptions;
using m01_labMedicine.DTO.Atendimento;
using m01_labMedicine.DTO.Pessoa.Enfermeiro;
using m01_labMedicine.DTO.Pessoa.Medico;
using m01_labMedicine.Services.Enfermeiro;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

namespace m01_labMedicine.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnfermeiroController : ControllerBase
    {
        private readonly IEnfermeiroService _enfermeiroService;

        //Construtor com parametro (Injetado)   
        public EnfermeiroController(IEnfermeiroService enfermeiroService) => _enfermeiroService = enfermeiroService;

        /// <summary>
        /// Inclui um enfermeiro
        /// </summary>
        /// <response code="201">Criado com sucesso, retornando as informações do cadastro.</response>
        /// <response code="400">Erro na requisição devido preenchimento incorreto dos campos esperados.</response>
        /// <response code="409">Enfermeiro já cadastrado.</response>
        [HttpPost("/enfermeiros/")]
        [ProducesResponseType(typeof(EnfermeiroResponseDTO), StatusCodes.Status201Created)]
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

        /// <summary>
        /// Altera um enfermeiro
        /// </summary>
        /// <response code="200">Alterado com sucesso, retornando as informações do cadastro.</response>
        /// <response code="400">Erro na requisição devido preenchimento incorreto dos campos esperados.</response>
        /// <response code="404">Enfermeiro não encontrado para o identificador informado.</response>
        [HttpPut("/enfermeiros/{identificador}")]
        [ProducesResponseType(typeof(EnfermeiroResponseDTO), StatusCodes.Status200OK)]
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

        /// <summary>
        /// Lista os enfermeiros
        /// </summary>
        /// <response code="200">Retorna a lista com as informações do cadastro.</response>
        /// <response code="400">Erro na requisição devido preenchimento incorreto dos campos esperados.</response>
        /// <response code="404">Nenhum enfermeiro encontrado.</response>
        [HttpGet("/enfermeiros")]
        [ProducesResponseType(typeof(List<EnfermeiroResponseDTO>), StatusCodes.Status200OK)]
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

        /// <summary>
        /// Busca um enfermeiro
        /// </summary>
        /// <response code="200">Retorna as informações do cadastro.</response>
        /// <response code="400">Erro na requisição devido preenchimento incorreto dos campos esperados.</response>
        /// <response code="404">Enfermeiro não encontrado para o identificador fornecido.</response>
        [HttpGet("/enfermeiros/{identificador}")]
        [ProducesResponseType(typeof(EnfermeiroResponseDTO), StatusCodes.Status200OK)]
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

        /// <summary>
        /// Remove um enfermeiro
        /// </summary>
        /// <response code="204">Enfermeiro removido com sucesso.</response>
        /// <response code="400">Erro na requisição devido preenchimento incorreto dos campos esperados.</response>
        /// <response code="404">Enfermeiro não encontrado para o identificador fornecido.</response>
        [HttpDelete("/enfermeiros/{identificador}")]
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