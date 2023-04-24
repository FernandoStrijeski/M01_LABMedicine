using m01_labMedicine.Core.Exceptions;
using m01_labMedicine.DTO.Atendimento;
using m01_labMedicine.Services.AtendimentoMedico;
using Microsoft.AspNetCore.Mvc;

namespace m01_labMedicine.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    
    public class AtendimentosController : ControllerBase
    {
        private readonly IAtendimentoMedicoService _atendimentoMedicoService;

        //Construtor com parametro (Injetado)   
        public AtendimentosController(IAtendimentoMedicoService atendimentoMedicoService) => _atendimentoMedicoService = atendimentoMedicoService;

        /// <summary>
        /// Inclui um atendimento
        /// </summary>
        /// <response code="201">Criado com sucesso, retornando as informa��es do m�dico, paciente e descri��o do atendimento realizado.</response>
        /// <response code="400">Erro na requisi��o devido preenchimento incorreto dos campos esperados.</response>
        /// <response code="404">M�dico ou paciente n�o encontrados.</response>
        [HttpPut("/atendimentos")]
        [ProducesResponseType(typeof(AtendimentoMedicoResponseDTO), StatusCodes.Status201Created)]
        public ActionResult<AtendimentoMedicoResponseDTO> Put([FromBody] AtendimentoMedicoRequestDTO atendimentoMeditoRequestDTO)
        {
            try
            {
                AtendimentoMedicoResponseDTO atendimentoMedicoResponseDTO = _atendimentoMedicoService.Atualiza(atendimentoMeditoRequestDTO);
                return Created("", atendimentoMedicoResponseDTO);
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
        /// Retorna o hist�rico dos atendimentos
        /// </summary>
        /// <response code="200">Retorna uma lista contendo as informa��es dos m�dicos, pacientes e descri��es dos atendimentos realizados.</response>
        /// <response code="400">Erro na requisi��o devido preenchimento incorreto dos campos esperados.</response>
        /// <response code="404">Nenhum hist�rico encontrado.</response>
        [HttpGet("/atendimentos")]
        [ProducesResponseType(typeof(List<AtendimentoMedicoResponseDTO>), StatusCodes.Status200OK)]
        public ActionResult<List<AtendimentoMedicoResponseDTO>> Get([FromQuery] int medicoId = 0)
        {
            try
            {
                List<AtendimentoMedicoResponseDTO> atendimentosMedicoResponseDTO = _atendimentoMedicoService.BuscaAtendimentos(medicoId);
                return Ok(atendimentosMedicoResponseDTO);
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