import { useState, useEffect } from "react";
import axios from "axios";

interface JogoApi {
  idJogo: number;
  usuario: string;
  selecao1: string;
  selecao2: string;
}

interface PalpiteApi {
  idAposta: number;
  idJogo: number;
  resultadoSelecao1: string | number;
  resultadoSelecao2: string | number;
  odd: number;
  oldAposta: number;
  criadoEm: string;
}

function rotuloResultado(r?: string | number) {
  if (r === 1 || r === "Vitoria") return "Vitória";
  if (r === 2 || r === "Derrota") return "Derrota";
  if (r === 3 || r === "Empate") return "Empate";
  return "—";
}

function textoPrevisao(p: PalpiteApi, selecao1: string, selecao2: string) {
  const r = rotuloResultado(p.resultadoSelecao1);
  if (r === "Vitória") return `${selecao1} vence`;
  if (r === "Derrota") return `${selecao2} vence`;
  if (r === "Empate") return "Empate";
  return "—";
}

function iniciais(nome: string) {
  return nome.slice(0, 2).toUpperCase();
}

function formatarMoeda(valor: number) {
  return valor.toLocaleString("pt-BR", { style: "currency", currency: "BRL" });
}

function JogosAoVivo() {
  const [jogos, setJogos] = useState<JogoApi[]>([]);
  const [palpites, setPalpites] = useState<PalpiteApi[]>([]);
  const [mensagem, setMensagem] = useState("");
  const [carregando, setCarregando] = useState(true);

  async function carregar() {
    setCarregando(true);
    setMensagem("");
    try {
      const [respJogos, respPalpites] = await Promise.all([
        axios.get("http://localhost:5047/api/jogo/listar"),
        axios.get("http://localhost:5047/api/palpite/listar"),
      ]);
      setJogos(respJogos.data);
      setPalpites(respPalpites.data);
    } catch (erro) {
      setMensagem("Não foi possível carregar os jogos. A API está rodando?");
    } finally {
      setCarregando(false);
    }
  }

  useEffect(() => {
    carregar();
  }, []);

  return (
    <div className="home">
      <div className="home-topo">
        <div>
          <h2 className="home-titulo">Palpites</h2>
          <p className="home-sub">Jogos ao Vivo</p>
        </div>
        <button type="button" className="btn-secundario" onClick={carregar}>
          Atualizar
        </button>
      </div>

      {carregando && <p className="aviso">Carregando jogos...</p>}
      {mensagem && <p className="aviso aviso-erro">{mensagem}</p>}
      {!carregando && !mensagem && jogos.length === 0 && (
        <p className="aviso">Nenhum jogo cadastrado ainda. Use o menu para cadastrar.</p>
      )}

      <div className="grade-jogos">
        {jogos.map((jogo) => {
          const palpite = palpites.find((p) => p.idJogo === jogo.idJogo);
          const data = palpite ? new Date(palpite.criadoEm) : null;
          // probabilidade implícita da odd (100 / odd), só para ilustrar na barra
          const probabilidade = palpite && palpite.odd > 0 ? Math.min(100, 100 / palpite.odd) : 0;

          return (
            <div className="card-jogo" key={jogo.idJogo}>
              <div className="card-jogo-topo">
                <strong>Copa do Mundo · Jogo #{jogo.idJogo}</strong>
                <span>
                  {data
                    ? `${data.toLocaleDateString("pt-BR")} ${data.toLocaleTimeString("pt-BR", {
                        hour: "2-digit",
                        minute: "2-digit",
                      })}`
                    : ""}
                </span>
              </div>

              <div className="confronto">
                <div className="time">
                  <span className="escudo">{iniciais(jogo.selecao1)}</span>
                  <span className="time-nome">{jogo.selecao1}</span>
                </div>
                <span className="vs">x</span>
                <div className="time">
                  <span className="escudo">{iniciais(jogo.selecao2)}</span>
                  <span className="time-nome">{jogo.selecao2}</span>
                </div>
              </div>

              <div className="previsao">
                {palpite ? (
                  <>
                    <div>
                      Palpite: <b>{textoPrevisao(palpite, jogo.selecao1, jogo.selecao2)}</b> · Odd{" "}
                      <b>{palpite.odd.toFixed(2)}</b>
                    </div>
                    <div className="previsao-old">
                      Old aposta: {formatarMoeda(palpite.oldAposta)}
                    </div>
                    <div className="barra">
                      <div className="barra-preenchida" style={{ width: `${probabilidade}%` }} />
                    </div>
                    <div className="confianca">
                      Probabilidade implícita {probabilidade.toFixed(2)}%
                    </div>
                  </>
                ) : (
                  <div>Sem palpite cadastrado</div>
                )}
              </div>
            </div>
          );
        })}
      </div>
    </div>
  );
}

export default JogosAoVivo;