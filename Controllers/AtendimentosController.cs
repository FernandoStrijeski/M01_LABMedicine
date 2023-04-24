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
        /// <response code="201">Criado com sucesso, retornando as informações do médico, paciente e descrição do atendimento realizado.</response>
        /// <response code="400">Erro na requisição devido preenchimento incorreto dos campos esperados.</response>
        /// <response code="404">Médico ou paciente não encontrados.</response>
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
                return BadRequest($"Ocorreu um erro na requisição! Tente novamente mais tarde.");
            }
        }
        
        /// <summary>
        /// Retorna o histórico dos atendimentos
        /// </summary>
        /// <response code="200">Retorna uma lista contendo as informações dos médicos, pacientes e descrições dos atendimentos realizados.</response>
        /// <response code="400">Erro na requisição devido preenchimento incorreto dos campos esperados.</response>
        /// <response code="404">Nenhum histórico encontrado.</response>
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
                return BadRequest($"Ocorreu um erro na requisição! Tente novamente mais tarde.");
            }
        }
    }
}