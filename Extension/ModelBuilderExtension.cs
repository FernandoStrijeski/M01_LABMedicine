using m01_labMedicine.Model;
using Microsoft.EntityFrameworkCore;

namespace m01_labMedicine.Extension
{
    public static class ModelBuilderExtension
    {
        //seeder
        public static ModelBuilder CargaInicial(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PacienteModel>()
                .HasData(new PacienteModel() { Id = 1, NomeCompleto = "Fernando Strijeski", CPF = "08974003058", DataNascimento = DateTime.Parse("27/03/1981"), Genero = "Masculino", Telefone = "51991234569", ContatoEmergencia = "José", StatusAtendimento = "AGUARDANDO_ATENDIMENTO" },
                         new PacienteModel() { Id = 2, NomeCompleto = "João Almeida", CPF = "93779385031", DataNascimento = DateTime.Parse("26/04/1982"), Genero = "Masculino", Telefone = "51991458547", ContatoEmergencia = "Adão", StatusAtendimento = "AGUARDANDO_ATENDIMENTO" },
                         new PacienteModel() { Id = 3, NomeCompleto = "Humberto de Assis", CPF = "96569905015", DataNascimento = DateTime.Parse("26/05/1983"), Genero = "Masculino", Telefone = "51998563256", ContatoEmergencia = "Maria", StatusAtendimento = "AGUARDANDO_ATENDIMENTO" },
                         new PacienteModel() { Id = 4, NomeCompleto = "Júlia Rosa", CPF = "33295762007", DataNascimento = DateTime.Parse("24/06/1984"), Genero = "Feminino", Telefone = "51998856632", ContatoEmergencia = "Jacó", StatusAtendimento = "AGUARDANDO_ATENDIMENTO" },
                         new PacienteModel() { Id = 5, NomeCompleto = "Thiago Moreira", CPF = "99919521019", DataNascimento = DateTime.Parse("23/07/1985"), Genero = "Masculino", Telefone = "11994152365", ContatoEmergencia = "Pedro", StatusAtendimento = "AGUARDANDO_ATENDIMENTO" },
                         new PacienteModel() { Id = 6, NomeCompleto = "Bárbara Motta", CPF = "61560818018", DataNascimento = DateTime.Parse("22/08/1986"), Genero = "Feminino", Telefone = "11984574125", ContatoEmergencia = "Otávio", StatusAtendimento = "AGUARDANDO_ATENDIMENTO" },
                         new PacienteModel() { Id = 7, NomeCompleto = "Luciana de Andrade", CPF = "86376962017", DataNascimento = DateTime.Parse("21/09/1987"), Genero = "Feminino", Telefone = "11996563696", ContatoEmergencia = "Gisele", StatusAtendimento = "AGUARDANDO_ATENDIMENTO" },
                         new PacienteModel() { Id = 8, NomeCompleto = "Roberto Lima da Silva", CPF = "32671890044", DataNascimento = DateTime.Parse("20/10/1988"), Genero = "Masculino", Telefone = "54998878985", ContatoEmergencia = "Olavo", StatusAtendimento = "AGUARDANDO_ATENDIMENTO" },
                         new PacienteModel() { Id = 9, NomeCompleto = "Daniel Holanda", CPF = "77549502005", DataNascimento = DateTime.Parse("19/11/1989"), Genero = "Masculino", Telefone = "51991147484", ContatoEmergencia = "Bianca", StatusAtendimento = "AGUARDANDO_ATENDIMENTO" },
                         new PacienteModel() { Id = 10, NomeCompleto = "Paulo Pedrozo", CPF = "61067801022", DataNascimento = DateTime.Parse("18/12/1990"), Genero = "Masculino", Telefone = "51996566996", ContatoEmergencia = "Renan", StatusAtendimento = "AGUARDANDO_ATENDIMENTO" });

            modelBuilder.Entity<MedicoModel>()
                .HasData(new MedicoModel() { Id = 11, NomeCompleto = "Amanda Duarte", CPF = "27580525046", DataNascimento = DateTime.Parse("15/03/1966"), Genero = "Feminino", Telefone = "51991144444", InstituicaoEnsinoFormacao = "UFGRS", CrmUF = "1234567", EspecializacaoClinica = "Clínico Geral", EstadoSistema = "Ativo" },
                         new MedicoModel() { Id = 12, NomeCompleto = "Vitor Moraes", CPF = "61164709046", DataNascimento = DateTime.Parse("16/04/1977"), Genero = "Masculino", Telefone = "51987220022", InstituicaoEnsinoFormacao = "Ulbra", CrmUF = "2345678", EspecializacaoClinica = "Dermatologia", EstadoSistema = "Ativo" });
            
            modelBuilder.Entity<EnfermeiroModel>()
                .HasData(new EnfermeiroModel() { Id = 13, NomeCompleto = "Lucas Nascimento", CPF = "29949606055", DataNascimento = DateTime.Parse("17/05/1988"), Genero = "Masculino", Telefone = "51991232145", InstituicaoEnsinoFormacao = "PUCRS", CofenUF = "9876543" },
                         new EnfermeiroModel() { Id = 14, NomeCompleto = "Tatiana Lopes", CPF = "31533137099", DataNascimento = DateTime.Parse("18/06/1999"), Genero = "Feminino", Telefone = "51991234564", InstituicaoEnsinoFormacao = "UNISP", CofenUF = "8765432" });

            return modelBuilder;
        }
    }
}
