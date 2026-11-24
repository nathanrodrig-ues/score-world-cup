import { useState, useEffect } from "react";
import axios from "axios";
import { Jogo } from "./models/Jogo";

function ListarJogo() {
  const [jogos, setJogos] = useState<Jogo[]>([]);
  const [mensagem, setMensagem] = useState("");

  async function carregarJogos() {
    try {
      const response = await axios.get("http://localhost:5047/api/jogo/listar");
      setJogos(response.data);
    } catch (erro) {
      setMensagem("Erro ao carregar os jogos.");
    }
  }

  useEffect(() => { carregarJogos(); }, []);

  return (
    <div>
      <h3>Lista de Jogos</h3>
      {mensagem && <p>{mensagem}</p>}
      <table border={1} style={{ width: '100%', textAlign: 'left', marginTop: '10px' }}>
        <thead>
          <tr>
            <th>ID Jogo</th><th>CPF do Usuário</th><th>ID Seleção 1</th><th>ID Seleção 2</th>
          </tr>
        </thead>
        <tbody>
          {jogos.map((j) => (
            <tr key={j.idJogo}>
              <td>{j.idJogo}</td><td>{j.cpfUsuario}</td>
              <td>{j.idSelecao1}</td><td>{j.idSelecao2}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}
export default ListarJogo;