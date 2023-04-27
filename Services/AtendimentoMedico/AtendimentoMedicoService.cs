using AutoMapper;
using m01_labMedicine.Core.Exceptions;
using m01_labMedicine.DTO.Atendimento;
using m01_labMedicine.DTO.Pessoa.Medico;
using m01_labMedicine.DTO.Pessoa.Paciente;
using m01_labMedicine.Model;
using Microsoft.EntityFrameworkCore;

namespace m01_labMedicine.Services.AtendimentoMedico
{
    public class AtendimentoMedicoService : IAtendimentoMedicoService
    {
        private readonly LabMedicineContext _labMedicineContext;
        private readonly IMapper _mapper;

        public AtendimentoMedicoService(IMapper mapper, LabMedicineContext labMedicineContext) => (_mapper, _labMedicineContext) = (mapper, labMedicineContext);

        public AtendimentoMedicoResponseDTO Atualiza(AtendimentoMedicoRequestDTO atendimentoMeditoRequestDTO)
        {
            try
            {
                //Verificar se existe o medico no banco de dados
                var medicoModel = _labMedicineContext.Medico.Where(x => x.Id == atendimentoMeditoRequestDTO.IdMedico).FirstOrDefault() ?? throw new MyException(404, "Médico não encontrado no sistema para o identificador informado! Revise.");

                //Verificar se existe o paciente no banco de dados
                var pacienteModel = _labMedicineContext.Paciente.Where(x => x.Id == atendimentoMeditoRequestDTO.IdPaciente).FirstOrDefault() ?? throw new MyException(404, "Paciente não encontrado no sistema para o identificador informado! Revise.");
                medicoModel.TotalAtendimentosRealizados += 1;

                //Add na lista do DBSet Medico
                _labMedicineContext.Medico.Attach(medicoModel);

                pacienteModel.TotalAtendimentos += 1;
                pacienteModel.StatusAtendimento = Models.Enum.StatusAtendimentoEnum.ATENDIDO;

                //Add na lista do DBSet Paciente
                _labMedicineContext.Paciente.Attach(pacienteModel);

                AtendimentoMedicoModel atendimentoMedicoModel = new()
                {
                    MedicoId = medicoModel.Id,
                    Medico = medicoModel,
                    PacienteId = pacienteModel.Id,
                    Paciente = pacienteModel,
                    DescricaoAtendimento = atendimentoMeditoRequestDTO.DescricaoAtendimento
                };
                _labMedicineContext.AtendimentoMedico.Add(atendimentoMedicoModel);

                //Salvar no banco de dados
                _labMedicineContext.SaveChanges();

                AtendimentoMedicoResponseDTO atendimentoResponseDTO = new()
                {
                    Medico = _mapper.Map<MedicoResponseDTO>(medicoModel),
                    Paciente = _mapper.Map<PacienteResponseDTO>(pacienteModel),
                    DescricaoAtendimento = atendimentoMedicoModel.DescricaoAtendimento
                };

                return atendimentoResponseDTO;

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

        public List<AtendimentoMedicoResponseDTO> BuscaAtendimentos(int medicoId)
        {
            try
            {
                List<AtendimentoMedicoResponseDTO> lista = new();
                IQueryable<AtendimentoMedicoModel> atendimentosMedicoInnerJoin;

                if (medicoId > 0)
                    atendimentosMedicoInnerJoin = _labMedicineContext.AtendimentoMedico.Where(x => x.MedicoId == medicoId)
                        .Include(a => a.Paciente)
                        .Include(a => a.Medico);
                else
                    atendimentosMedicoInnerJoin = _labMedicineContext.AtendimentoMedico
                        .Include(a => a.Paciente)
                        .Include(a => a.Medico);

                if (atendimentosMedicoInnerJoin.Any())
                {
                    foreach (var atendimentoMedico in atendimentosMedicoInnerJoin)
                    {
                        var medico = _mapper.Map<MedicoResponseDTO>(atendimentoMedico.Medico);
                        medico.Codigo = atendimentoMedico.MedicoId;

                        var paciente = _mapper.Map<PacienteResponseDTO>(atendimentoMedico.Paciente);
                        paciente.Codigo = atendimentoMedico.PacienteId;

                        AtendimentoMedicoResponseDTO atendimentoMedicoResponseDTO = new()
                        {
                            Medico = medico,
                            Paciente = paciente,
                            DescricaoAtendimento = atendimentoMedico.DescricaoAtendimento
                        };

                        lista.Add(atendimentoMedicoResponseDTO);
                    }
                    return lista;
                }
                else
                {
                    if (medicoId > 0)
                        throw new MyException(404, "Nenhum atendimento encontrado para o médico informado.");
                    else
                        throw new MyException(404, "Nenhum atendimento realizado.");
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
    }
}
