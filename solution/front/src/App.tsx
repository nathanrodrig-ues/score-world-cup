import React from 'react';

import CadastrarSelecao from './CadastrarSelecao';
import CadastrarJogo from './CadastrarJogo';
import CadastrarPalpite from './CadastrarPalpite';


import ListarSelecao from './ListarSelecao';
import ListarJogo from './ListarJogo';
import ListarPalpites from './ListarPalpite';
import AlterarPalpite from './AlterarPalpite';


function App() {
  return (
    <div className="App">
      <hr />

      <section>
        <h2>Seleções</h2>
        <CadastrarSelecao />
        <br />
        <ListarSelecao />
      </section>

      <hr />

      <section>
        <h2>Jogos</h2>
        <CadastrarJogo />
        <br />
        <ListarJogo />
      </section>

      <hr />

      <section>
        <h2>Apostas</h2>
        <CadastrarPalpite />
        <br />
        <ListarPalpites />
        <br />
        <AlterarPalpite />
      </section>
    </div>
  );
}

export default App;