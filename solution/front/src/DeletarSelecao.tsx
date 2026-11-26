import { useState, FormEvent } from "react";
import axios from "axios";

function DeletarSelecao() {
  const [idSelecao, setIdSelecao] = useState<number>(0);
  const [mensagem, setMensagem] = useState("");

  async function deletarSelecaoAPI(e: FormEvent<HTMLFormElement>) {
    e.preventDefault();
    setMensagem("");

    if (idSelecao <= 0) {
      return setMensagem("Informe um ID de seleção válido.");
    }

    if (!window.confirm(`Deseja realmente deletar a seleção ${idSelecao}?`)) {
      return;
    }

    try {
      const response = await axios.delete(
        `http://localhost:5047/api/selecao/deletar/${idSelecao}`
      );

      setMensagem(String(response.data));
      setIdSelecao(0);
    } catch (erro) {
      if (axios.isAxiosError(erro) && erro.response) {
        setMensagem(String(erro.response.data));
      } else {
        setMensagem("Não foi possível conectar à API.");
      }
    }
  }

  return (
    <form onSubmit={deletarSelecaoAPI} className="form-deletar">
      <h3>Deletar Seleção</h3>

      <div>
        <label>ID da Seleção</label>
        <input
          type="number"
          min="1"
          value={idSelecao || ""}
          onChange={(e) => setIdSelecao(Number(e.target.value))}
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

export default DeletarSelecao;