
import React from "react";

import CadastrarUsuario from "./CadastrarUsuario";
import ListarUsuario from "./ListarUsuario";

import CadastrarSelecao from "./CadastrarSelecao";
import ListarSelecao from "./ListarSelecao";

import CadastrarJogo from "./CadastrarJogo";
import ListarJogo from "./ListarJogo";

import CadastrarPalpite from "./CadastrarPalpite";
import ListarPalpites from "./ListarPalpite";
import AlterarPalpite from "./AlterarPalpite";

import DeletarSelecao from "./DeletarSelecao";
import DeletarJogo from "./AlterarJogo";
import DeletarPalpite from "./AlterarPalpite";
import DeletarUsuario from "./AlterarUsuario";

function App() {
  return (
    <div className="App">
      <h1>Score World Cup</h1>

      <hr />

      <section>
        <h2>Usuários</h2>
        <CadastrarUsuario />
        <ListarUsuario />
      </section>

      <hr />

      <section>
        <h2>Seleções</h2>
        <CadastrarSelecao />
        <ListarSelecao />
      </section>

      <hr />

      <section>
        <h2>Jogos</h2>
        <CadastrarJogo />
        <ListarJogo />
      </section>

      <hr />

      <section>
        <h2>Palpites</h2>
        <CadastrarPalpite />
        <ListarPalpites />
        <AlterarPalpite />
      </section>
    </div>
  );
}

export default App;
