using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models;

public class Jogo
{
    [Key]
    public int IdJogo { get; set; }

    // Mesmo tipo da PK de Usuario (string)
    public string CpfUsuario { get; set; } = string.Empty;

    [ForeignKey(nameof(CpfUsuario))]
    public Usuario? Usuario { get; set; }

    public int IdSelecao1 { get; set; }

    [ForeignKey(nameof(IdSelecao1))]
    public Selecao? Selecao1 { get; set; }

    public int IdSelecao2 { get; set; }

    [ForeignKey(nameof(IdSelecao2))]
    public Selecao? Selecao2 { get; set; }
}