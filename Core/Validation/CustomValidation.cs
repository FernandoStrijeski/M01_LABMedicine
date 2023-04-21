using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace m01_labMedicine.Core.Validation
{
    public partial class CustomValidation
    {
        public sealed class CheckStatusAtendimento : ValidationAttribute
        {
            public string AllowStatus { get; set; }
            public bool AllowStatusNull { get; set; }

            protected override ValidationResult IsValid(object statusAtendimento, ValidationContext validationContext)
            {
                if (AllowStatusNull && (statusAtendimento == null || statusAtendimento.ToString() == ""))
                    return ValidationResult.Success;

                string[] myarr = AllowStatus.ToString().Split(',');
                if (myarr.Contains(statusAtendimento))
                    return ValidationResult.Success;

                return new ValidationResult($"Por favor, informar um status válido! Ex. [{AllowStatus}]");
            }
        }

        public sealed class CheckEspecializacaoClinica : ValidationAttribute
        {
            public string AllowEspecializacoes { get; set; }

            protected override ValidationResult IsValid(object especializacoes, ValidationContext validationContext)
            {
                string[] myarr = AllowEspecializacoes.ToString().Split(',');
                if (myarr.Contains(especializacoes))
                    return ValidationResult.Success;

                return new ValidationResult($"Por favor, informar uma especialização válida! Ex. [{AllowEspecializacoes}]");
            }
        }
        public sealed class CheckSituacao : ValidationAttribute
        {
            public string AllowSituacoes { get; set; }

            protected override ValidationResult IsValid(object especializacoes, ValidationContext validationContext)
            {
                string[] myarr = AllowSituacoes.ToString().Split(',');
                if (myarr.Contains(especializacoes))
                    return ValidationResult.Success;

                return new ValidationResult($"Por favor, informar uma especialização válida! Ex. [{AllowSituacoes}]");
            }
        }

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
    }
}