using m01_labMedicine.Core.Exceptions;
using m01_labMedicine.DTO.Pessoa.Medico;
using m01_labMedicine.Model;
using m01_labMedicine.Services.Medico;
using Microsoft.AspNetCore.Mvc;

namespace m01_labMedicine.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicoController : ControllerBase
    {
        private readonly LabMedicineContext _atendimentoMedicoContext;
        private readonly IMedicoService _medicoService;

        //Construtor com parametro (Injetado)   
        public MedicoController(IMedicoService medicoService) => _medicoService = medicoService;

        /// <summary>
        /// Inclui um m�dico
        /// </summary>
        /// <response code="201">Criado com sucesso, retornando as informa��es do cadastro.</response>
        /// <response code="400">Erro na requisi��o devido preenchimento incorreto dos campos esperados.</response>
        /// <response code="409">M�dico j� cadastrado.</response>
        [HttpPost("/api/medicos/")]
        [ProducesResponseType(typeof(MedicoResponseDTO), StatusCodes.Status201Created)]
        public ActionResult<MedicoResponseDTO> Post([FromBody] MedicoRequestDTO medicoDTO)
        {
            try
            {
                MedicoResponseDTO medicoResponseDTO = _medicoService.Insere(medicoDTO);
                return Created("", medicoResponseDTO);
            }
            catch (MyException ex)
            {
                return StatusCode(ex.ErrorCode, ex.Message);
            }
            catch (Exception)
            {
                return BadRequest($"Ocorreu um erro na requisi��o! Tente novamente mais tarde.");
            }
        }

        /// <summary>
        /// Altera um m�dico
        /// </summary>
        /// <response code="200">Alterado com sucesso, retornando as informa��es do cadastro.</response>
        /// <response code="400">Erro na requisi��o devido preenchimento incorreto dos campos esperados.</response>
        /// <response code="404">M�dico n�o encontrado para o identificador informado.</response>
        [HttpPut("/api/medicos/{identificador}")]
        [ProducesResponseType(typeof(MedicoResponseDTO), StatusCodes.Status200OK)]
        public ActionResult<MedicoResponseDTO> Put([FromRoute] int identificador, MedicoUpdateDTO medicoUpdateDTO)
        {
            try
            {
                MedicoResponseDTO medicoResponseDTO = _medicoService.Atualiza(identificador, medicoUpdateDTO);
                return Ok(medicoResponseDTO);
            }
            catch (MyException ex)
            {
                return StatusCode(ex.ErrorCode, ex.Message);
            }
            catch (Exception)
            {
                return BadRequest($"Ocorreu um erro na requisi��o! Tente novamente mais tarde.");
            }
        }

        /// <summary>
        /// Lista os m�dicos (status opcional)
        /// </summary>
        /// <response code="200">Retorna a lista com as informa��es do cadastro.</response>
        /// <response code="400">Erro na requisi��o devido preenchimento incorreto dos campos esperados.</response>
        /// <response code="404">Nenhum m�dico encontrado.</response>
        [HttpGet("/api/medicos")]
        [ProducesResponseType(typeof(List<MedicoResponseDTO>), StatusCodes.Status200OK)]
        public ActionResult<List<MedicoResponseDTO>> Get(string status = "")
        {
            try
            {
                List<MedicoResponseDTO> medicosResponseDTO = _medicoService.BuscaMedicos(status);
                return Ok(medicosResponseDTO);
            }
            catch (MyException ex)
            {
                return StatusCode(ex.ErrorCode, ex.Message);
            }
            catch (Exception)
            {
                return BadRequest($"Ocorreu um erro na requisi��o! Tente novamente mais tarde.");
            }
        }

        /// <summary>
        /// Busca um m�dico
        /// </summary>
        /// <response code="200">Retorna as informa��es do cadastro.</response>
        /// <response code="400">Erro na requisi��o devido preenchimento incorreto dos campos esperados.</response>
        /// <response code="404">M�dico n�o encontrado para o identificador fornecido.</response>
        [HttpGet("/api/medicos/{identificador}")]
        [ProducesResponseType(typeof(MedicoResponseDTO), StatusCodes.Status200OK)]
        public ActionResult<MedicoResponseDTO> GetPorId([FromRoute] int identificador)
        {
            try
            {
                MedicoResponseDTO medicoResponseDTO = _medicoService.BuscaMedico(identificador);
                return Ok(medicoResponseDTO);
            }
            catch (MyException ex)
            {
                return StatusCode(ex.ErrorCode, ex.Message);
            }
            catch (Exception)
            {
                return BadRequest($"Ocorreu um erro na requisi��o! Tente novamente mais tarde.");
            }
        }

        /// <summary>
        /// Remove um m�dico
        /// </summary>
        /// <response code="204">M�dico removido com sucesso.</response>
        /// <response code="400">Erro na requisi��o devido preenchimento incorreto dos campos esperados.</response>
        /// <response code="404">M�dico n�o encontrado para o identificador fornecido.</response>
        [HttpDelete("/api/medicos/{identificador}")]
        public ActionResult Delete([FromRoute] int identificador)
        {
            try
            {
                _medicoService.Remove(identificador);
                return NoContent();
            }
            catch (MyException ex)
            {
                return StatusCode(ex.ErrorCode, ex.Message);
            }
            catch (Exception)
            {
                return BadRequest($"Ocorreu um erro na requisi��o! Tente novamente mais tarde.");
            }
        }
    }
}