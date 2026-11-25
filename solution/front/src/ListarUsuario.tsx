
import { useState, useEffect } from "react";
import axios from "axios";
import { Usuario } from "./models/Usuario";

function ListarUsuario() {
  const [usuarios, setUsuarios] = useState<Usuario[]>([]);
  const [mensagem, setMensagem] = useState("");

  async function carregarUsuarios() {
    try {
      const response = await axios.get(
        "http://localhost:5047/api/usuario/listar"
      );

      setUsuarios(response.data);
      setMensagem("");
    } catch (erro) {
      setMensagem("Erro ao carregar os usuários.");
    }
  }

  async function deletarUsuario(cpfUsuario: string) {
    const confirmar = window.confirm(
      "Deseja realmente excluir este usuário?"
    );

    if (!confirmar) return;

    try {
      await axios.delete(
        `http://localhost:5047/api/Usuario/deletar/${encodeURIComponent(cpfUsuario)}`
      );

      setMensagem("Usuário excluído com sucesso!");
      carregarUsuarios();
    } catch (erro) {
      setMensagem("Erro ao excluir o usuário.");
    }
  }

  useEffect(() => {
    carregarUsuarios();
  }, []);

  return (
    <div>
      <h3>Lista de Usuários</h3>

      {mensagem && <p>{mensagem}</p>}

      <table
        border={1}
        style={{
          width: "100%",
          textAlign: "left",
          marginTop: "10px"
        }}
      >
        <thead>
          <tr>
            <th>CPF</th>
            <th>Nome</th>
            <th>Método de Pagamento</th>
            <th>Valor Apostado</th>
            <th>Ações</th>
          </tr>
        </thead>

        <tbody>
          {usuarios.map((usuario) => (
            <tr key={usuario.cpfUsuario}>
              <td>{usuario.cpfUsuario}</td>
              <td>{usuario.nameUser}</td>
              <td>{usuario.metodoPagamento}</td>
              <td>R$ {usuario.valorApostado}</td>
              <td>
                <button
                  onClick={() => deletarUsuario(usuario.cpfUsuario)}
                >
                  Excluir
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export default ListarUsuario;
