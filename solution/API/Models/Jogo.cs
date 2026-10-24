using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

public class Jogo
{
    [Key]
    public int Id { get; set; }

    
    public int IdSelecao1 { get; set; }
    [ForeignKey(nameof(IdSelecao1))]
    public Selecao Selecao1 {get; set; } = null!;
   
    
    public int IdSelecao2 { get; set; }
    [ForeignKey(nameof(IdSelecao2))]
    public Selecao Selecao2 {get; set; } = null!;

    public DateTime HorarioJogo {get; set; } = DateTime.Now;
}
