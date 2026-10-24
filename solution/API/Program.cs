using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDataContext>();
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

//seleçao
app.MapPost("/api/selecao/cadastrar", ([FromBody] Selecao? selecao, [FromServices] AppDataContext ctx) =>{
    if(selecao is null){
        return Results.BadRequest("Seleçao não pode ser nulo");
    }
    if(string.IsNullOrEmpty(selecao.Nome)){
        return Results.BadRequest("Nome da seleçao não pode ser nulo");
    }

    if (string.IsNullOrEmpty(selecao.Grupo))
    {
        return Results.BadRequest("O grupo da seleção deve ser informado");
    }

    selecao.Grupo = selecao.Grupo.ToUpper().Trim();
    
    Selecao? selecaoEncontrada = ctx.Seleçoes.FirstOrDefault(x => x.Nome == selecao.Nome);
    if(selecaoEncontrada is not null){
        return Results.BadRequest("Seleçao ja existe");
    }

    int totalGrupo = ctx.Seleçoes.Count(x => x.Grupo == selecao.Grupo);
    if(totalGrupo >= 4){
        return Results.BadRequest("O grupo ja esta cheio");
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

//Usuario
app.MapPost("/api/usuario/cadastrar", ([FromBody] Usuario? usuario, [FromServices] AppDataContext ctx) => 
{
    if (usuario is null)
    {
        return Results.BadRequest("usuario não pode ser nulo");
    }

    if (string.IsNullOrEmpty(usuario.NameUser))
    {
        return Results.BadRequest("O nome do usuario é obrigatório.");
    }

    if(string.IsNullOrEmpty(usuario.Cpf)){
        return Results.BadRequest("O CPF do usuario é obrigatório.");
    }

    Usuario? usuarioEncontrado = ctx.Usuarios.FirstOrDefault(x => x.Cpf == usuario.Cpf);
    if(usuarioEncontrado is not null){
        return Results.BadRequest("Esse cpf ja foi cadastrado");
    }

    ctx.Usuarios.Add(usuario);
    ctx.SaveChanges();
    return Results.Created("", usuario);
});

app.MapGet("/api/usuario/listar", ([FromServices] AppDataContext ctx) =>{
    if(ctx.Usuarios.Count() == 0){
        return Results.BadRequest("Nenhum usuario foi cadastrado");
    }
    return Results.Ok(ctx.Usuarios.ToList());
});

//jogo
app.MapPost("/api/jogo/cadastrar", ([FromBody] Jogo? jogo, [FromServices] AppDataContext ctx) => 
{
    if (jogo is null)
    {
        return Results.BadRequest("Jogo não pode ser nulo");
    }

    var selecao1 = ctx.Seleçoes.Find(jogo.IdSelecao1);
    var selecao2 = ctx.Seleçoes.Find(jogo.IdSelecao2);

    if (selecao1 is null || selecao2 is null)
    {
        return Results.NotFound("Uma ou ambas as seleções informadas não foram encontradas.");
    }

    ctx.Jogos.Add(jogo);
    ctx.SaveChanges();
    
    return Results.Created("", jogo);
});

app.MapGet("/api/jogo/listar", ([FromServices] AppDataContext ctx) =>{
    if (!ctx.Jogos.Any())
    {
        return Results.NotFound("Não existem jogos cadastrados.");
    }

    var jogos = ctx.Jogos
        .Include(x => x.Selecao1)
        .Include(x => x.Selecao2)
        .ToList();

    return Results.Ok(jogos);
});

//PalpitesOdds
app.MapPost("/api/usuario/{idUser}/palpites", (int idUser, [FromBody] PalpitesOdds? palpite, [FromServices] AppDataContext ctx) => 
{
    if (palpite is null)
    {
        return Results.BadRequest("Palpite não pode ser nulo.");
    }

    palpite.IdUser = idUser;

    if (palpite.OldAposta <= 0)
    {
        return Results.BadRequest("O valor da odd da aposta não pode ser menor ou igual a 0.");
    }

    var usuarioExiste = ctx.Usuarios.Find(idUser);
    if (usuarioExiste is null)
    {
        return Results.NotFound("Utilizador não encontrado.");
    }

    var jogoExiste = ctx.Jogos.Find(palpite.IdJogo);
    if (jogoExiste is null)
    {
        return Results.NotFound("Jogo não encontrado.");
    }

    if (palpite.Vitoria == true)
    {
        palpite.Empate = false;
        palpite.Derrota = false;
    }
    else if (palpite.Empate == true)
    {
        palpite.Vitoria = false;
        palpite.Derrota = false;
    }
    else if (palpite.Derrota == true)
    {
        palpite.Vitoria = false;
        palpite.Empate = false;
    }

    ctx.PalpitesOdds.Add(palpite);
    ctx.SaveChanges();
    
    return Results.Created($"/api/usuarios/{idUser}/palpites/{palpite.IdAposta}", new {
        mensagem = "Palpite cadastrado com sucesso!",
        palpite.IdAposta,
        palpite.IdUser,
        palpite.IdJogo,
        palpite.Vitoria,
        palpite.Empate,
        palpite.Derrota,
        palpite.OldAposta
    });
});

app.MapGet("/api/usuario/{idUser}/palpites/listar", (int idUser, [FromServices] AppDataContext ctx) =>
{
    // Verifica primeiro se o utilizador existe
    var usuarioExiste = ctx.Usuarios.Find(idUser);
    if (usuarioExiste is null)
    {
        return Results.NotFound("Usuario não encontrado.");
    }

    // Filtra os palpites apenas pelo ID do utilizador da rota
    var palpitesDoUsuario = ctx.PalpitesOdds
        .Where(x => x.IdUser == idUser)
        .Include(x => x.Jogo) // Traz os dados do jogo associado
        .ToList();

    if (!palpitesDoUsuario.Any())
    {
        return Results.NotFound("Este Usuario ainda não fez nenhum palpite.");
    }

    return Results.Ok(palpitesDoUsuario);
});

app.Run();