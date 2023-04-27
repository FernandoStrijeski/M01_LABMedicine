using m01_labMedicine.DTO.Pessoa.Medico;
using m01_labMedicine.Models.Enum;

namespace m01_labMedicine.Services.Medico
{
    public interface IMedicoService
    {
        MedicoResponseDTO Atualiza(int identificador, MedicoUpdateDTO medicoUpdateDTO);
        MedicoResponseDTO BuscaMedico(int identificador);
        List<MedicoResponseDTO> BuscaMedicos(SituacaoEnum? status);
        MedicoResponseDTO Insere(MedicoRequestDTO medicoDTO);
        void Remove(int identificador);
    }
}