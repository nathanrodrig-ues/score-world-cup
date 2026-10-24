using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

public class PalpitesOdds
{
    [Key]
    public int IdAposta { get; set; }

    public int IdJogo {get; set;}
    [ForeignKey(nameof(IdJogo))]
    public Jogo Jogo {get; set; } = null!;
    
    public int IdUser {get; set; }
    [ForeignKey(nameof(IdUser))]
    public Usuario Usuario {get; set; } = null!;

    public bool Vitoria { get; set; }

    public bool Derrota { get; set; } 

    public bool Empate { get; set; } 

    public decimal OldAposta { get; set; }

    public DateTime CriadoEm { get; set; } = DateTime.Now;
}
