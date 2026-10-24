
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

public class Usuario
{
    [Key]
    public int IdUser { get; set; }

    [ForeignKey(nameof(PalpitesOdds))]
    public int IdAposta { get; set; }

    public PalpitesOdds PalpitesOdds { get; set; }

    public string NameUser { get; set; } = string.Empty;

    public string MetodoPagamento { get; set; } = string.Empty;

    public decimal ValorApostado { get; set; }

    public DateTime CriadoEm { get; set; } = DateTime.Now;
}
