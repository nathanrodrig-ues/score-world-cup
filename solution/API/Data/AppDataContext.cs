using Microsoft.EntityFrameworkCore;
using API.Models;

public class AppDataContext : DbContext{
    public DbSet<Selecao> Seleçoes {get; set;}

    public DbSet<PalpitesOdds> PalpitesOdds {get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=CopaDoMundo.db");
    }
}