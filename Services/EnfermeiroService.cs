using m01_labMedicine.DTO.Pessoa.Enfermeiro;
using m01_labMedicine.DTO.Pessoa.Medico;
using m01_labMedicine.DTO.Pessoa.Paciente;
using m01_labMedicine.Extension;
using m01_labMedicine.Model;
using System.Text.RegularExpressions;

namespace m01_labMedicine.Services
{
    public class EnfermeiroService : IEnfermeiroService
    {
        private readonly LabMedicineContext _atendimentoMedicoContext;

        public EnfermeiroService(LabMedicineContext atendimentoMedicoContext) => _atendimentoMedicoContext = atendimentoMedicoContext;

        public EnfermeiroResponseDTO Insere(EnfermeiroRequestDTO enfermeiroRequestDTO)
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
                var EnfermeiroModelDb = _atendimentoMedicoContext.Enfermeiro.Where(x => x.CPF == enfermeiroRequestDTO.CPF).FirstOrDefault();
                if (EnfermeiroModelDb != null)
                    throw new MyException(409, $"Enfermeiro com o CPF informado já cadastrado [{EnfermeiroModelDb.NomeCompleto}]!");

                //Add na lista do DBSet Enfermeiro
                _atendimentoMedicoContext.Enfermeiro.Add(enfermeiroModel);

                //Salvar no banco de dados
                _atendimentoMedicoContext.SaveChanges();

                EnfermeiroResponseDTO enfermeiroResponseDTO = new()
                {
                    Codigo = enfermeiroModel.Id,
                    Nome = enfermeiroModel.NomeCompleto,
                    Genero = enfermeiroModel.Genero,
                    DataNascimento = enfermeiroModel.DataNascimento,
                    CPF = enfermeiroModel.CPF,
                    Telefone = enfermeiroModel.Telefone,
                    InstituicaoEnsino = enfermeiroRequestDTO.InstituicaoEnsino,
                    CofenUF = enfermeiroRequestDTO.CofenUF
                };

                return enfermeiroResponseDTO;

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

        public EnfermeiroResponseDTO Atualiza(int identificador, EnfermeiroUpdateDTO enfermeiroUpdateDTO)
        {
            try
            {
                //Verificar se existe o Enfermeiro no banco de dados
                var enfermeiroModel = _atendimentoMedicoContext.Enfermeiro.Where(x => x.Id == identificador).FirstOrDefault();

                if (enfermeiroModel != null)
                {
                    enfermeiroModel.NomeCompleto = enfermeiroUpdateDTO.Nome;
                    enfermeiroModel.Genero = enfermeiroUpdateDTO.Genero;
                    enfermeiroModel.DataNascimento = enfermeiroUpdateDTO.DataNascimento;
                    enfermeiroModel.Telefone = enfermeiroUpdateDTO.Telefone;
                    enfermeiroModel.InstituicaoEnsinoFormacao = enfermeiroUpdateDTO.InstituicaoEnsino;
                    enfermeiroModel.CofenUF = enfermeiroUpdateDTO.CofenUF;

                    //Add na lista do DBSet Enfermeiro
                    _atendimentoMedicoContext.Enfermeiro.Attach(enfermeiroModel);

                    //Salvar no banco de dados
                    _atendimentoMedicoContext.SaveChanges();

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

                    return enfermeiroResponseDTO;
                }
                else
                    throw new MyException(404, "Enfermeiro não encontrado com o identificador informado.");

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
                var enfermeiroModel = _atendimentoMedicoContext.Enfermeiro.Find(identificador);

                if (enfermeiroModel != null)
                {
                    _atendimentoMedicoContext.Enfermeiro.Remove(enfermeiroModel);
                    _atendimentoMedicoContext.SaveChanges();
                }
                else
                    throw new MyException(404, "Enfermeiro não encontrado com o identificador informado.");

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


        public List<EnfermeiroResponseDTO> BuscaEnfermeiros()
        {
            try
            {
                List<EnfermeiroResponseDTO> lista = new();
                IQueryable<EnfermeiroModel> enfermeirosInnerJoin;

                enfermeirosInnerJoin = _atendimentoMedicoContext.Enfermeiro;

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

                    return lista;
                }
                else
                    throw new MyException(404, "Nenhum enfermeiro cadastrado.");
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

        public EnfermeiroResponseDTO BuscaEnfermeiro(int identificador)
        {
            try
            {
                EnfermeiroModel enfermeiroInnerJoin = _atendimentoMedicoContext.Enfermeiro.Where(w => w.Id == identificador).FirstOrDefault();
                if (enfermeiroInnerJoin == null)
                    throw new MyException(404, "Enfermeiro não encontrado para o identificador informado.");

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

                return enfermeiroResponseDTO;

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
