using m01_labMedicine.DTO.Pessoa.Paciente;

namespace m01_labMedicine.Services
{
    public interface IPacienteService
    {
        PacienteResponseDTO Atualiza(int identificador, PacienteUpdateDTO pacienteUpdateDTO);
        PacienteResponseDTO BuscaPaciente(int identificador);
        List<PacienteResponseDTO> BuscaPacientes(PacienteStatusRequestDTO status);
        PacienteResponseDTO Insere(PacienteRequestDTO pacienteDTO);
        void Remove(int identificador);
    }
}