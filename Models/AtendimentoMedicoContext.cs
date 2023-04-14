using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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

        public DbSet<PacienteModel> Paciente { get; set; }
        public DbSet<MedicoModel> Medico { get; set; }
        public DbSet<EnfermeiroModel> Enfermeiro { get; set; }
    }
}