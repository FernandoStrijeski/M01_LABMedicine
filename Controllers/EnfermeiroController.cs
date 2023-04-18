using m01_labMedicine.DTO.Pessoa.Enfermeiro;
using m01_labMedicine.DTO.Pessoa.Medico;
using m01_labMedicine.Model;
using Microsoft.AspNetCore.Mvc;

namespace m01_labMedicine.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnfermeiroController : ControllerBase
    {
        private readonly AtendimentoMedicoContext atendimentoMedicoContext;

        //Construtor com parametro (Injetado)   
        public EnfermeiroController(AtendimentoMedicoContext atendimentoMedicoContext) => this.atendimentoMedicoContext = atendimentoMedicoContext;

        [HttpPost("/api/enfermeiros/")]
        public ActionResult<EnfermeiroResponseDTO> Post([FromBody] EnfermeiroRequestDTO enfermeiroRequestDTO)
        {
            try
            {
                EnfermeiroModel enfermeiroModel = new()
                {
                    NomeCompleto = enfermeiroRequestDTO.Nome,
                    Genero = enfermeiroRequestDTO.Genero,
                    DataNascimento = enfermeiroRequestDTO.DataNascimento,
                    CPF = enfermeiroRequestDTO.CPF,
                    Telefone = enfermeiroRequestDTO.Telefone,
                    InstituicaoEnsinoFormacao = enfermeiroRequestDTO.InstituicaoEnsino,
                    CofenUF = enfermeiroRequestDTO.CofenUF
                };

                //Verificar se existe o Enfermeiro no banco de dados
                var EnfermeiroModelDb = atendimentoMedicoContext.Enfermeiro.Where(x => x.CPF == enfermeiroRequestDTO.CPF).FirstOrDefault();
                if (EnfermeiroModelDb != null)
                    return Conflict($"Enfermeiro com o CPF informado já cadastrado [{EnfermeiroModelDb.NomeCompleto}]!");

                //Add na lista do DBSet Enfermeiro
                atendimentoMedicoContext.Enfermeiro.Add(enfermeiroModel);

                //Salvar no banco de dados
                atendimentoMedicoContext.SaveChanges();

                EnfermeiroResponseDTO EnfermeiroResponseDTO = new()
                {
                    Codigo = enfermeiroModel.Id,
                    Nome = enfermeiroModel.NomeCompleto,
                    Genero = enfermeiroModel.Genero,
                    DataNascimento = enfermeiroModel.DataNascimento,
                    CPF = enfermeiroModel.CPF,
                    Telefone = enfermeiroModel.Telefone,
                    InstituicaoEnsino = enfermeiroRequestDTO.InstituicaoEnsino,
                    CofenUF = enfermeiroRequestDTO.CofenUF //,
                    //atendimentos = enfermeiroModel.TotalAtendimentosRealizados
                };

                return Created("", EnfermeiroResponseDTO);

            }
            catch (Exception)
            {
                return BadRequest("Dados inválidos!");
            }
        }

        [HttpPut("/api/enfermeiros/{identificador}")]
        public ActionResult<EnfermeiroResponseDTO> Put([FromRoute] int identificador, EnfermeiroUpdateDTO enfermeiroUpdateDTO)
        {
            try
            {
                //Verificar se existe o Enfermeiro no banco de dados
                var enfermeiroModel = atendimentoMedicoContext.Enfermeiro.Where(x => x.Id == identificador).FirstOrDefault();

                if (enfermeiroModel != null)
                {
                    enfermeiroModel.NomeCompleto = enfermeiroUpdateDTO.Nome;
                    enfermeiroModel.Genero = enfermeiroUpdateDTO.Genero;
                    enfermeiroModel.DataNascimento = enfermeiroUpdateDTO.DataNascimento;
                    enfermeiroModel.Telefone = enfermeiroUpdateDTO.Telefone;
                    enfermeiroModel.InstituicaoEnsinoFormacao = enfermeiroUpdateDTO.InstituicaoEnsino;
                    enfermeiroModel.CofenUF = enfermeiroUpdateDTO.CofenUF;

                    //Add na lista do DBSet Enfermeiro
                    atendimentoMedicoContext.Enfermeiro.Attach(enfermeiroModel);

                    //Salvar no banco de dados
                    atendimentoMedicoContext.SaveChanges();

                    EnfermeiroResponseDTO enfermeiroResponseDTO = new()
                    {
                        Codigo = enfermeiroModel.Id,
                        Nome = enfermeiroModel.NomeCompleto,
                        Genero = enfermeiroModel.Genero,
                        DataNascimento = enfermeiroModel.DataNascimento,
                        CPF = enfermeiroModel.CPF,
                        Telefone = enfermeiroModel.Telefone,
                        InstituicaoEnsino = enfermeiroModel.InstituicaoEnsinoFormacao,
                        CofenUF = enfermeiroModel.CofenUF
                    };

                    return Ok(enfermeiroResponseDTO);
                }
                else
                    return NotFound("Enfermeiro não encontrado com o identificador informado.");

            }
            catch (Exception)
            {
                return BadRequest("Dados inválidos!");
            }
        }

        //Devolve todos os registros ou pelo status opcional
        [HttpGet("/api/enfermeiros")]
        public ActionResult<List<EnfermeiroResponseDTO>> Get()
        {
            List<EnfermeiroResponseDTO> lista = new();
            IQueryable<EnfermeiroModel> enfermeirosInnerJoin;

            enfermeirosInnerJoin = atendimentoMedicoContext.Enfermeiro;

            if (enfermeirosInnerJoin.Count() > 0)
            {
                foreach (var enfermeiro in enfermeirosInnerJoin)
                {
                    EnfermeiroResponseDTO enfermeiroGet = new()
                    {
                        Codigo = enfermeiro.Id,
                        Nome = enfermeiro.NomeCompleto,
                        Genero = enfermeiro.Genero,
                        DataNascimento = enfermeiro.DataNascimento,
                        CPF = enfermeiro.CPF,
                        Telefone = enfermeiro.Telefone,
                        InstituicaoEnsino = enfermeiro.InstituicaoEnsinoFormacao,
                        CofenUF = enfermeiro.CofenUF
                    };

                    lista.Add(enfermeiroGet);
                }

                return Ok(lista);
            }
            else                
                return NotFound("Nenhum enfermeiro cadastrado.");
        }

        //Devolve por id
        [HttpGet("/api/enfermeiros/{identificador}")]
        public ActionResult<EnfermeiroResponseDTO> GetPorId([FromRoute] int identificador)
        {
            EnfermeiroModel enfermeiroInnerJoin = atendimentoMedicoContext.Enfermeiro.Where(w => w.Id == identificador).FirstOrDefault();
            if (enfermeiroInnerJoin == null)
                return NotFound("Enfermeiro não encontrado para o identificador informado.");

            EnfermeiroResponseDTO enfermeiroResponseDTO = new()
            {
                Codigo = enfermeiroInnerJoin.Id,
                Nome = enfermeiroInnerJoin.NomeCompleto,
                Genero = enfermeiroInnerJoin.Genero,
                DataNascimento = enfermeiroInnerJoin.DataNascimento,
                CPF = enfermeiroInnerJoin.CPF,
                Telefone = enfermeiroInnerJoin.Telefone,
                InstituicaoEnsino = enfermeiroInnerJoin.InstituicaoEnsinoFormacao,
                CofenUF = enfermeiroInnerJoin.CofenUF
            };

            return Ok(enfermeiroInnerJoin);
        }

        [HttpDelete("/api/enfermeiros/{identificador}")]
        public ActionResult Delete([FromRoute] int identificador)
        {
            //Verificar se existe no banco de dados
            var enfermeiroModel = atendimentoMedicoContext.Enfermeiro.Find(identificador);

            if (enfermeiroModel != null)
            {
                atendimentoMedicoContext.Enfermeiro.Remove(enfermeiroModel);
                atendimentoMedicoContext.SaveChanges();

                return NoContent();
            }
            else
                return NotFound("Enfermeiro não encontrado com o identificador informado.");
        }
    }
}