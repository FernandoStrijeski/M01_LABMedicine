using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace m01_labMedicine.Migrations
{
    /// <inheritdoc />
    public partial class CargaInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Enfermeiro",
                columns: new[] { "Id", "CPF", "CofenUF", "DataNascimento", "Genero", "InstituicaoEnsinoFormacao", "NomeCompleto", "Telefone" },
                values: new object[,]
                {
                    { 1, "29949606055", "9876543", new DateTime(1988, 5, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Masculino", "PUCRS", "Lucas Nascimento", "51991232145" },
                    { 2, "31533137099", "8765432", new DateTime(1999, 6, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Feminino", "UNISP", "Tatiana Lopes", "51991234564" }
                });

            migrationBuilder.InsertData(
                table: "Medico",
                columns: new[] { "Id", "CPF", "CrmUF", "DataNascimento", "EspecializacaoClinica", "EstadoSistema", "Genero", "InstituicaoEnsinoFormacao", "NomeCompleto", "Telefone", "TotalAtendimentosRealizados" },
                values: new object[,]
                {
                    { 1, "27580525046", "1234567", new DateTime(1966, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Clínico Geral", "Ativo", "Feminino", "UFGRS", "Amanda Duarte", "51991144444", 0 },
                    { 2, "61164709046", "2345678", new DateTime(1977, 4, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dermatologia", "Ativo", "Masculino", "Ulbra", "Vitor Moraes", "51987220022", 0 }
                });

            migrationBuilder.InsertData(
                table: "Paciente",
                columns: new[] { "Id", "Alergias", "CPF", "ContatoEmergencia", "Convenio", "CuidadosEspecificos", "DataNascimento", "Genero", "NomeCompleto", "StatusAtendimento", "Telefone", "TotalAtendimentos" },
                values: new object[,]
                {
                    { 1, "", "08974003058", "José", null, "", new DateTime(1981, 3, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "Masculino", "Fernando Strijeski", "AGUARDANDO_ATENDIMENTO", "51991234569", 0 },
                    { 2, "", "93779385031", "Adão", null, "", new DateTime(1982, 4, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Masculino", "João Almeida", "AGUARDANDO_ATENDIMENTO", "51991458547", 0 },
                    { 3, "", "96569905015", "Maria", null, "", new DateTime(1983, 5, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Masculino", "Humberto de Assis", "AGUARDANDO_ATENDIMENTO", "51998563256", 0 },
                    { 4, "", "33295762007", "Jacó", null, "", new DateTime(1984, 6, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Feminino", "Júlia Rosa", "AGUARDANDO_ATENDIMENTO", "51998856632", 0 },
                    { 5, "", "99919521019", "Pedro", null, "", new DateTime(1985, 7, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "Masculino", "Thiago Moreira", "AGUARDANDO_ATENDIMENTO", "11994152365", 0 },
                    { 6, "", "61560818018", "Otávio", null, "", new DateTime(1986, 8, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Feminino", "Bárbara Motta", "AGUARDANDO_ATENDIMENTO", "11984574125", 0 },
                    { 7, "", "86376962017", "Gisele", null, "", new DateTime(1987, 9, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Feminino", "Luciana de Andrade", "AGUARDANDO_ATENDIMENTO", "11996563696", 0 },
                    { 8, "", "32671890044", "Olavo", null, "", new DateTime(1988, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Masculino", "Roberto Lima da Silva", "AGUARDANDO_ATENDIMENTO", "54998878985", 0 },
                    { 9, "", "77549502005", "Bianca", null, "", new DateTime(1989, 11, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Masculino", "Daniel Holanda", "AGUARDANDO_ATENDIMENTO", "51991147484", 0 },
                    { 10, "", "61067801022", "Renan", null, "", new DateTime(1990, 12, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Masculino", "Paulo Pedrozo", "AGUARDANDO_ATENDIMENTO", "51996566996", 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Enfermeiro",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Enfermeiro",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Medico",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Medico",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Paciente",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Paciente",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Paciente",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Paciente",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Paciente",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Paciente",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Paciente",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Paciente",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Paciente",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Paciente",
                keyColumn: "Id",
                keyValue: 10);
        }
    }
}
