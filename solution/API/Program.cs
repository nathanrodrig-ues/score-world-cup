using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models;
using System.Security.Cryptography.X509Certificates;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDataContext>();
builder.Services.ConfigureHttpJsonOptions(o => {
    // Enum: "Vitoria" / "Empate" / "Derrota"
    o.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
    // "cpfUsuario": "12345678901"
    o.SerializerOptions.NumberHandling = JsonNumberHandling.AllowReadingFromString;
});
var app = builder.Build();


//Selecao
app.MapPost("/api/selecao/cadastrar", ([FromBody] Selecao? selecao, [FromServices] AppDataContext ctx) =>{
    
    if(selecao is null){return Results.BadRequest("ERRO: JSON NULL");}

    if(string.IsNullOrEmpty(selecao.Nome)){
        return Results.BadRequest("ERRO: CAMPO NOME VAZIO");
    }
    if (string.IsNullOrEmpty(selecao.Grupo)){return Results.BadRequest("ERRO: INSIRA O GRUPO DA SELEÇÃO");}

    selecao.Grupo = selecao.Grupo.ToUpper().Trim();
    Selecao? selecaoEncontrada = ctx.Seleçoes.FirstOrDefault(x => x.Nome == selecao.Nome);
    if(selecaoEncontrada is not null){return Results.BadRequest("ERRO: INSIRA UMA SELEÇÃO!");}

    int totalGrupo = ctx.Seleçoes.Count(x => x.Grupo == selecao.Grupo);
    if(totalGrupo >= 4){return Results.BadRequest("ERRO: GRUPO CHEIO!");}

    ctx.Seleçoes.Add(selecao);
    ctx.SaveChanges();
    return Results.Created("", selecao);
});

app.MapGet("/api/selecao/listar", ([FromServices] AppDataContext ctx) =>{
    if(ctx.Seleçoes.Count() == 0){return Results.BadRequest("ERRO: INSIRA UMA LISTA!");}
    return Results.Ok(ctx.Seleçoes.ToList());
});

app.MapDelete("/api/selecao/deletar/{id}",([FromRoute] int id, [FromBody] Selecao? selecao, [FromServices] AppDataContext ctx) => {
    Selecao? selecaoEncontrada = ctx.Seleçoes.Find(id);

    if(selecaoEncontrada is null){
        return Results.BadRequest("ERRO: NENHUMA SELEÇÃO ENCONTRADA");
    }

    ctx.Seleçoes.Remove(selecaoEncontrada);
    ctx.SaveChanges();
    return Results.Ok("SELEÇÂO DELETADA");
});

//Usuario
app.MapPost("/api/usuario/cadastrar", ([FromBody] Usuario? usuario, [FromServices] AppDataContext ctx) => {

    if (usuario is null) { return Results.BadRequest("ERRO: USUARIO NÃO PODE SER NULO"); }
    if (usuario.CpfUsuario is null) { return Results.BadRequest("ERRO: INSIRA O CPF DO USUARIO"); }
    if (string.IsNullOrWhiteSpace(usuario.NameUser)) { return Results.BadRequest("ERRO: INSIRA O NOME DO USUARIO"); }
    if (string.IsNullOrWhiteSpace(usuario.MetodoPagamento)) { return Results.BadRequest("ERRO: INSIRA O METODO DE PAGAMENTO"); }
    if (usuario.ValorApostado <= 10) { return Results.BadRequest("ERRO: VALOR APOSTADO DEVE SER MAIOR QUE R$ 10,00"); }

    if (ctx.Usuarios.Any(u => u.CpfUsuario == usuario.CpfUsuario))
    {
        return Results.BadRequest("ERRO: CPF JÁ CADASTRADO!");
    }

    ctx.Usuarios.Add(usuario);
    ctx.SaveChanges();
    return Results.Created("", usuario);
});

app.MapGet("/api/usuario/listar", ([FromServices] AppDataContext ctx) => {
    return Results.Ok(ctx.Usuarios.ToList());
});

app.MapDelete("/api/Usuario/deletar/{CpfUsuario}", ([FromRoute] string CpfUsuario, [FromBody] Usuario? usuario, [FromServices] AppDataContext ctx) => {
    Usuario? usuarioEncontrado = ctx.Usuarios.Find(CpfUsuario);

    if(usuarioEncontrado is null){
        return Results.BadRequest("ERRO: NÂO EXISTE NENHUM USUARIO CADASTRADO COM ESSE CPF");
    }

    ctx.Usuarios.Remove(usuarioEncontrado);
    ctx.SaveChanges();
    return Results.Ok("USUÁRIO DELETADO");
});


// JSON: { "cpfUsuario": 12345678901, "idSelecao1": 1, "idSelecao2": 23 }
app.MapPost("/api/jogo/cadastrar", ([FromBody] Jogo? jogo, [FromServices] AppDataContext ctx) => {
    if (jogo is null) { return Results.BadRequest("ERRO: JOGO NÃO PODE SER NULO"); }

    if (jogo.IdSelecao1 == jogo.IdSelecao2)
    {
        return Results.BadRequest("ERRO: UMA SELEÇÃO NÃO PODE JOGAR CONTRA ELA MESMA");
    }
    if (ctx.Usuarios.Find(jogo.CpfUsuario) is null)
    {
        return Results.NotFound("ERRO: USUARIO NÃO ENCONTRADO");
    }
    if (ctx.Seleçoes.Find(jogo.IdSelecao1) is null || ctx.Seleçoes.Find(jogo.IdSelecao2) is null)
    {
        return Results.NotFound("ERRO: SELEÇÃO NÃO ENCONTRADA");
    }

    // Cria um objeto novo: ignora qualquer IdJogo/navegação enviado no JSON
    Jogo novoJogo = new Jogo
    {
        CpfUsuario = jogo.CpfUsuario,
        IdSelecao1 = jogo.IdSelecao1,
        IdSelecao2 = jogo.IdSelecao2
    };

    ctx.Jogos.Add(novoJogo);
    ctx.SaveChanges();
    return Results.Created("", new
    {
        novoJogo.IdJogo,
        novoJogo.CpfUsuario,
        novoJogo.IdSelecao1,
        novoJogo.IdSelecao2
    });
});

app.MapGet("/api/jogo/listar", ([FromServices] AppDataContext ctx) => {
    return Results.Ok(ctx.Jogos.Select(j => new {
        j.IdJogo,
        j.CpfUsuario,
        Usuario = j.Usuario!.NameUser,
        j.IdSelecao1,
        Selecao1 = j.Selecao1!.Nome,
        j.IdSelecao2,
        Selecao2 = j.Selecao2!.Nome
    }).ToList());
});

app.MapDelete("/api/jogo/deletar/{IdJogo}", ([FromRoute] int IdJogo, [FromBody] Jogo? jogo, [FromServices] AppDataContext ctx) =>{
    Jogo? jogoEncontrado = ctx.Jogos.Find(IdJogo);
    if(jogoEncontrado is null){
        return Results.BadRequest("ERRO: JOGO NÂO ENCONTRADO");
    }

    ctx.Jogos.Remove(jogoEncontrado);
    ctx.SaveChanges();
    return Results.Ok("JOGO DELETADO");
});

//palpite
app.MapPost("/api/palpite/cadastrar", ([FromBody] Palpite? palpite, [FromServices] AppDataContext ctx) => {
    if (palpite is null) { return Results.BadRequest("ERRO: PALPITE NÃO PODE SER NULO"); }

    if (!Enum.IsDefined(palpite.ResultadoSelecao1))
    {
        return Results.BadRequest("ERRO: RESULTADO DEVE SER Vitoria, Empate OU Derrota");
    }
    if (palpite.Odd <= 0)
    {
        return Results.BadRequest("ERRO: INSIRA A ODD (MAIOR QUE ZERO)");
    }

    Jogo? jogo = ctx.Jogos.Include(j => j.Usuario).FirstOrDefault(j => j.IdJogo == palpite.IdJogo);
    if (jogo is null || jogo.Usuario is null) { return Results.NotFound("ERRO: JOGO NÃO ENCONTRADO"); }

    if (ctx.Palpites.Any(p => p.IdJogo == jogo.IdJogo))
    {
        return Results.BadRequest("ERRO: ESTE JOGO JÁ POSSUI PALPITE");
    }

    Palpite novoPalpite = new Palpite
    {
        IdJogo = jogo.IdJogo,
        Odd = palpite.Odd,
        OldAposta = palpite.Odd * jogo.Usuario.ValorApostado
    };
    novoPalpite.DefinirResultado(palpite.ResultadoSelecao1);

    ctx.Palpites.Add(novoPalpite);
    ctx.SaveChanges();

    return Results.Created("", new
    {
        novoPalpite.IdAposta,
        novoPalpite.IdJogo,
        novoPalpite.ResultadoSelecao1,
        novoPalpite.ResultadoSelecao2,
        novoPalpite.Odd,
        novoPalpite.OldAposta,
        novoPalpite.CriadoEm
    });
});

app.MapGet("/api/palpite/listar", ([FromServices] AppDataContext ctx) => {
    return Results.Ok(ctx.Palpites.Select(p => new {
        p.IdAposta,
        p.IdJogo,
        Usuario = p.Jogo!.Usuario!.NameUser,
        Selecao1 = p.Jogo.Selecao1!.Nome,
        p.ResultadoSelecao1,
        Selecao2 = p.Jogo.Selecao2!.Nome,
        p.ResultadoSelecao2,
        p.Odd,
        p.OldAposta,
        p.CriadoEm
    }).ToList());
});

app.MapDelete("/api/palpite/deletar/{IdAposta}", ([FromRoute] int IdAposta, [FromBody] Palpite? palpite, [FromServices] AppDataContext ctx ) =>{
    Palpite? palpiteEncontrado = ctx.Palpites.Find(IdAposta);
    if(palpiteEncontrado is null){
        return Results.BadRequest("ERRO: ESSE PALPITE NÂO EXISTE");
    }

    ctx.Palpites.Remove(palpiteEncontrado);
    ctx.SaveChanges();
    return Results.Ok("Palpite Deletado");
});

app.MapPut("/api/palpite/alterar/{idAposta}", ([FromRoute] int idAposta, [FromBody] Palpite? palpiteModificado, [FromServices] AppDataContext ctx) => {
    
    if (palpiteModificado is null)
    {
        return Results.BadRequest("ERRO: PALPITE ALTERADO ESTA NULO");
    }

    Palpite? palpiteExistente = ctx.Palpites.Find(idAposta);
    if (palpiteExistente is null)
    {
        return Results.NotFound("ERRO: ESSE PALPITE NÃO EXISTE");
    }

    palpiteExistente.IdJogo = palpiteModificado.IdJogo;
    palpiteExistente.ResultadoSelecao1 = palpiteModificado.ResultadoSelecao1;
    palpiteExistente.ResultadoSelecao2 = palpiteModificado.ResultadoSelecao2;
    palpiteExistente.Odd = palpiteModificado.Odd;

    ctx.Palpites.Update(palpiteExistente);
    ctx.SaveChanges();

    return Results.Ok(new { Mensagem = "SUCESSO: PALPITE ALTERADO COM SUCESSO!", Palpite = palpiteExistente });
});
app.Run();