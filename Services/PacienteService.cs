using m01_labMedicine.DTO.Pessoa.Paciente;
using m01_labMedicine.Extension;
using m01_labMedicine.Model;
using System.Text.RegularExpressions;

namespace m01_labMedicine.Services
{
    public class PacienteService : IPacienteService
    {
        private readonly LabMedicineContext _atendimentoMedicoContext;

        public PacienteService(LabMedicineContext atendimentoMedicoContext) => _atendimentoMedicoContext = atendimentoMedicoContext;

        public PacienteResponseDTO Insere(PacienteRequestDTO pacienteDTO)
        {
            try
            {
                PacienteModel pacienteModel = new()
                {
                    NomeCompleto = pacienteDTO.Nome,
                    Genero = pacienteDTO.Genero,
                    DataNascimento = pacienteDTO.DataNascimento,
                    CPF = pacienteDTO.CPF,
                    Telefone = pacienteDTO.Telefone,
                    ContatoEmergencia = pacienteDTO.ContatoEmergencia,
                    Alergias = pacienteDTO.Alergias,
                    CuidadosEspecificos = pacienteDTO.CuidadosEspecificos,
                    Convenio = pacienteDTO.Convenio,
                    StatusAtendimento = pacienteDTO.StatusAtendimento
                };

                //Verificar se existe o Paciente no banco de dados
                var pacienteModelDb = _atendimentoMedicoContext.Paciente.Where(x => x.CPF == pacienteDTO.CPF).FirstOrDefault();
                if (pacienteModelDb != null)
                    throw new MyException(409, $"O CPF informado já está cadastrado no sistema!");

                //Add na lista do DBSet Paciente
                _atendimentoMedicoContext.Paciente.Add(pacienteModel);

                //Salvar no banco de dados
                _atendimentoMedicoContext.SaveChanges();

                PacienteResponseDTO pacienteResponseDTO = new()
                {
                    Codigo = pacienteModel.Id,
                    Nome = pacienteModel.NomeCompleto,
                    Genero = pacienteModel.Genero,
                    DataNascimento = pacienteModel.DataNascimento,
                    CPF = pacienteModel.CPF,
                    Telefone = pacienteModel.Telefone,
                    ContatoEmergencia = pacienteModel.ContatoEmergencia,
                    Alergias = pacienteModel.Alergias,
                    CuidadosEspecificos = pacienteModel.CuidadosEspecificos,
                    Convenio = pacienteModel.Convenio,
                    StatusAtendimento = pacienteModel.StatusAtendimento,
                    Atendimentos = pacienteModel.TotalAtendimentos
                };

                return pacienteResponseDTO;

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
                var pacienteModel = _atendimentoMedicoContext.Paciente.Where(x => x.Id == identificador).FirstOrDefault();

                if (pacienteModel != null)
                {
                    pacienteModel.NomeCompleto = pacienteUpdateDTO.Nome;
                    pacienteModel.Genero = pacienteUpdateDTO.Genero;
                    pacienteModel.DataNascimento = pacienteUpdateDTO.DataNascimento;
                    pacienteModel.Telefone = pacienteUpdateDTO.Telefone;
                    pacienteModel.ContatoEmergencia = pacienteUpdateDTO.ContatoEmergencia;
                    pacienteModel.Alergias = pacienteUpdateDTO.Alergias;
                    pacienteModel.CuidadosEspecificos = pacienteUpdateDTO.CuidadosEspecificos;
                    pacienteModel.Convenio = pacienteUpdateDTO.Convenio;
                    pacienteModel.StatusAtendimento = pacienteUpdateDTO.StatusAtendimento;

                    //Add na lista do DBSet Paciente
                    _atendimentoMedicoContext.Paciente.Attach(pacienteModel);

                    //Salvar no banco de dados
                    _atendimentoMedicoContext.SaveChanges();

                    PacienteResponseDTO pacienteResponseDTO = new()
                    {
                        Codigo = pacienteModel.Id,
                        Nome = pacienteModel.NomeCompleto,
                        Genero = pacienteModel.Genero,
                        DataNascimento = pacienteModel.DataNascimento,
                        CPF = pacienteModel.CPF,
                        Telefone = pacienteModel.Telefone,
                        ContatoEmergencia = pacienteModel.ContatoEmergencia,
                        Alergias = pacienteModel.Alergias,
                        CuidadosEspecificos = pacienteModel.CuidadosEspecificos,
                        Convenio = pacienteModel.Convenio,
                        StatusAtendimento = pacienteModel.StatusAtendimento,
                        Atendimentos = pacienteModel.TotalAtendimentos
                    };

                    return pacienteResponseDTO;
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

        public void Remove(int identificador)
        {
            try
            {
                //Verificar se existe no banco de dados
                var pacienteModel = _atendimentoMedicoContext.Paciente.Find(identificador);

                if (pacienteModel != null)
                {
                    _atendimentoMedicoContext.Paciente.Remove(pacienteModel);
                    _atendimentoMedicoContext.SaveChanges();
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
                {
                    pacientesInnerJoin = _atendimentoMedicoContext.Paciente.Where(x => x.StatusAtendimento == status.StatusAtendimento);
                }
                else
                    pacientesInnerJoin = _atendimentoMedicoContext.Paciente;

                if (pacientesInnerJoin.Count() > 0)
                {
                    foreach (var paciente in pacientesInnerJoin)
                    {
                        PacienteResponseDTO pacienteGet = new()
                        {
                            Codigo = paciente.Id,
                            Nome = paciente.NomeCompleto,
                            Genero = paciente.Genero,
                            DataNascimento = paciente.DataNascimento,
                            CPF = paciente.CPF,
                            Telefone = paciente.Telefone,
                            ContatoEmergencia = paciente.ContatoEmergencia,
                            Alergias = paciente.Alergias,
                            CuidadosEspecificos = paciente.CuidadosEspecificos,
                            Convenio = paciente.Convenio,
                            StatusAtendimento = paciente.StatusAtendimento,
                            Atendimentos = paciente.TotalAtendimentos
                        };

                        lista.Add(pacienteGet);
                    }

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
                PacienteModel pacienteInnerJoin = _atendimentoMedicoContext.Paciente.Where(w => w.Id == identificador).FirstOrDefault();
                if (pacienteInnerJoin == null)
                    throw new MyException(404, "Paciente não encontrado para o identificador informado.");

                PacienteResponseDTO pacienteResponseDTO = new()
                {
                    Codigo = pacienteInnerJoin.Id,
                    Nome = pacienteInnerJoin.NomeCompleto,
                    Genero = pacienteInnerJoin.Genero,
                    DataNascimento = pacienteInnerJoin.DataNascimento,
                    CPF = pacienteInnerJoin.CPF,
                    Telefone = pacienteInnerJoin.Telefone,
                    ContatoEmergencia = pacienteInnerJoin.ContatoEmergencia,
                    Alergias = pacienteInnerJoin.Alergias,
                    CuidadosEspecificos = pacienteInnerJoin.CuidadosEspecificos,
                    Convenio = pacienteInnerJoin.Convenio,
                    StatusAtendimento = pacienteInnerJoin.StatusAtendimento,
                    Atendimentos = pacienteInnerJoin.TotalAtendimentos
                };

                return pacienteResponseDTO;

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
