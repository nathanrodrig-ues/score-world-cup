using System.ComponentModel.DataAnnotations;

namespace API.Models;

public class Usuario
{
    [Key]
    public int IdUser { get; set; }

    public string NameUser { get; set; } = string.Empty;

    public string Cpf {get; set; } = string.Empty;

    public string MetodoPagamento { get; set; } = string.Empty;

    public decimal ValorApostado { get; set; }

    // Manténs apenas a coleção de navegação
    public ICollection<PalpitesOdds> PalpitesOdds { get; set; } = null!;

    public DateTime CriadoEm { get; set; } = DateTime.Now;
}