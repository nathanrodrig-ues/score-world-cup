import { useState, FormEvent } from "react";
import axios from "axios";

function DeletarPalpite() {
  const [idAposta, setIdAposta] = useState<number>(0);
  const [mensagem, setMensagem] = useState("");

  async function deletarPalpiteAPI(e: FormEvent<HTMLFormElement>) {
    e.preventDefault();
    setMensagem("");

    if (idAposta <= 0) {
      return setMensagem("Informe um ID de palpite válido.");
    }

    if (!window.confirm(`Deseja realmente deletar o palpite ${idAposta}?`)) {
      return;
    }

    try {
      const response = await axios.delete(
        `http://localhost:5047/api/palpite/deletar/${idAposta}`
      );

      setMensagem(String(response.data));
      setIdAposta(0);
    } catch (erro) {
      if (axios.isAxiosError(erro) && erro.response) {
        setMensagem(String(erro.response.data));
      } else {
        setMensagem("Não foi possível conectar à API.");
      }
    }
  }

  return (
    <form onSubmit={deletarPalpiteAPI} className="form-deletar">
      <h3>Deletar Palpite</h3>

      <div>
        <label>ID do Palpite</label>
        <input
          type="number"
          min="1"
          value={idAposta || ""}
          onChange={(e) => setIdAposta(Number(e.target.value))}
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

export default DeletarPalpite;