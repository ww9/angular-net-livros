# Livros CRUD em .NET and Angular

# Passos que realizei para criar esta aplicação

## Incializar git no projeto

`git init --initial-branch=main`

`git config user.name "Bruno Cassol"`

`git config user.email "brunocassol+ww9@gmail.com"`

## Configuração de ambiente Ubuntu 20.04 em WSL2

Instalar repositorio de pacotes da Microsoft no Ubuntu:

`cd ~ && curl -sSL -O https://packages.microsoft.com/config/ubuntu/20.04/packages-microsoft-prod.deb`

`sudo dpkg -i packages-microsoft-prod.deb`

`rm packages-microsoft-prod.deb`

Instalar .NET 9:

`sudo apt-get update && sudo apt-get install -y dotnet-sdk-9.0`

Adicionar certificados para TLS local:

`dotnet dev-certs https --trust`

# Criar projeto .NET para API

Criar pasta para backend:
`mkdir backend && cd backend`

Atualizar lista de workloads:
`dotnet workload update`

Listar templates:
`dotnet new list`

Listar opções para template Web API:
`dotnet new webapi --help`

Criar projeto com as opções desejadas:

`dotnet new webapi --language C# --auth None --framework net9.0 --use-controllers --name Livros.API`

Criar arquivo .gitignore com dotnet CLI:

`dotnet new gitignore`

Anotações até aqui:

- O arquivo `Properties/launchSettings.json` é utilizado pelo Visual Studio porém não funciona com o VS Code.
  - Devemos então criar um arquivo `.vscode/launchSettings.json` com o conteúdo similar ao arquivo `Properties/launchSettings.json`.
- O comando criou um entidade `WeatherForecast.cs` na raiz do projeto. Mas iremos armazenar entidades em outro projeto chamado Livros.Data que será criado posteriormente.
- Por padrão o arquivo appsettings.Development.json será comitado. Devemos excluir este arquivo do controle de versão pois deve ser usado apenas para desenvolvimento local e varia entre desenvolvedores.
  - .NET escolhe qual arquivo appsettings usar baseado na variável de ambiente ASPNETCORE_ENVIRONMENT. Se ela estiver setada como "Development" então o arquivo appsettings.Development.json será carregado e poderá sobrescrever valores do arquivo `appsettings.json`.

Excluir `appsettings.Development.json` do controle de versão:

`echo "appsettings.Development.json" >> .gitignore`

Comitar estrutura inicial do projeto:

`git add .`

`git commit -m "feat: Estrutura inicial do backend criado por: dotnet new webapi --language C# --auth None --framework net9.0 --use-controllers --name Livros.API"`

Rodar projeto para testar:

`dotnet watch`

Abrir http://localhost:5225/WeatherForecast

Agora vamos começar a reorganizar os arquivos em diferentes projetos .NET:

- Livros.API (atual, responde HTTPs)
- Livros.Data (entidades, contexto de banco de dados e migrations)
- Livros.Reports (relatórios)
- Livros.Tests (testes unitários)

Poderíamos ter também um projeto "Livros.Domain" porém para simplificar vamos agrupar no projeto Livros.Data.

Criar projeto Licros.Data:

`cd ..` para voltar para a pasta raiz do projeto .NET.

`dotnet new classlib --language C# --framework net9.0 --name Livros.Data`

`cd Livros.Data`

`dotnet new gitignore`

Comitar estrutura inicial do projeto Livros.Data:

`git add .`

`git commit -m "feat: Estrutura inicial do projeto Livros.Data criado por: dotnet new classlib --language C# --framework net9.0 --name Livros.Data"`

Mover arquivo WeatherForecast.cs para a pasta Livros.Data:

`mv ../Livros.API/WeatherForecast.cs ./`

Alterar namespace de WeatherForecast.cs para Livros.Data:

`sed -i 's/Livros.API/Livros.Data/g' WeatherForecast.cs`
