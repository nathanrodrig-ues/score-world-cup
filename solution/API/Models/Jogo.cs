
using System.ComponentModel.DataAnnotations;

namespace API.Models;

public class Jogo
{
    public int IdJogo { get; set; }

    public long CpfUsuario { get; set; }
    public Usuario? Usuario { get; set; }

    public int IdSelecao1 { get; set; }
    public Selecao? Selecao1 { get; set; }

    public int IdSelecao2 { get; set; }
    public Selecao? Selecao2 { get; set; }
}
