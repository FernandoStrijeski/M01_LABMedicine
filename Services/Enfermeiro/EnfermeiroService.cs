using AutoMapper;
using m01_labMedicine.Core.Exceptions;
using m01_labMedicine.DTO.Pessoa.Enfermeiro;
using m01_labMedicine.DTO.Pessoa.Medico;
using m01_labMedicine.Model;

namespace m01_labMedicine.Services.Enfermeiro
{
    public class EnfermeiroService : IEnfermeiroService
    {
        private readonly LabMedicineContext _labMedicineContext;
        private readonly IMapper _mapper;

        public EnfermeiroService(IMapper mapper, LabMedicineContext labMedicineContext) => (_mapper, _labMedicineContext) = (mapper, labMedicineContext);

        public EnfermeiroResponseDTO Insere(EnfermeiroRequestDTO enfermeiroRequestDTO)
        {
            try
            {
                var enfermeiroModel = _mapper.Map<EnfermeiroModel>(enfermeiroRequestDTO);

                //Verificar se existe o Enfermeiro no banco de dados
                var EnfermeiroModelDb = _labMedicineContext.Enfermeiro.Where(x => x.CPF == enfermeiroRequestDTO.CPF).FirstOrDefault();
                if (EnfermeiroModelDb != null)
                    throw new MyException(409, $"Enfermeiro com o CPF informado já cadastrado!");

                //Add na lista do DBSet Enfermeiro
                _labMedicineContext.Enfermeiro.Add(enfermeiroModel);

                //Salvar no banco de dados
                _labMedicineContext.SaveChanges();
                
                return _mapper.Map<EnfermeiroResponseDTO>(enfermeiroModel);
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
                var enfermeiroModel = _labMedicineContext.Enfermeiro.Where(x => x.Id == identificador).FirstOrDefault();

                if (enfermeiroModel != null)
                {
                    if(enfermeiroUpdateDTO.Nome != null)
                        enfermeiroModel.NomeCompleto = enfermeiroUpdateDTO.Nome;
                    if (enfermeiroUpdateDTO.Genero != null)
                        enfermeiroModel.Genero = enfermeiroUpdateDTO.Genero;
                    enfermeiroModel.DataNascimento = enfermeiroUpdateDTO.DataNascimento;
                    if (enfermeiroUpdateDTO.Telefone != null)
                        enfermeiroModel.Telefone = enfermeiroUpdateDTO.Telefone;
                    if (enfermeiroUpdateDTO.InstituicaoEnsino != null)
                        enfermeiroModel.InstituicaoEnsinoFormacao = enfermeiroUpdateDTO.InstituicaoEnsino;
                    if (enfermeiroUpdateDTO.CofenUF != null)
                        enfermeiroModel.CofenUF = enfermeiroUpdateDTO.CofenUF;

                    //Add na lista do DBSet Enfermeiro
                    _labMedicineContext.Enfermeiro.Attach(enfermeiroModel);

                    //Salvar no banco de dados
                    _labMedicineContext.SaveChanges();
                    
                    return _mapper.Map<EnfermeiroResponseDTO>(enfermeiroModel);
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
                var enfermeiroModel = _labMedicineContext.Enfermeiro.Find(identificador);

                if (enfermeiroModel != null)
                {
                    _labMedicineContext.Enfermeiro.Remove(enfermeiroModel);
                    _labMedicineContext.SaveChanges();
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

                enfermeirosInnerJoin = _labMedicineContext.Enfermeiro;

                if (enfermeirosInnerJoin.Any())
                {
                    foreach (var enfermeiro in enfermeirosInnerJoin)
                        lista.Add(_mapper.Map<EnfermeiroResponseDTO>(enfermeiro));
                    
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
                EnfermeiroModel enfermeiroInnerJoin = _labMedicineContext.Enfermeiro.Where(w => w.Id == identificador).FirstOrDefault() ?? throw new MyException(404, "Enfermeiro não encontrado para o identificador informado.");                
                return _mapper.Map<EnfermeiroResponseDTO>(enfermeiroInnerJoin);
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