import { useState, ReactNode, MouseEvent } from "react";
import "./App.css";
import logo from "./assets/logo.png";

import JogosAoVivo from "./JogosAoVivo";

import CadastrarUsuario from "./CadastrarUsuario";
import ListarUsuario from "./ListarUsuario";
import DeletarUsuario from "./DeletarUsuario";

import CadastrarSelecao from "./CadastrarSelecao";
import ListarSelecao from "./ListarSelecao";
import DeletarSelecao from "./DeletarSelecao";

import CadastrarJogo from "./CadastrarJogo";
import ListarJogo from "./ListarJogo";
import DeletarJogo from "./DeletarJogo";

import CadastrarPalpite from "./CadastrarPalpite";
import ListarPalpites from "./ListarPalpite";
import AlterarPalpite from "./AlterarPalpite";
import DeletarPalpite from "./DeletarPalpite";

type Pagina =
  | "inicio"
  | "list-usuario"
  | "list-selecao"
  | "cad-selecao"
  | "del-selecao"
  | "list-jogo"
  | "cad-jogo"
  | "del-jogo"
  | "list-palpite"
  | "cad-palpite"
  | "alt-palpite"
  | "del-palpite";

type PainelUsuario = "cadastrar" | "deletar" | null;

interface ItemMenu {
  id: Pagina;
  rotulo: string;
  perigo?: boolean;
}

interface GrupoMenu {
  titulo: string;
  itens: ItemMenu[];
}

// Menus com submenu (abrem ao passar o mouse ou tocar)
const grupos: GrupoMenu[] = [
  {
    titulo: "Palpites",
    itens: [
      { id: "list-palpite", rotulo: "Listar Palpites" },
      { id: "cad-palpite", rotulo: "Cadastrar Palpite" },
      { id: "alt-palpite", rotulo: "Alterar Palpite" },
      { id: "del-palpite", rotulo: "Deletar Palpite", perigo: true },
    ],
  },
  {
    titulo: "Jogos",
    itens: [
      { id: "list-jogo", rotulo: "Listar Jogos" },
      { id: "cad-jogo", rotulo: "Cadastrar Jogo" },
      { id: "del-jogo", rotulo: "Deletar Jogo", perigo: true },
    ],
  },
  {
    titulo: "Seleções",
    itens: [
      { id: "list-selecao", rotulo: "Listar Seleções" },
      { id: "cad-selecao", rotulo: "Cadastrar Seleção" },
      { id: "del-selecao", rotulo: "Deletar Seleção", perigo: true },
    ],
  },
];

const titulos: Record<Pagina, string> = {
  "inicio": "Jogos ao Vivo",
  "list-usuario": "Usuários",
  "list-selecao": "Listar Seleções",
  "cad-selecao": "Cadastrar Seleção",
  "del-selecao": "Deletar Seleção",
  "list-jogo": "Listar Jogos",
  "cad-jogo": "Cadastrar Jogo",
  "del-jogo": "Deletar Jogo",
  "list-palpite": "Listar Palpites",
  "cad-palpite": "Cadastrar Palpite",
  "alt-palpite": "Alterar Palpite",
  "del-palpite": "Deletar Palpite",
};

const telas: Record<Pagina, ReactNode> = {
  "inicio": <JogosAoVivo />,
  "list-usuario": <ListarUsuario />,
  "list-selecao": <ListarSelecao />,
  "cad-selecao": <CadastrarSelecao />,
  "del-selecao": <DeletarSelecao />,
  "list-jogo": <ListarJogo />,
  "cad-jogo": <CadastrarJogo />,
  "del-jogo": <DeletarJogo />,
  "list-palpite": <ListarPalpites />,
  "cad-palpite": <CadastrarPalpite />,
  "alt-palpite": <AlterarPalpite />,
  "del-palpite": <DeletarPalpite />,
};

function App() {
  const [pagina, setPagina] = useState<Pagina>("inicio");
  const [painelUsuario, setPainelUsuario] = useState<PainelUsuario>(null);

  function ir(destino: Pagina, e?: MouseEvent<HTMLButtonElement>) {
    // tira o foco para o submenu fechar depois do clique
    e?.currentTarget.blur();
    setPagina(destino);
    setPainelUsuario(null);
  }

  function alternarPainel(painel: Exclude<PainelUsuario, null>) {
    setPainelUsuario(painelUsuario === painel ? null : painel);
  }

  return (
    <div className="app">
      <header className="topo">
        <img
          className="logo"
          src={logo}
          alt="Score World Cup"
          onClick={() => ir("inicio")}
        />

        <nav className="nav">
          <button
            type="button"
            className={"nav-link" + (pagina === "inicio" ? " ativo" : "")}
            onClick={(e) => ir("inicio", e)}
          >
            Jogos ao Vivo
          </button>

          {grupos.map((grupo) => {
            const ativo = grupo.itens.some((item) => item.id === pagina);
            return (
              <div className="nav-grupo" key={grupo.titulo}>
                <button
                  type="button"
                  className={"nav-link" + (ativo ? " ativo" : "")}
                >
                  {grupo.titulo} <span className="seta">▾</span>
                </button>
                <div className="nav-menu">
                  {grupo.itens.map((item) => (
                    <button
                      key={item.id}
                      type="button"
                      className={
                        "nav-menu-item" +
                        (item.perigo ? " perigo" : "") +
                        (pagina === item.id ? " ativo" : "")
                      }
                      onClick={(e) => ir(item.id, e)}
                    >
                      {item.rotulo}
                    </button>
                  ))}
                </div>
              </div>
            );
          })}

          <button
            type="button"
            className={"nav-link" + (pagina === "list-usuario" ? " ativo" : "")}
            onClick={(e) => ir("list-usuario", e)}
          >
            Usuários
          </button>
        </nav>

        <div className="topo-acoes">
          <button
            type="button"
            className={"btn-topo" + (painelUsuario === "cadastrar" ? " ativo" : "")}
            onClick={() => alternarPainel("cadastrar")}
          >
            Cadastrar Usuário
          </button>
          <button
            type="button"
            className={
              "btn-topo btn-topo-perigo" + (painelUsuario === "deletar" ? " ativo" : "")
            }
            onClick={() => alternarPainel("deletar")}
          >
            Deletar Usuário
          </button>
        </div>
      </header>

      {painelUsuario && (
        <>
          <div className="overlay" onClick={() => setPainelUsuario(null)} />
          <div className="popover">
            <button
              type="button"
              className="popover-fechar"
              aria-label="Fechar"
              onClick={() => setPainelUsuario(null)}
            >
              ×
            </button>
            {painelUsuario === "cadastrar" ? <CadastrarUsuario /> : <DeletarUsuario />}
          </div>
        </>
      )}

      <main className="conteudo">
        <div className="migalha">
          Home › <strong>{titulos[pagina]}</strong>
        </div>

        {/* key recarrega a tela (e as listas) ao trocar de página */}
        <div className="painel" key={pagina}>
          {telas[pagina]}
        </div>
      </main>
    </div>
  );
}

export default App;