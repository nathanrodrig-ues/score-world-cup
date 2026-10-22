using Microsoft.AspNetCore.Mvc;
using API.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDataContext>();
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapPost("/api/selecao/cadastrar", ([FromBody] Selecao? selecao, [FromServices] AppDataContext ctx) =>{
    if(selecao is null){
        return Results.BadRequest("Seleçao não pode ser nulo");
    }
    if(string.IsNullOrEmpty(selecao.Nome)){
        return Results.BadRequest("Nome da seleçao não pode ser nulo");
    }
    
    Selecao? selecaoEncontrada = ctx.Seleçoes.FirstOrDefault(x => x.Nome == selecao.Nome);
    if(selecaoEncontrada is not null){
        return Results.BadRequest("Seleçao ja existe");
    }

    ctx.Seleçoes.Add(selecao);
    ctx.SaveChanges();
    return Results.Created("", selecao);

});

app.MapGet("/api/selecao/listar", ([FromServices] AppDataContext ctx) =>{
    if(ctx.Seleçoes.Count() == 0){
        return Results.BadRequest("A lista de seleçoes está vazia");
    }
    return Results.Ok(ctx.Seleçoes.ToList());
});

app.Run();