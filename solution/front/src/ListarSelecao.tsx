import { useState, useEffect } from "react";
import axios from "axios";
import { Selecao } from "./models/Selecao"; // Ajuste o caminho se necessário

function ListarSelecao() {
  const [selecoes, setSelecoes] = useState<Selecao[]>([]);
  const [mensagem, setMensagem] = useState("");

  async function carregarSelecoes() {
    try {
      const response = await axios.get("http://localhost:5047/api/selecao/listar");
      setSelecoes(response.data);
    } catch (erro) {
      setMensagem("Erro ao carregar as seleções.");
    }
  }

  useEffect(() => {
    carregarSelecoes();
  }, []);

  return (
    <div>
      <h3>Lista de Seleções</h3>
      {mensagem && <p>{mensagem}</p>}

      <table border={1} style={{ width: '100%', textAlign: 'left', marginTop: '10px', borderCollapse: 'collapse' }}>
        <thead>
          <tr style={{ backgroundColor: '#f2f2f2' }}>
            <th>ID</th>
            <th>Nome</th>
            <th>Continente</th>
            <th>Títulos</th>
            <th>Participações em Copas</th>
            <th>Grupo</th>
          </tr>
        </thead>
        <tbody>
          {selecoes.map((selecao) => (
            <tr key={selecao.idSelecao}>
              <td>{selecao.idSelecao}</td>
              <td>{selecao.nome}</td>
              <td>{selecao.continente}</td>
              <td>{selecao.titulo}</td>
              <td>{selecao.participacaoEmCopas}</td>
              <td>{selecao.grupo}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export default ListarSelecao;