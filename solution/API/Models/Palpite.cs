using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

public enum Resultado{ Vitoria,Empate,Derrota}

public class Palpite
{
    [Key] public int IdAposta { get; set; }
    [ForeignKey(nameof(IdJogo))] public Jogo? Jogo { get; set; }
    public decimal OldAposta { get; set; }
    public DateTime CriadoEm { get; set; } = DateTime.Now;
}
