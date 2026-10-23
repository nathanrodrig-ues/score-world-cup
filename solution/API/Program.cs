using Microsoft.AspNetCore.Mvc;
using API.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDataContext>();
var app = builder.Build();


app.MapPost("/api/selecao/cadastrar", ([FromBody] Selecao? selecao, [FromServices] AppDataContext ctx) =>{
    if(selecao is null){
        return Results.BadRequest("Seleçao não pode ser nulo");
    }
    if(string.IsNullOrEmpty(selecao.Nome)){
        return Results.BadRequest("Nome da seleçao não pode ser nulo");
    }

    if (string.IsNullOrEmpty(selecao.Grupo))
    {
        return Results.BadRequest("ERRO: INSIRA O GRUPO DA SELEÇÃO");
    }

    selecao.Grupo = selecao.Grupo.ToUpper().Trim();
    
    Selecao? selecaoEncontrada = ctx.Seleçoes.FirstOrDefault(x => x.Nome == selecao.Nome);
    if(selecaoEncontrada is not null){
        return Results.BadRequest("ERRO: INSIRA UMA SELEÇÃO!");
    }

    int totalGrupo = ctx.Seleçoes.Count(x => x.Grupo == selecao.Grupo);
    if(totalGrupo >= 4){
        return Results.BadRequest("ERRO: GRUPO CHEIO!");
    }

    ctx.Seleçoes.Add(selecao);
    ctx.SaveChanges();
    return Results.Created("", selecao);

});

app.MapGet("/api/selecao/listar", ([FromServices] AppDataContext ctx) =>{
    if(ctx.Seleçoes.Count() == 0){
        return Results.BadRequest("ERRO: INSIRA UMA LISTA!");
    }
    return Results.Ok(ctx.Seleçoes.ToList());
});

app.MapDelete("/api/deletar/palpite/{IdAposta}", ([FromRoute] int IdAposta, [FromServices] AppDataContext ctx) => {

    PalpitesOdds? deletarAposta = ctx.PalpitesOdds.FirstOrDefault(d => d.IdAposta == IdAposta);
    if (deletarAposta is null) { return Results.NotFound("ERROR");}

    ctx.PalpitesOdds.Remove(deletarAposta);
    ctx.SaveChanges();
    return Results.Ok("SUCESSO: APOSTA DELETADA!");
});

app.MapDelete("/api/deletar/usuario/{IdUser}", ([FromRoute] int IdUser, [FromServices] AppDataContext ctx) => {

    Usuario? deletarUsuario = ctx.Usuarios.FirstOrDefault(d => d.IdUser == IdUser);
    if (deletarUsuario is null) { return Results.NotFound("ERROR"); }

    ctx.Usuarios.Remove(deletarUsuario);
    ctx.SaveChanges();
    return Results.Ok("SUCESSO: USUARIO DELETADO!");
});



app.Run();