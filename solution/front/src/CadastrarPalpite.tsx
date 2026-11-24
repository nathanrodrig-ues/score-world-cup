import { useState, FormEvent } from "react";
import axios from "axios";
import { Aposta, Resultado } from "./models/Palpite";

function CadastrarPalpite() {
  const [idJogo, setIdJogo] = useState<number>(0);
  const [resultado, setResultado] = useState<Resultado>(Resultado.Vitoria);
  const [odd, setOdd] = useState<number>(1.50);
  const [mensagem, setMensagem] = useState("");

  async function enviarPalpiteAPI(e: FormEvent<HTMLFormElement>) {
    e.preventDefault();
    setMensagem("");

    if (idJogo <= 0) {
      return setMensagem("Informe um ID de jogo válido.");
    }

    const palpite: Aposta = { 
      idJogo, 
      resultadoSelecao1: resultado, 
      odd 
    };

    try {
      await axios.post("http://localhost:5047/api/palpite/cadastrar", palpite);
      setMensagem("Palpite cadastrado com sucesso!");
      
      // Limpando o formulário
      setIdJogo(0);
      setResultado(Resultado.Vitoria);
      setOdd(1.50);
    } catch (erro) {
      if (axios.isAxiosError(erro) && erro.response) {
        setMensagem(String(erro.response.data));
      } else {
        setMensagem("Não foi possível conectar à API.");
      }
    }
  }

  return (
    <form onSubmit={enviarPalpiteAPI}>
      <h1>Cadastrar Palpite</h1>
      
      <div>
        <label>ID do Jogo</label>
        <input
          type="number"
          value={idJogo || ''}
          onChange={(e) => setIdJogo(Number(e.target.value))}
          required
        />
      </div>

      <div>
        <label>Palpite (Seleção 1)</label>
        <select
          value={resultado}
          onChange={(e) => setResultado(Number(e.target.value) as Resultado)}
          required
        >
          <option value={Resultado.Vitoria}>Vitória</option>
          <option value={Resultado.Derrota}>Derrota</option>
          <option value={Resultado.Empate}>Empate</option>
        </select>
      </div>

      <div>
        <label>Odd</label>
        <input
          type="number"
          step="0.01"
          min="1.01"
          value={odd}
          onChange={(e) => setOdd(Number(e.target.value))}
          required
        />
      </div>

      <button type="submit">Cadastrar</button>
      {mensagem && <p>{mensagem}</p>}
    </form>
  );
}

export default CadastrarPalpite;