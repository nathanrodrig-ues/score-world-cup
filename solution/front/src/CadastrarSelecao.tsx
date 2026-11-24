import { useState, FormEvent } from "react";
import axios from "axios";
import { Selecao } from "./models/Selecao"; // Ajuste o caminho se o seu modelo estiver em outra pasta

function CadastrarSelecao() {
  const [nome, setNome] = useState("");
  const [continente, setContinente] = useState("");
  const [titulo, setTitulo] = useState<number>(0);
  const [participacaoEmCopas, setParticipacaoEmCopas] = useState<number>(0);
  const [grupo, setGrupo] = useState("");
  const [mensagem, setMensagem] = useState("");

  async function enviarSelecaoAPI(e: FormEvent<HTMLFormElement>) {
    e.preventDefault();
    setMensagem("");

    // Monta o objeto exatamente como o C# espera
    const selecao: Selecao = { 
      nome, 
      continente, 
      titulo, 
      participacaoEmCopas, 
      grupo 
    };

    try {
      await axios.post("http://localhost:5047/api/selecao/cadastrar", selecao);
      
      setMensagem("Seleção cadastrada com sucesso!");
      setNome(""); 
      setContinente(""); 
      setTitulo(0); 
      setParticipacaoEmCopas(0); 
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
      <h3>Cadastrar Seleção</h3>
      
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
        <label>Continente</label>
        <input
          type="text"
          value={continente}
          onChange={(e) => setContinente(e.target.value)}
          required
        />
      </div>

      <div>
        <label>Títulos</label>
        <input
          type="number"
          value={titulo}
          onChange={(e) => setTitulo(Number(e.target.value))}
          required
        />
      </div>

      <div>
        <label>Participações em Copas</label>
        <input
          type="number"
          value={participacaoEmCopas}
          onChange={(e) => setParticipacaoEmCopas(Number(e.target.value))}
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