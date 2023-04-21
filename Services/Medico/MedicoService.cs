using AutoMapper;
using m01_labMedicine.Core.Exceptions;
using m01_labMedicine.DTO.Pessoa.Medico;
using m01_labMedicine.Model;

namespace m01_labMedicine.Services.Medico
{
    public class MedicoService : IMedicoService
    {
        private readonly LabMedicineContext _labMedicineContext;
        private readonly IMapper _mapper;

        public MedicoService(IMapper mapper, LabMedicineContext labMedicineContext) => (_mapper, _labMedicineContext) = (mapper, labMedicineContext);

        public MedicoResponseDTO Insere(MedicoRequestDTO medicoDTO)
        {
            try
            {
                var medicoModel = _mapper.Map<MedicoModel>(medicoDTO);

                //Verificar se existe o Medico no banco de dados
                var MedicoModelDb = _labMedicineContext.Medico.Where(x => x.CPF == medicoDTO.CPF).FirstOrDefault();
                if (MedicoModelDb != null)
                    throw new MyException(409, $"O CPF informado já está cadastrado para um médico no sistema!");

                //Add na lista do DBSet Medico
                _labMedicineContext.Medico.Add(medicoModel);

                //Salvar no banco de dados
                _labMedicineContext.SaveChanges();

                return _mapper.Map<MedicoResponseDTO>(medicoModel);

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
                var medicoModel = _labMedicineContext.Medico.Where(x => x.Id == identificador).FirstOrDefault();

                if (medicoModel != null)
                {
                    if(medicoUpdateDTO.Nome != null)
                        medicoModel.NomeCompleto = medicoUpdateDTO.Nome;
                    if (medicoUpdateDTO.Genero != null)
                        medicoModel.Genero = medicoUpdateDTO.Genero;
                    medicoModel.DataNascimento = medicoUpdateDTO.DataNascimento;
                    if (medicoUpdateDTO.Telefone != null)
                        medicoModel.Telefone = medicoUpdateDTO.Telefone;
                    if (medicoUpdateDTO.InstituicaoEnsino != null)
                        medicoModel.InstituicaoEnsinoFormacao = medicoUpdateDTO.InstituicaoEnsino;
                    if (medicoUpdateDTO.Nome != null)
                        medicoModel.CrmUF = medicoUpdateDTO.CRMUF;
                    if (medicoUpdateDTO.EspecializacaoClinica != null)
                        medicoModel.EspecializacaoClinica = medicoUpdateDTO.EspecializacaoClinica;
                    medicoModel.EstadoSistema = medicoUpdateDTO.SituacaoSistema;
                    
                    medicoModel.TotalAtendimentosRealizados = medicoUpdateDTO.TotalAtendimentos;

                    //Add na lista do DBSet Medico
                    _labMedicineContext.Medico.Attach(medicoModel);

                    //Salvar no banco de dados
                    _labMedicineContext.SaveChanges();

                    return _mapper.Map<MedicoResponseDTO>(medicoModel);
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
                var medicoModel = _labMedicineContext.Medico.Find(identificador);

                if (medicoModel != null)
                {
                    _labMedicineContext.Medico.Remove(medicoModel);
                    _labMedicineContext.SaveChanges();
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

                    medicosInnerJoin = _labMedicineContext.Medico.Where(x => x.EstadoSistema == status);
                }
                else
                    medicosInnerJoin = _labMedicineContext.Medico;

                if (medicosInnerJoin.Any())
                {
                    foreach (var medico in medicosInnerJoin)                    
                        lista.Add(_mapper.Map<MedicoResponseDTO>(medico));
                    
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
                MedicoModel medicoInnerJoin = _labMedicineContext.Medico.Where(w => w.Id == identificador).FirstOrDefault() ?? throw new MyException(404, "Médico não encontrado para o identificador informado.");                
                return _mapper.Map<MedicoResponseDTO>(medicoInnerJoin);
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