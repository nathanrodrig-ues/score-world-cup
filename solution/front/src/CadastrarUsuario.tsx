
import { useState, FormEvent } from "react";
import axios from "axios";
import { Usuario } from "./models/Usuario";

function CadastrarUsuario() {
  const [cpfUsuario, setCpfUsuario] = useState("");
  const [nameUser, setNameUser] = useState("");
  const [metodoPagamento, setMetodoPagamento] = useState("");
  const [valorApostado, setValorApostado] = useState<number>(0);
  const [mensagem, setMensagem] = useState("");

  async function cadastrarUsuario(e: FormEvent<HTMLFormElement>) {
    e.preventDefault();
    setMensagem("");

    const usuario: Usuario = {
      cpfUsuario,
      nameUser,
      metodoPagamento,
      valorApostado
    };

    try {
      await axios.post(
        "http://localhost:5047/api/usuario/cadastrar",
        usuario
      );

      setMensagem("Usuário cadastrado com sucesso!");

      setCpfUsuario("");
      setNameUser("");
      setMetodoPagamento("");
      setValorApostado(0);

    } catch (erro) {
      if (axios.isAxiosError(erro) && erro.response) {
        setMensagem(String(erro.response.data));
      } else {
        setMensagem("Não foi possível conectar à API.");
      }
    }
  }

  return (
    <form onSubmit={cadastrarUsuario}>
      <h2>Cadastrar Usuário</h2>

      <div>
        <label>CPF:</label>
        <input
          type="text"
          value={cpfUsuario}
          onChange={(e) => setCpfUsuario(e.target.value)}
          required
        />
      </div>

      <div>
        <label>Nome:</label>
        <input
          type="text"
          value={nameUser}
          onChange={(e) => setNameUser(e.target.value)}
          required
        />
      </div>

      <div>
        <label>Método de pagamento:</label>
        <input
          type="text"
          value={metodoPagamento}
          onChange={(e) => setMetodoPagamento(e.target.value)}
          required
        />
      </div>

      <div>
        <label>Valor apostado:</label>
        <input
          type="number"
          min="10.01"
          step="0.01"
          value={valorApostado || ""}
          onChange={(e) => setValorApostado(Number(e.target.value))}
          required
        />
      </div>

      <button type="submit">Cadastrar Usuário</button>

      {mensagem && <p>{mensagem}</p>}
    </form>
  );
}

export default CadastrarUsuario;
