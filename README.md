Sistema de Pontuação da Copa do Mundo



Sobre o projeto



Este projeto foi desenvolvido em equipe com o objetivo de criar uma API para gerenciar seleções, jogos, usuários e palpites relacionados à Copa do Mundo.



A aplicação foi desenvolvida em C# utilizando ASP.NET Core, permitindo organizar as informações do sistema e realizar operações relacionadas aos jogos e aos palpites.



Funcionalidades



\- Cadastro e gerenciamento de seleções.

\- Cadastro e gerenciamento de usuários.

\- Cadastro e gerenciamento de jogos.

\- Registro e gerenciamento de palpites.

\- Armazenamento de informações em banco de dados.

\- Integração com banco de dados SQLite.



Tecnologias utilizadas



\- C#

\- .NET 8

\- ASP.NET Core

\- Entity Framework Core

\- EntityFrameworkCore.Designer

\- SQLite

\- Git

\- GitHub



Bibliotecas utilizadas



Entity Framework Core: biblioteca utilizada para facilitar o acesso e o gerenciamento dos dados do banco de dados por meio de objetos C#.



EntityFrameworkCore.Designer: biblioteca relacionada às ferramentas de design e suporte utilizadas no desenvolvimento com Entity Framework Core.



SQLite: banco de dados leve utilizado para armazenar as informações da aplicação.



Estrutura do projeto



Data: contém arquivos relacionados à configuração e ao acesso ao banco de dados.



Migrations: contém os arquivos de migração utilizados para acompanhar alterações na estrutura do banco de dados.



Models: contém as classes que representam os dados do sistema, como seleções, jogos e palpites.



REST: contém arquivos utilizados para testar as requisições HTTP da API.



Program.cs: arquivo responsável pela configuração e inicialização da aplicação.



Como executar o projeto



Requisitos



É necessário ter o Git e o SDK do .NET instalados.



1\. Clonar o repositório



git clone https://github.com/nathanrodrig-ues/score-world-cup.git



2\. Acessar a pasta da API



cd score-world-cup/solution/API



3\. Restaurar as dependências



dotnet restore



4\. Executar a aplicação



dotnet run



Após executar o comando, o terminal informará o endereço local em que a API estará disponível.



Testes



Os arquivos de requisições HTTP presentes na pasta REST podem ser utilizados para testar os endpoints da API em um editor compatível.



Desenvolvimento



O projeto foi desenvolvido em equipe, utilizando Git e GitHub para o controle de versão e o compartilhamento do código-fonte.



Licença



O projeto ainda não possui uma licença definida.

