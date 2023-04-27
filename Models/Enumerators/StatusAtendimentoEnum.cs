using System.Text.Json.Serialization;

namespace m01_labMedicine.Models.Enum
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum StatusAtendimentoEnum
    {
        AGUARDANDO_ATENDIMENTO = 0,
        EM_ATENDIMENTO = 1,
        ATENDIDO = 2,
        NAO_ATENDIDO = 3
    }
}
