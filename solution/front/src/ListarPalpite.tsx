import { useState, useEffect } from "react";
import axios from "axios";
import { Aposta } from "./models/Palpite"; 

function ListarPalpite() {
  const [apostas, setApostas] = useState<Aposta[]>([]);
  const [mensagem, setMensagem] = useState("");

  async function carregarApostas() {
    try {
      const response = await axios.get("http://localhost:5047/api/palpite/listar");
      setApostas(response.data);
    } catch (erro) {
      setMensagem("Erro ao carregar as apostas.");
    }
  }

  useEffect(() => {
    carregarApostas();
  }, []);

  return (
    <div>
      <h3>Lista de Apostas</h3>
      {mensagem && <p>{mensagem}</p>}

      <table border={1} style={{ width: '100%', textAlign: 'left', marginTop: '10px' }}>
        <thead>
          <tr>
            <th>ID</th>
            <th>ID Jogo</th>
            <th>Resultado 1</th>
            <th>Resultado 2</th>
            <th>Odd</th>
          </tr>
        </thead>
        <tbody>
          {apostas.map((aposta) => (
            <tr key={aposta.idAposta}>
              <td>{aposta.idAposta}</td>
              <td>{aposta.idJogo}</td>
              <td>{aposta.resultadoSelecao1}</td>
              <td>{aposta.resultadoSelecao2}</td>
              <td>{aposta.odd}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export default ListarPalpite;