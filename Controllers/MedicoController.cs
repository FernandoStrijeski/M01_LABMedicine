using m01_labMedicine.DTO.Pessoa.Medico;
using m01_labMedicine.Model;
using Microsoft.AspNetCore.Mvc;

namespace m01_labMedicine.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicoController : ControllerBase
    {
        private readonly AtendimentoMedicoContext atendimentoMedicoContext;

        //Construtor com parametro (Injetado)   
        public MedicoController(AtendimentoMedicoContext atendimentoMedicoContext) => this.atendimentoMedicoContext = atendimentoMedicoContext;

        [HttpPost("/api/medicos/")]
        public ActionResult<MedicoResponseDTO> Post([FromBody] MedicoRequestDTO medicoDTO)
        {
            try
            {
                MedicoModel medicoModel = new()
                {
                    NomeCompleto = medicoDTO.Nome,
                    Genero = medicoDTO.Genero,
                    DataNascimento = medicoDTO.DataNascimento,
                    CPF = medicoDTO.CPF,
                    Telefone = medicoDTO.Telefone,
                    InstituicaoEnsinoFormacao = medicoDTO.InstituicaoEnsino,
                    CrmUF = medicoDTO.CRMUF,
                    EspecializacaoClinica = medicoDTO.EspecializacaoClinica,
                    EstadoSistema = medicoDTO.SituacaoSistema,
                    TotalAtendimentosRealizados = medicoDTO.TotalAtendimentos
                };

                //Verificar se existe o Medico no banco de dados
                var MedicoModelDb = atendimentoMedicoContext.Medico.Where(x => x.CPF == medicoDTO.CPF).FirstOrDefault();
                if (MedicoModelDb != null)
                    return Conflict($"Médico com o CPF informado já cadastrado [{MedicoModelDb.NomeCompleto}]!");

                //Add na lista do DBSet Medico
                atendimentoMedicoContext.Medico.Add(medicoModel);

                //Salvar no banco de dados
                atendimentoMedicoContext.SaveChanges();

                MedicoResponseDTO MedicoResponseDTO = new()
                {
                    Codigo = medicoModel.Id,
                    Nome = medicoModel.NomeCompleto,
                    Genero = medicoModel.Genero,
                    DataNascimento = medicoModel.DataNascimento,
                    CPF = medicoModel.CPF,
                    Telefone = medicoModel.Telefone,
                    InstituicaoEnsinoFormacao = medicoModel.InstituicaoEnsinoFormacao,
                    CrmUF = medicoModel.CrmUF,
                    EspecializacaoClinica = medicoModel.EspecializacaoClinica,
                    EstadoSistema = medicoModel.EstadoSistema,
                    atendimentos = medicoModel.TotalAtendimentosRealizados
                };

                return Created("", MedicoResponseDTO);

            }
            catch (Exception)
            {
                return BadRequest("Dados inválidos!");
            }
        }

        [HttpPut("/api/medicos/{identificador}")]
        public ActionResult<MedicoResponseDTO> Put([FromRoute] int identificador, MedicoRequestDTO medicoUpdateDTO)
        {
            try
            {
                //Verificar se existe o medico no banco de dados
                var medicoModel = atendimentoMedicoContext.Medico.Where(x => x.Id == identificador).FirstOrDefault();

                if (medicoModel != null)
                {
                    medicoModel.NomeCompleto = medicoUpdateDTO.Nome;
                    medicoModel.Genero = medicoUpdateDTO.Genero;
                    medicoModel.DataNascimento = medicoUpdateDTO.DataNascimento;
                    medicoModel.Telefone = medicoUpdateDTO.Telefone;
                    medicoModel.InstituicaoEnsinoFormacao = medicoUpdateDTO.InstituicaoEnsino;
                    medicoModel.CrmUF = medicoUpdateDTO.CRMUF;
                    medicoModel.EspecializacaoClinica = medicoUpdateDTO.EspecializacaoClinica;
                    medicoModel.EstadoSistema = medicoUpdateDTO.SituacaoSistema;
                    medicoModel.TotalAtendimentosRealizados = medicoUpdateDTO.TotalAtendimentos;

                    //Add na lista do DBSet Medico
                    atendimentoMedicoContext.Medico.Attach(medicoModel);

                    //Salvar no banco de dados
                    atendimentoMedicoContext.SaveChanges();

                    MedicoResponseDTO medicoResponseDTO = new()
                    {
                        Codigo = medicoModel.Id,
                        Nome = medicoModel.NomeCompleto,
                        Genero = medicoModel.Genero,
                        DataNascimento = medicoModel.DataNascimento,
                        CPF = medicoModel.CPF,
                        Telefone = medicoModel.Telefone,
                        InstituicaoEnsinoFormacao = medicoModel.InstituicaoEnsinoFormacao,
                        CrmUF = medicoModel.CrmUF,
                        EspecializacaoClinica = medicoModel.EspecializacaoClinica,
                        EstadoSistema = medicoModel.EstadoSistema,
                        atendimentos = medicoModel.TotalAtendimentosRealizados
                    };

                    return Ok(medicoResponseDTO);
                }
                else
                    return NotFound("Médico não encontrado com o identificador informado.");

            }
            catch (Exception)
            {
                return BadRequest("Dados inválidos!");
            }
        }

        //Devolve todos os registros ou pelo status opcional
        [HttpGet("/api/medicos")]
        public ActionResult<List<MedicoResponseDTO>> Get(string status = "")
        {
            List<MedicoResponseDTO> lista = new();
            IQueryable<MedicoModel> medicosInnerJoin;

            if (status != "")
            {
                List<string> lstStatus = new(new string[] { "ATIVO", "INATIVO" });
                if (!lstStatus.Contains(status.ToUpper()))
                    return BadRequest("O status informado não existe.");

                medicosInnerJoin = atendimentoMedicoContext.Medico.Where(x => x.EstadoSistema == status);
            }
            else
                medicosInnerJoin = atendimentoMedicoContext.Medico;

            if (medicosInnerJoin.Count() > 0)
            {
                foreach (var medico in medicosInnerJoin)
                {
                    MedicoResponseDTO medicoGet = new()
                    {
                        Codigo = medico.Id,
                        Nome = medico.NomeCompleto,
                        Genero = medico.Genero,
                        DataNascimento = medico.DataNascimento,
                        CPF = medico.CPF,
                        Telefone = medico.Telefone,
                        InstituicaoEnsinoFormacao = medico.InstituicaoEnsinoFormacao,
                        CrmUF = medico.CrmUF,
                        EspecializacaoClinica = medico.EspecializacaoClinica,
                        EstadoSistema = medico.EstadoSistema,
                        atendimentos = medico.TotalAtendimentosRealizados
                    };

                    lista.Add(medicoGet);
                }

                return Ok(lista);
            }
            else
            {
                if (status != "")
                    return NotFound("Nenhum médico encontrado para o status informado.");
                else
                    return NotFound("Nenhum médico cadastrado.");
            }
        }

        //Devolve por id
        [HttpGet("/api/medicos/{identificador}")]
        public ActionResult<MedicoResponseDTO> GetPorId([FromRoute] int identificador)
        {
            MedicoModel medicoInnerJoin = atendimentoMedicoContext.Medico.Where(w => w.Id == identificador).FirstOrDefault();
            if (medicoInnerJoin == null)
                return NotFound("Médico não encontrado para o identificador informado.");

            MedicoResponseDTO medicoResponseDTO = new()
            {
                Codigo = medicoInnerJoin.Id,
                Nome = medicoInnerJoin.NomeCompleto,
                Genero = medicoInnerJoin.Genero,
                DataNascimento = medicoInnerJoin.DataNascimento,
                CPF = medicoInnerJoin.CPF,
                Telefone = medicoInnerJoin.Telefone,
                InstituicaoEnsinoFormacao = medicoInnerJoin.InstituicaoEnsinoFormacao,
                CrmUF = medicoInnerJoin.CrmUF,
                EspecializacaoClinica = medicoInnerJoin.EspecializacaoClinica,
                EstadoSistema = medicoInnerJoin.EstadoSistema,
                atendimentos = medicoInnerJoin.TotalAtendimentosRealizados
            };

            return Ok(medicoInnerJoin);
        }

        [HttpDelete("/api/medicos/{identificador}")]
        public ActionResult Delete([FromRoute] int identificador)
        {
            //Verificar se existe no banco de dados
            var medicoModel = atendimentoMedicoContext.Medico.Find(identificador);

            if (medicoModel != null)
            {
                atendimentoMedicoContext.Medico.Remove(medicoModel);
                atendimentoMedicoContext.SaveChanges();

                return NoContent();
            }
            else
                return NotFound("Médico não encontrado com o identificador informado.");
        }
    }
}