import { useState, FormEvent } from "react";
import axios from "axios";
import { Resultado } from "./models/Palpite";

function AlterarPalpite() {
  const [idAposta, setIdAposta] = useState<number>(0);
  const [idJogo, setIdJogo] = useState<number>(0);
  const [resultado, setResultado] = useState<Resultado>(Resultado.Vitoria);
  const [odd, setOdd] = useState<number>(1.50);
  const [mensagem, setMensagem] = useState("");

  async function alterarApostaAPI(e: FormEvent<HTMLFormElement>) {
    e.preventDefault();
    setMensagem("");

    if (idAposta <= 0 || idJogo <= 0) {
      return setMensagem("Preencha o ID da Aposta e o ID do Jogo.");
    }

    const apostaAtualizada = {
      idJogo: idJogo,
      resultadoSelecao1: resultado,
      resultadoSelecao2: resultado === Resultado.Vitoria ? Resultado.Derrota : Resultado.Vitoria,
      odd: odd
    };

    try {
      await axios.put(`http://localhost:5047/api/palpite/alterar/${idAposta}`, apostaAtualizada);
      
      setMensagem("Aposta alterada com sucesso!");
      
      setIdAposta(0);
      setIdJogo(0);
      setResultado(Resultado.Vitoria);
      setOdd(1.50);
      
    } catch (erro) {
      if (axios.isAxiosError(erro) && erro.response) {
        setMensagem(String(erro.response.data));
      } else {
        setMensagem("Erro ao alterar. Verifique se o ID da aposta existe.");
      }
    }
  }

  return (
    <form onSubmit={alterarApostaAPI}>
      <h3>Alterar Aposta</h3>
      
      <div>
        <label>ID da Aposta (a ser alterada)</label>
        <input
          type="number"
          value={idAposta || ''}
          onChange={(e) => setIdAposta(Number(e.target.value))}
          required
        />
      </div>

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
        <label>Novo Resultado (Seleção 1)</label>
        <select 
          value={resultado} 
          onChange={(e) => setResultado(Number(e.target.value) as Resultado)}>
          <option value={Resultado.Vitoria}>Vitória</option>
          <option value={Resultado.Derrota}>Derrota</option>
          <option value={Resultado.Empate}>Empate</option>
        </select>
      </div>

      <div>
        <label>Nova Odd</label>
        <input
          type="number"
          step="0.01"
          min="1.01"
          value={odd}
          onChange={(e) => setOdd(Number(e.target.value))}
          required
        />
      </div>

      <button type="submit" style={{ backgroundColor: 'orange', color: 'white' }}>
        Salvar Alteração
      </button>
      
      {mensagem && <p>{mensagem}</p>}
    </form>
  );
}

export default AlterarPalpite;