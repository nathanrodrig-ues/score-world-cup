using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Models;

// Começa em 1: se o JSON não enviar o resultado, o 0 cai como inválido
public enum Resultado { Vitoria = 1, Empate = 2, Derrota = 3 }

// Índice único: 1 Palpite : 1 Jogo
[Index(nameof(IdJogo), IsUnique = true)]
public class Palpite
{
    [Key] public int IdAposta { get; set; }

    public int IdJogo { get; set; }
    [ForeignKey(nameof(IdJogo))] public Jogo? Jogo { get; set; }

    public Resultado ResultadoSelecao1 { get; set; }
    public Resultado ResultadoSelecao2 { get; set; }

    public decimal Odd { get; set; }        // informada no JSON
    public decimal OldAposta { get; set; }  // Odd * ValorApostado (calculado pela API)
    public DateTime CriadoEm { get; set; } = DateTime.Now;

    public void DefinirResultado(Resultado resultadoSelecao1)
    {
        ResultadoSelecao1 = resultadoSelecao1;
        ResultadoSelecao2 = resultadoSelecao1 switch
        {
            Resultado.Vitoria => Resultado.Derrota,
            Resultado.Derrota => Resultado.Vitoria,
            _ => Resultado.Empate
        };
    }
}