using m01_labMedicine.DTO.Pessoa.Medico;
using m01_labMedicine.DTO.Pessoa.Paciente;
using m01_labMedicine.Extension;
using m01_labMedicine.Model;
using System.Text.RegularExpressions;

namespace m01_labMedicine.Services
{
    public class MedicoService : IMedicoService
    {
        private readonly LabMedicineContext _atendimentoMedicoContext;

        public MedicoService(LabMedicineContext atendimentoMedicoContext) => _atendimentoMedicoContext = atendimentoMedicoContext;

        public MedicoResponseDTO Insere(MedicoRequestDTO medicoDTO)
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
                var MedicoModelDb = _atendimentoMedicoContext.Medico.Where(x => x.CPF == medicoDTO.CPF).FirstOrDefault();
                if (MedicoModelDb != null)
                    throw new MyException(409, $"O CPF informado já está cadastrado para um médico no sistema!");

                //Add na lista do DBSet Medico
                _atendimentoMedicoContext.Medico.Add(medicoModel);

                //Salvar no banco de dados
                _atendimentoMedicoContext.SaveChanges();

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
                    Atendimentos = medicoModel.TotalAtendimentosRealizados
                };

                return medicoResponseDTO;

            }
            catch (MyException ex)
            {
                throw ex;
            }

            catch (Exception)
            {
                throw new MyException(400, "Dados inválidos!");
            }
        }

        public MedicoResponseDTO Atualiza(int identificador, MedicoUpdateDTO medicoUpdateDTO)
        {
            try
            {
                //Verificar se existe o medico no banco de dados
                var medicoModel = _atendimentoMedicoContext.Medico.Where(x => x.Id == identificador).FirstOrDefault();

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
                    _atendimentoMedicoContext.Medico.Attach(medicoModel);

                    //Salvar no banco de dados
                    _atendimentoMedicoContext.SaveChanges();

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
                        Atendimentos = medicoModel.TotalAtendimentosRealizados
                    };

                    return medicoResponseDTO;
                }
                else
                    throw new MyException(404, "Médico não encontrado com o identificador informado.");

            }
            catch (MyException ex)
            {
                throw ex;
            }

            catch (Exception)
            {
                throw new MyException(400, "Dados inválidos!");
            }
        }

        public void Remove(int identificador)
        {
            try
            {
                //Verificar se existe no banco de dados
                var medicoModel = _atendimentoMedicoContext.Medico.Find(identificador);

                if (medicoModel != null)
                {
                    _atendimentoMedicoContext.Medico.Remove(medicoModel);
                    _atendimentoMedicoContext.SaveChanges();
                }
                else
                    throw new MyException(404, "Médico não encontrado com o identificador informado.");

            }
            catch (MyException ex)
            {
                throw ex;
            }

            catch (Exception)
            {
                throw new MyException(400, "Dados inválidos!");
            }
        }


        public List<MedicoResponseDTO> BuscaMedicos(string status)
        {
            try
            {
                List<MedicoResponseDTO> lista = new();
                IQueryable<MedicoModel> medicosInnerJoin;

                if (status != "")
                {
                    List<string> lstStatus = new(new string[] { "ATIVO", "INATIVO" });
                    if (!lstStatus.Contains(status.ToUpper()))
                        throw new MyException(404, "O status informado não existe.");

                    medicosInnerJoin = _atendimentoMedicoContext.Medico.Where(x => x.EstadoSistema == status);
                }
                else
                    medicosInnerJoin = _atendimentoMedicoContext.Medico;

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
                            Atendimentos = medico.TotalAtendimentosRealizados
                        };

                        lista.Add(medicoGet);
                    }

                    return lista;
                }
                else
                {
                    if (status != "")
                        throw new MyException(404, "Nenhum médico encontrado para o status informado.");
                    else
                        throw new MyException(404, "Nenhum médico cadastrado.");
                }

            }
            catch (MyException ex)
            {
                throw ex;
            }

            catch (Exception)
            {
                throw new MyException(400, "Dados inválidos!");
            }
        }

        public MedicoResponseDTO BuscaMedico(int identificador)
        {
            try
            {
                MedicoModel medicoInnerJoin = _atendimentoMedicoContext.Medico.Where(w => w.Id == identificador).FirstOrDefault();
                if (medicoInnerJoin == null)
                    throw new MyException(404, "Médico não encontrado para o identificador informado.");

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
                    Atendimentos = medicoInnerJoin.TotalAtendimentosRealizados
                };

                return medicoResponseDTO;

            }
            catch (MyException ex)
            {
                throw ex;
            }

            catch (Exception)
            {
                throw new MyException(400, "Dados inválidos!");
            }
        }

    }
}
