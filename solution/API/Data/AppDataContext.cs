using Microsoft.EntityFrameworkCore;
using API.Models;

public class AppDataContext : DbContext{
    public DbSet<Selecao> Seleçoes {get; set;}
    public DbSet<Palpite> Palpites {get; set; }
    public DbSet<Usuario> Usuarios {get; set; }
    public DbSet<Jogo> Jogos {get; set;}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=CopaDoMundo.db");
    }
}