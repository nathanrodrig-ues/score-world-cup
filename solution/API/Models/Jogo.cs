
using System.ComponentModel.DataAnnotations;

namespace API.Models;

public class Jogo
{
    [Key]
    public int Id { get; set; }

    public int IdTime1 { get; set; }

    public int IdTime2 { get; set; }
}
