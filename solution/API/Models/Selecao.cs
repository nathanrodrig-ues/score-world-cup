using System.ComponentModel.DataAnnotations;

namespace API.Models;


public class Selecao
{
    [Key]
    public int IdSelecao { get; set; }

    public string Nome { get; set; } = string.Empty;

    public string Continente { get; set; } = string.Empty;

    public int Titulo { get; set; }

    public int ParticipacaoEmCopas { get; set; }

    public string Grupo {get; set;} = string.Empty;

    public DateTime CriadoEm { get; set; } = DateTime.Now;
}
