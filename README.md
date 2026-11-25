Sistema de Pontuação da Copa do Mundo

Sobre o projeto

Este projeto foi desenvolvido em equipe com o objetivo de criar uma aplicação completa (Fullstack) para gerenciar seleções, jogos, usuários e palpites (apostas) relacionados à Copa do Mundo.

A aplicação é dividida em duas partes integradas: um Backend desenvolvido em C# utilizando ASP.NET Core (responsável pelas regras de negócio e persistência) e um Frontend desenvolvido em React com TypeScript (responsável pela interface visual), permitindo organizar as informações do sistema e realizar operações relacionadas aos jogos e aos palpites.

Funcionalidades

- Cadastro e gerenciamento de seleções.
- Cadastro e gerenciamento de usuários.
- Cadastro e gerenciamento de jogos.
- Registro e gerenciamento de palpites (CRUD completo: cadastro, listagem, alteração e exclusão).
- Armazenamento de informações em banco de dados.
- Integração com banco de dados SQLite.
- Interface visual moderna e responsiva.

Tecnologias utilizadas

Backend:
- C#
- .NET 8
- ASP.NET Core
- Entity Framework Core
- EntityFrameworkCore.Designer
- SQLite

Frontend:
- React (Create React App)
- TypeScript
- Axios
- CSS (gerado com auxílio de Inteligência Artificial)

Versionamento:
- Git
- GitHub

Bibliotecas utilizadas

Entity Framework Core: biblioteca utilizada para facilitar o acesso e o gerenciamento dos dados do banco de dados por meio de objetos C#.

EntityFrameworkCore.Designer: biblioteca relacionada às ferramentas de design e suporte utilizadas no desenvolvimento com Entity Framework Core.

SQLite: banco de dados leve utilizado para armazenar as informações da aplicação.

Axios: cliente HTTP baseado em promises para fazer as requisições à API no React.

Estrutura do projeto

Data: contém arquivos relacionados à configuração e ao acesso ao banco de dados.

Migrations: contém os arquivos de migração utilizados para acompanhar alterações na estrutura do banco de dados.

Models (API): contém as classes que representam os dados do sistema, como seleções, jogos e palpites.

REST: contém arquivos utilizados para testar as requisições HTTP da API.

Program.cs: arquivo responsável pela configuração e inicialização da aplicação.

front: pasta do frontend em React. Contém os arquivos de interface (src), incluindo:
- App.tsx: componente principal.
- CadastrarSelecao.tsx, ListarSelecao.tsx
- CadastrarJogo.tsx, ListarJogo.tsx
- CadastrarUsuario.tsx, ListarUsuario.tsx
- CadastrarPalpite.tsx, ListarPalpite.tsx, AlterarPalpite.tsx, DeletarPalpite.tsx
- models: interfaces TypeScript dos modelos.
- index.css e index.tsx: estilos globais e ponto de entrada do React.

Como executar o projeto

Requisitos

É necessário ter o Git, o SDK do .NET 8 e o Node.js instalados.

1. Clonar o repositório

git clone https://github.com/nathanrodrig-ues/score-world-cup.git

2. Executar o Backend (API)

Acesse a pasta da API:

cd score-world-cup/solution/API

Restaure as dependências:

dotnet restore

Execute a aplicação:

dotnet run

Após executar o comando, o terminal informará o endereço local em que a API estará disponível (ex: http://localhost:5047).

3. Executar o Frontend (React)

Abra outro terminal e acesse a pasta do frontend:

cd score-world-cup/solution/front

Instale as dependências do projeto:

npm install

Instale a biblioteca Axios (para requisições HTTP):

npm install axios

Inicie o projeto:

npm start

O frontend abrirá automaticamente em http://localhost:3000.

Testes

Os arquivos de requisições HTTP presentes na pasta REST podem ser utilizados para testar os endpoints da API de forma isolada em um editor compatível (como o VS Code com a extensão REST Client).

Desenvolvimento

O projeto foi desenvolvido em equipe, utilizando Git e GitHub para o controle de versão e o compartilhamento do código-fonte. Foram utilizadas branches separadas para o desenvolvimento do frontend e backend, garantindo a organização e colaboração de todos os integrantes.

Licença

O projeto ainda não possui uma licença definida.
