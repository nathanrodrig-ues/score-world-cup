import { useState, FormEvent } from "react";
import axios from "axios";

function DeletarJogo() {
  const [idJogo, setIdJogo] = useState<number>(0);
  const [mensagem, setMensagem] = useState("");

  async function deletarJogoAPI(e: FormEvent<HTMLFormElement>) {
    e.preventDefault();
    setMensagem("");

    if (idJogo <= 0) {
      return setMensagem("Informe um ID de jogo válido.");
    }

    if (!window.confirm(`Deseja realmente deletar o jogo ${idJogo}?`)) {
      return;
    }

    try {
      const response = await axios.delete(
        `http://localhost:5047/api/jogo/deletar/${idJogo}`
      );

      setMensagem(String(response.data));
      setIdJogo(0);
    } catch (erro) {
      if (axios.isAxiosError(erro) && erro.response) {
        setMensagem(String(erro.response.data));
      } else {
        setMensagem("Não foi possível conectar à API.");
      }
    }
  }

  return (
    <form onSubmit={deletarJogoAPI} className="form-deletar">
      <h3>Deletar Jogo</h3>

      <div>
        <label>ID do Jogo</label>
        <input
          type="number"
          min="1"
          value={idJogo || ""}
          onChange={(e) => setIdJogo(Number(e.target.value))}
          required
        />
      </div>

      <button type="submit" className="btn-deletar">
        Deletar
      </button>

      {mensagem && <p>{mensagem}</p>}
    </form>
  );
}

export default DeletarJogo;