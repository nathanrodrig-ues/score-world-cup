using System.ComponentModel.DataAnnotations;

namespace API.Models;

public class Usuario
{
    // PK = CPF, como texto (não perde o zero à esquerda)
    [Key] public string CpfUsuario { get; set; } = string.Empty;
    public string NameUser { get; set; } = string.Empty;
    public string MetodoPagamento { get; set; } = string.Empty;
    public decimal ValorApostado { get; set; }
}