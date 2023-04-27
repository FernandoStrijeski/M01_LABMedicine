using m01_labMedicine.Models.Enum;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace m01_labMedicine.Core.Validation
{
    public partial class CustomValidation
    {              
        public sealed class CheckCPF : ValidationAttribute
        {
            protected override ValidationResult IsValid(object cpf, ValidationContext validationContext)
            {
                if (ValidarCPF(cpf.ToString()))
                    return ValidationResult.Success;

                return new ValidationResult("Por favor, informar um CPF válido, somente números!");
            }
            public static bool ValidarCPF(string cpf)
            {
                // Remove caracteres não numéricos do CPF
                cpf = Regex.Replace(cpf, @"[^\d]", "");

                // Verifica se o CPF possui 11 dígitos
                if (cpf.Length != 11)
                    return false;

                // Verifica se todos os dígitos são iguais (ex: 111.111.111-11)
                if (new string(cpf[0], 11) == cpf)
                    return false;

                // Calcula os dois dígitos verificadores
                int soma = 0;
                for (int i = 0; i < 9; i++)
                    soma += int.Parse(cpf[i].ToString()) * (10 - i);
                int digito1 = 11 - soma % 11;
                if (digito1 >= 10)
                    digito1 = 0;

                soma = 0;
                for (int i = 0; i < 10; i++)
                    soma += int.Parse(cpf[i].ToString()) * (11 - i);
                int digito2 = 11 - soma % 11;
                if (digito2 >= 10)
                    digito2 = 0;

                // Verifica se os dígitos calculados são iguais aos dígitos informados
                if (digito1 != int.Parse(cpf[9].ToString()) || digito2 != int.Parse(cpf[10].ToString()))
                    return false;

                // CPF válido
                return true;
            }
        }

        public sealed partial class CheckTelefone : ValidationAttribute
        {
            protected override ValidationResult IsValid(object telefone, ValidationContext validationContext)
            {
                if (telefone == null || ValidarTelefone(telefone.ToString()))
                    return ValidationResult.Success;

                return new ValidationResult("Por favor, informar um telefone válido contendo DDD, somente números!");
            }

            public static bool ValidarTelefone(string telefone)
            {
                // Remove caracteres não numéricos do telefone
                telefone = Regex.Replace(telefone, @"[^\d]", "");

                // Verifica se o telefone/celular possui 10 ou 11 dígitos (com DDD de 2 dígitos)
                if (telefone.Length != 10 && telefone.Length != 11)
                    return false;

                // Telefone válido
                return true;
            }
        }
        
        public sealed class StatusAtendimentoConverter : JsonConverter<StatusAtendimentoEnum>
        {
            public override StatusAtendimentoEnum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                string statusAtendimentoValidos = string.Join(",", Enum.GetNames(typeof(StatusAtendimentoEnum)));

                var value = reader.GetString();
                if (!Enum.TryParse<StatusAtendimentoEnum>(value, out var result)) 
                    throw new JsonException($"Por favor, informe um valor válido para o Status de Atendimento: {statusAtendimentoValidos}");

                return result;
            }

            public override void Write(Utf8JsonWriter writer, StatusAtendimentoEnum value, JsonSerializerOptions options) => writer.WriteStringValue(value.ToString());            
        }

        public sealed class SituacaoConverter : JsonConverter<SituacaoEnum>
        {
            public override SituacaoEnum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                var value = reader.GetString();
                if (!Enum.TryParse<SituacaoEnum>(value, out var result))
                    throw new JsonException($"Por favor, informe um valor válido para a Situação: {string.Join(",", Enum.GetNames(typeof(SituacaoEnum)))}");

                return result;
            }

            public override void Write(Utf8JsonWriter writer, SituacaoEnum value, JsonSerializerOptions options) => writer.WriteStringValue(value.ToString());
        }

        public sealed class EspecializacaoClinicaConverter : JsonConverter<EspecializacaoClinicaEnum>
        {
            public override EspecializacaoClinicaEnum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {                
                var value = reader.GetString();
                if (!Enum.TryParse<EspecializacaoClinicaEnum>(value, out var result))
                    throw new JsonException($"Por favor, informe um valor válido para Especialização Clínica: {string.Join(",", Enum.GetNames(typeof(EspecializacaoClinicaEnum)))}");

                return result;
            }

            public override void Write(Utf8JsonWriter writer, EspecializacaoClinicaEnum value, JsonSerializerOptions options) => writer.WriteStringValue(value.ToString());
        }
        public sealed class GeneroConverter : JsonConverter<GeneroEnum>
        {
            public override GeneroEnum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {                
                var value = reader.GetString();
                if (!Enum.TryParse<GeneroEnum>(value, out var result))
                    throw new JsonException($"Por favor, informe um valor válido para o Gênero: {string.Join(",", Enum.GetNames(typeof(GeneroEnum)))}");

                return result;
            }

            public override void Write(Utf8JsonWriter writer, GeneroEnum value, JsonSerializerOptions options) => writer.WriteStringValue(value.ToString());
        }
    }
}