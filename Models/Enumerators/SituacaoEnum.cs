using System.Text.Json.Serialization;

namespace m01_labMedicine.Models.Enum
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum SituacaoEnum
    {
        ATIVO = 0,
        INATIVO = 1
    }
}
