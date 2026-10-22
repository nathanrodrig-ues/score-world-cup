
namespace API.Models;

public class Usuario
{
    public int IdUser { get; set; }

    public int IdAposta { get; set; }

    public string NameUser { get; set; } = string.Empty;

    public string MetodoPagamento { get; set; } = string.Empty;

    public decimal ValorApostado { get; set; }
}
