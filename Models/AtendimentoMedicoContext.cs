using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;

namespace m01_labMedicine.Model
{
   public class AtendimentoMedicoContext : DbContext
    {
        public AtendimentoMedicoContext(DbContextOptions<AtendimentoMedicoContext> options) : base(options)
        {             
            //Todo: Configurar migration
            //ToDo: Executar os comandos do migration
            //ToDo: Inserir, Atualizar, Deletar e Selecionar
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the value converter for the Alergias

            modelBuilder.Entity<PacienteModel>()
                .Property(x => x.Alergias)
                .HasConversion(new ValueConverter<List<string>, string>(
                    v => JsonConvert.SerializeObject(v), // Convert to string for persistence
                    v => JsonConvert.DeserializeObject<List<string>>(v))); // Convert to List<String> for use

            modelBuilder.Entity<PacienteModel>()
                .Property(x => x.CuidadosEspecificos)
                .HasConversion(new ValueConverter<List<string>, string>(
                    v => JsonConvert.SerializeObject(v), // Convert to string for persistence
                    v => JsonConvert.DeserializeObject<List<string>>(v))); // Convert to List<String> for use
        }
        public DbSet<PacienteModel> Paciente { get; set; }
        public DbSet<MedicoModel> Medico { get; set; }
        public DbSet<EnfermeiroModel> Enfermeiro { get; set; }
    }
}