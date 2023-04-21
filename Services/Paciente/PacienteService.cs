using AutoMapper;
using m01_labMedicine.Core.Exceptions;
using m01_labMedicine.DTO.Pessoa.Paciente;
using m01_labMedicine.Model;

namespace m01_labMedicine.Services.Paciente
{
    public class PacienteService : IPacienteService
    {
        private readonly LabMedicineContext _labMedicineContext;
        private readonly IMapper _mapper;

        public PacienteService(IMapper mapper, LabMedicineContext labMedicineContext) => (_mapper, _labMedicineContext) = (mapper, labMedicineContext);

        public PacienteResponseDTO Insere(PacienteRequestDTO pacienteDTO)
        {
            try
            {
                var pacienteModel = _mapper.Map<PacienteModel>(pacienteDTO);

                //Verificar se existe o Paciente no banco de dados
                var pacienteModelDb = _labMedicineContext.Paciente.Where(x => x.CPF == pacienteDTO.CPF).FirstOrDefault();
                if (pacienteModelDb != null)
                    throw new MyException(409, $"O CPF informado já está cadastrado no sistema!");

                //Add na lista do DBSet Paciente
                _labMedicineContext.Paciente.Add(pacienteModel);

                //Salvar no banco de dados
                _labMedicineContext.SaveChanges();

                return _mapper.Map<PacienteResponseDTO>(pacienteModel);

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

        public PacienteResponseDTO Atualiza(int identificador, PacienteUpdateDTO pacienteUpdateDTO)
        {
            try
            {
                //Verificar se existe o paciente no banco de dados
                var pacienteModel = _labMedicineContext.Paciente.Where(x => x.Id == identificador).FirstOrDefault() ?? throw new MyException(404, "Paciente não encontrado com o identificador informado.");

                if (pacienteUpdateDTO.Nome != null)
                    pacienteModel.NomeCompleto = pacienteUpdateDTO.Nome;
                if (pacienteUpdateDTO.Genero != null)
                    pacienteModel.Genero = pacienteUpdateDTO.Genero;
                pacienteModel.DataNascimento = pacienteUpdateDTO.DataNascimento;
                if (pacienteUpdateDTO.Telefone != null)
                    pacienteModel.Telefone = pacienteUpdateDTO.Telefone;
                if (pacienteUpdateDTO.ContatoEmergencia != null)
                    pacienteModel.ContatoEmergencia = pacienteUpdateDTO.ContatoEmergencia;
                if (pacienteUpdateDTO.Alergias != null)
                    pacienteModel.Alergias = pacienteUpdateDTO.Alergias;
                if (pacienteUpdateDTO.CuidadosEspecificos != null)
                    pacienteModel.CuidadosEspecificos = pacienteUpdateDTO.CuidadosEspecificos;
                if (pacienteUpdateDTO.Convenio != null)
                    pacienteModel.Convenio = pacienteUpdateDTO.Convenio;

                pacienteModel.StatusAtendimento = pacienteUpdateDTO.StatusAtendimento;

                //Add na lista do DBSet Paciente
                _labMedicineContext.Paciente.Attach(pacienteModel);

                //Salvar no banco de dados
                _labMedicineContext.SaveChanges();

                return _mapper.Map<PacienteResponseDTO>(pacienteModel);


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
                var pacienteModel = _labMedicineContext.Paciente.Find(identificador);

                if (pacienteModel != null)
                {
                    _labMedicineContext.Paciente.Remove(pacienteModel);
                    _labMedicineContext.SaveChanges();
                }
                else
                    throw new MyException(404, "Paciente não encontrado com o identificador informado.");

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


        public List<PacienteResponseDTO> BuscaPacientes(PacienteStatusRequestDTO status)
        {
            try
            {
                List<PacienteResponseDTO> lista = new();
                IQueryable<PacienteModel> pacientesInnerJoin;

                if (status.StatusAtendimento != null)
                    pacientesInnerJoin = _labMedicineContext.Paciente.Where(x => x.StatusAtendimento == status.StatusAtendimento);
                else
                    pacientesInnerJoin = _labMedicineContext.Paciente;

                if (pacientesInnerJoin.Any())
                {
                    foreach (var paciente in pacientesInnerJoin)
                        lista.Add(_mapper.Map<PacienteResponseDTO>(paciente));

                    return lista;
                }
                else
                {
                    if (status.StatusAtendimento != "")
                        throw new MyException(404, "Nenhum paciente encontrado para o status informado.");
                    else
                        throw new MyException(404, "Nenhum paciente cadastrado.");
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

        public PacienteResponseDTO BuscaPaciente(int identificador)
        {
            try
            {
                PacienteModel pacienteInnerJoin = _labMedicineContext.Paciente.Where(w => w.Id == identificador).FirstOrDefault() ?? throw new MyException(404, "Paciente não encontrado para o identificador informado.");
                return _mapper.Map<PacienteResponseDTO>(pacienteInnerJoin);

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
