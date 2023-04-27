using System.Text.Json.Serialization;

namespace m01_labMedicine.Models.Enum
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum GeneroEnum
    {
        MASCULINO = 0,
        FEMININO = 1,
        TRANSGENERO = 2,
        GENERO_NEUTRO = 3,
        NAO_BINARIO = 4,
        AGENERO = 5,
        PANGENERO = 6,
        GENDERQUEER = 7,
        TWO_SPIRIT = 8,
        TERCEIROGENERO = 9,
        OUTRO = 10
    }
}
