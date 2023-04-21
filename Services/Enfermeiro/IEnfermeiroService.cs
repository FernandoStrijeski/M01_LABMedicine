using m01_labMedicine.DTO.Pessoa.Enfermeiro;
using m01_labMedicine.DTO.Pessoa.Medico;

namespace m01_labMedicine.Services.Enfermeiro
{
    public interface IEnfermeiroService
    {
        EnfermeiroResponseDTO Atualiza(int identificador, EnfermeiroUpdateDTO enfermeiroUpdateDTO);
        EnfermeiroResponseDTO BuscaEnfermeiro(int identificador);
        List<EnfermeiroResponseDTO> BuscaEnfermeiros();
        EnfermeiroResponseDTO Insere(EnfermeiroRequestDTO enfermeiroRequestDTO);
        void Remove(int identificador);
    }
}