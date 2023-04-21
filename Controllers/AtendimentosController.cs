using m01_labMedicine.DTO.Atendimento;
using m01_labMedicine.DTO.Pessoa.Medico;
using m01_labMedicine.DTO.Pessoa.Paciente;
using m01_labMedicine.Extension;
using m01_labMedicine.Model;
using m01_labMedicine.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace m01_labMedicine.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AtendimentosController : ControllerBase
    {
        private readonly IAtendimentoMedicoService _atendimentoMedicoService;

        //Construtor com parametro (Injetado)   
        public AtendimentosController(IAtendimentoMedicoService atendimentoMedicoService) => _atendimentoMedicoService = atendimentoMedicoService;

        [HttpPut("/api/atendimentos")]
        public ActionResult<AtendimentoMedicoResponseDTO> Put([FromBody] AtendimentoMedicoRequestDTO atendimentoMeditoRequestDTO)
        {
            try
            {
                AtendimentoMedicoResponseDTO atendimentoMedicoResponseDTO = _atendimentoMedicoService.Atualiza(atendimentoMeditoRequestDTO);
                return Ok(atendimentoMedicoResponseDTO);
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

        //Devolve todos os registros ou pelo medicoId
        [HttpGet("/api/atendimentos")]
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