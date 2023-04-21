using m01_labMedicine.DTO.Atendimento;

namespace m01_labMedicine.Services
{
    public interface IAtendimentoMedicoService
    {
        AtendimentoMedicoResponseDTO Atualiza(AtendimentoMedicoRequestDTO atendimentoMeditoRequestDTO);
        List<AtendimentoMedicoResponseDTO> BuscaAtendimentos(int medicoId);
    }
}