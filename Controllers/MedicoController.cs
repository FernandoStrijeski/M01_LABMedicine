using m01_labMedicine.DTO.Pessoa.Medico;
using m01_labMedicine.DTO.Pessoa.Paciente;
using m01_labMedicine.Extension;
using m01_labMedicine.Model;
using m01_labMedicine.Services;
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
        

        [HttpPost("/api/medicos/")]
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
                return BadRequest($"Ocorreu um erro na requisição! Tente novamente mais tarde.");
            }
        }

        [HttpPut("/api/medicos/{identificador}")]
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
                return BadRequest($"Ocorreu um erro na requisição! Tente novamente mais tarde.");
            }
        }

        //Devolve todos os registros ou pelo status opcional
        [HttpGet("/api/medicos")]
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
                return BadRequest($"Ocorreu um erro na requisição! Tente novamente mais tarde.");
            }
        }

        //Devolve por id
        [HttpGet("/api/medicos/{identificador}")]
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
                return BadRequest($"Ocorreu um erro na requisição! Tente novamente mais tarde.");
            }
        }

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
                return BadRequest($"Ocorreu um erro na requisição! Tente novamente mais tarde.");
            }
        }
    }
}