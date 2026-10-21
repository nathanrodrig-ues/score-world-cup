
namespace API.Models;

public class Selecao
{
    public int Id { get; set; }

    public string Nome { get; set; } = string.Empty;

    public string Continente { get; set; } = string.Empty;

    public int Titulo { get; set; }

    public int ParticipacaoEmCopas { get; set; }

    public int CriadoEm { get; set; }
}
