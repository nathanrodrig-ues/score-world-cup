
namespace API.Models;

public class Usuario
{
    [Key] public string CpfUsuario { get; set; } = string.Empty.ToString();
    public int IdAposta { get; set; }
    public string NameUser { get; set; } = string.Empty;
    public string MetodoPagamento { get; set; } = string.Empty;
    public decimal ValorApostado { get; set; }
}
