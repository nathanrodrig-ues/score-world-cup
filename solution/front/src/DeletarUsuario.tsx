import { useState, FormEvent } from "react";
import axios from "axios";

function DeletarUsuario() {
  const [cpfUsuario, setCpfUsuario] = useState("");
  const [mensagem, setMensagem] = useState("");

  async function deletarUsuarioAPI(e: FormEvent<HTMLFormElement>) {
    e.preventDefault();
    setMensagem("");

    const cpf = cpfUsuario.trim();
    if (!cpf) {
      return setMensagem("Informe o CPF do usuário.");
    }

    if (!window.confirm(`Deseja realmente deletar o usuário ${cpf}?`)) {
      return;
    }

    try {
      const response = await axios.delete(
        `http://localhost:5047/api/usuario/deletar/${encodeURIComponent(cpf)}`
      );

      setMensagem(String(response.data));
      setCpfUsuario("");
    } catch (erro) {
      if (axios.isAxiosError(erro) && erro.response) {
        setMensagem(String(erro.response.data));
      } else {
        setMensagem("Não foi possível conectar à API.");
      }
    }
  }

  return (
    <form onSubmit={deletarUsuarioAPI} className="form-deletar">
      <h3>Deletar Usuário</h3>

      <div>
        <label>CPF do usuário</label>
        <input
          type="text"
          value={cpfUsuario}
          onChange={(e) => setCpfUsuario(e.target.value)}
          placeholder="Igual ao cadastrado"
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

export default DeletarUsuario;