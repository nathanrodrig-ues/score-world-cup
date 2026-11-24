import { useState, FormEvent } from "react";
import axios from "axios";
import { Selecao } from "./models/Selecao";

function CadastrarSelecao() {
  const [nome, setNome] = useState("");
  const [grupo, setGrupo] = useState("");
  const [mensagem, setMensagem] = useState("");

  async function enviarSelecaoAPI(e: FormEvent<HTMLFormElement>) {
    e.preventDefault();
    setMensagem("");

    const selecao: Selecao = { nome, grupo };

    try {
      await axios.post("http://localhost:5047/api/selecao/cadastrar", selecao);
      setMensagem("Seleção cadastrada com sucesso!");
      setNome("");
      setGrupo("");
    } catch (erro) {
      if (axios.isAxiosError(erro) && erro.response) {
        setMensagem(String(erro.response.data));
      } else {
        setMensagem("Não foi possível conectar à API.");
      }
    }
  }

  return (
    <form onSubmit={enviarSelecaoAPI}>
      <h1>Cadastrar Seleção</h1>
      <div>
        <label>Nome</label>
        <input
          type="text"
          value={nome}
          onChange={(e) => setNome(e.target.value)}
          required
        />
      </div>
      <div>
        <label>Grupo</label>
        <input
          type="text"
          value={grupo}
          maxLength={1}
          onChange={(e) => setGrupo(e.target.value)}
          required
        />
      </div>
      <button type="submit">Cadastrar</button>
      {mensagem && <p>{mensagem}</p>}
    </form>
  );
}

export default CadastrarSelecao;