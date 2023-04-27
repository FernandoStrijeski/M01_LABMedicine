using System.Text.Json.Serialization;

namespace m01_labMedicine.Models.Enum
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum EspecializacaoClinicaEnum
    {
        CLINICO_GERAL = 0,
        ANESTESISTA = 1,
        DERMATOLOGIA = 2,
        GINECOLOGIA = 3,
        NEUROLOGIA = 4,
        PEDIATRIA = 5,
        PSIQUIATRIA = 6,
        ORTOPEDIA = 7
    }
}
