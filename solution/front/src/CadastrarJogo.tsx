import { useState, FormEvent } from "react";
import axios from "axios";
import { Jogo } from "./models/Jogo";

function CadastrarJogo() {
  const [cpfUsuario, setCpfUsuario] = useState("");
  const [idSelecao1, setIdSelecao1] = useState<number>(0);
  const [idSelecao2, setIdSelecao2] = useState<number>(0);
  const [mensagem, setMensagem] = useState("");

  async function enviarJogoAPI(e: FormEvent<HTMLFormElement>) {
    e.preventDefault();
    setMensagem("");

    // Validação simples para não enviar IDs iguais
    if (idSelecao1 === idSelecao2 && idSelecao1 !== 0) {
      return setMensagem("As seleções 1 e 2 devem ser diferentes.");
    }

    const jogo: Jogo = { 
      cpfUsuario, 
      idSelecao1, 
      idSelecao2 
    };

    try {
      // Ajuste a URL se o seu Controller tiver um nome diferente (ex: /api/jogo/cadastrar)
      await axios.post("http://localhost:5047/api/jogo/cadastrar", jogo);
      
      setMensagem("Jogo cadastrado com sucesso!");
      
      // Limpando o formulário
      setCpfUsuario("");
      setIdSelecao1(0);
      setIdSelecao2(0);
    } catch (erro) {
      if (axios.isAxiosError(erro) && erro.response) {
        setMensagem(String(erro.response.data));
      } else {
        setMensagem("Não foi possível conectar à API.");
      }
    }
  }

  return (
    <form onSubmit={enviarJogoAPI}>
      <h1>Cadastrar Jogo</h1>
      
      <div>
        <label>CPF do Usuário (Criador)</label>
        <input
          type="text"
          value={cpfUsuario}
          onChange={(e) => setCpfUsuario(e.target.value)}
          placeholder="000.000.000-00"
          required
        />
      </div>

      <div>
        <label>ID da Seleção 1</label>
        <input
          type="number"
          value={idSelecao1 || ''}
          onChange={(e) => setIdSelecao1(Number(e.target.value))}
          required
        />
      </div>

      <div>
        <label>ID da Seleção 2</label>
        <input
          type="number"
          value={idSelecao2 || ''}
          onChange={(e) => setIdSelecao2(Number(e.target.value))}
          required
        />
      </div>

      <button type="submit">Cadastrar</button>
      {mensagem && <p>{mensagem}</p>}
    </form>
  );
}

export default CadastrarJogo;