
using System.ComponentModel.DataAnnotations;

namespace API.Models;

public class PalpitesOdds
{
    [Key]
    public int IdAposta { get; set; }

    public int Id { get; set; }

    public bool Vitoria { get; set; }

    public bool Derrota { get; set; }

    public bool Empate { get; set; }

    public decimal OldAposta { get; set; }

    public DateTime CriadoEm { get; set; } = DateTime.Now;
}
