﻿using m01_labMedicine.Models.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace m01_labMedicine.Model
{
    public abstract class Pessoa
    {
        [Key]
        [Column("Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [NotNull]
        [MaxLength(100)]
        public string NomeCompleto { get; set; }

        public GeneroEnum Genero { get; set; }

        [NotNull]
        public DateTime DataNascimento { get; set; }

        [NotNull]
        [MaxLength(11)]
        public string CPF { get; set; }
        
        [MaxLength(11)]
        public string Telefone { get; set; }
    }
}
