# Livros CRUD em .NET and Angular

# Passos que realizei para criar esta aplicação

## Extensões recomendadas do VS Code

- C# Dev kit: https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csdevkit
- IntelliCode for C# Dev Kit: https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.vscodeintellicode-csharp

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

Criar pasta para api:
`mkdir api && cd api`

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

`git commit -m "feat: Estrutura inicial do api criado por: dotnet new webapi --language C# --auth None --framework net9.0 --use-controllers --name Livros.API"`

Rodar projeto para testar:

`dotnet watch`

Abrir http://localhost:5225/WeatherForecast

# Criar projeto Livros.Data

Agora vamos começar a reorganizar os arquivos em diferentes projetos .NET:

- Livros.API (atual, responde HTTPs)
- Livros.Data (entidades, contexto de banco de dados e migrations)
- Livros.Reports (relatórios)
- Livros.Tests (testes unitários)

Poderíamos ter também um projeto "Livros.Domain" porém para simplificar vamos agrupar no projeto Livros.Data.

Criar projeto Livros.Data:

`cd ..` para voltar para a pasta raiz do projeto .NET.

`dotnet new classlib --language C# --framework net9.0 --name Livros.Data`

`cd Livros.Data`

`dotnet new gitignore`

Apagar arquivo `Class1.cs` pois não será utilizado:

`rm Class1.cs`

Comitar estrutura inicial do projeto Livros.Data:

`git add .`

`git commit -m "feat: Estrutura inicial do projeto Livros.Data criado por: dotnet new classlib --language C# --framework net9.0 --name Livros.Data"`

Mover arquivo WeatherForecast.cs para uma nova pasta Entities do projeto Livros.Data:

`mkdir Entities`

`mv ../Livros.API/WeatherForecast.cs ./Entities/`

Alterar namespace de WeatherForecast.cs para Livros.Data:

`sed -i 's/Livros.API/Livros.Data.Entities/g' Entities/WeatherForecast.cs`

Adicionar projeto Livros.Data como referência no projeto Livros.API:

`cd ../Livros.API`

`dotnet add reference ../Livros.Data/Livros.Data.csproj`

Mover arquivo `/angular-net-livros.sln` para `/api/api.sln`:

`mv angular-net-livros.sln api/api.sln`

Editar o arquivo `api/api.sln` e alterar o caminho do projeto Livros.API para `Livros.API/Livros.API.csproj` e o caminho do projeto Livros.Data para `Livros.Data/Livros.Data.csproj`.

Corrigir namespace de WeatherForecast.cs no aruqivo do controller WeatherForecastController.cs adicionando `using Livros.Data.Entities;`.

Rodar solução com watch:

`cd api`

`dotnet watch --project Livros.API`

Abrir http://localhost:5225/WeatherForecast

Pronto agora temos nossas entidades em um projeto separado e referenciado no projeto da API.

# Passar lógica da controler para o projeto Livros.Data

É preferível encapsular lógica de negócio em classes e funções que podem ser testadas unitariamente e reutilizadas em outros lugares.

Vamos criar uma pasta Services no projeto Livros.Data para abrigar a lógica de negócio.

`mkdir Services`

`touch Services/WeatherForecastService.cs`

Adicionar a classe WeatherForecastService.cs para que contenha um function que retorne uma lista de WeatherForecast conforme o código encontrado originalmente na Controller WeatherForecastController.cs.

Trecho relevante:

```csharp
Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();

```

O código do arquivo WeatherForecastService.cs ficará assim:

```csharp
using Livros.Data.Entities;

namespace Livros.Data.Services;

public class WeatherForecastService
{
	private static readonly string[] Summaries = ["Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"];

	public IEnumerable<WeatherForecast> GetWeatherForecasts()
	{
		return Enumerable.Range(1, 5).Select(index => new WeatherForecast
		{
			Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
			TemperatureC = Random.Shared.Next(-20, 55),
			Summary = Summaries[Random.Shared.Next(Summaries.Length)]
		}).ToArray();
	}
}
```

Crie uma interface IWeatherForecastService.cs para que possamos injetar a dependência de WeatherForecastService em outras classes.

`touch Services/IWeatherForecastService.cs`

O conteúdo do arquivo IWeatherForecastService.cs será:

```csharp
using Livros.Data.Entities;

namespace Livros.Data.Services;

public interface IWeatherForecastService
{
	IEnumerable<WeatherForecast> GetWeatherForecasts();
}
```

Configure injeção de depedência dinâmica de Services na API editando o arquivo Program.cs do projeto Livros.API:

```csharp
var builder = WebApplication.CreateBuilder(args);

// Configura injeção de depedência dinâmica de Services
var servicesAssembly = typeof(Livros.Data.Services.IWeatherForecastService).Assembly;
Console.WriteLine("Configurando Services para DI do projeto Livros.Data: " + servicesAssembly.FullName);
foreach (var type in servicesAssembly.GetTypes()
    .Where(t => t.Namespace == "Livros.Data.Services" && t.IsClass && !t.IsAbstract))
{
    var serviceInterface = type.GetInterfaces().FirstOrDefault();
    if (serviceInterface != null)
    {
        builder.Services.AddScoped(serviceInterface, type);
        Console.WriteLine("Service registrado dinamicamente: " + serviceInterface.Name);
    }
}
```

Altere o controller em WeatherForecastController.cs para chamar o serviço WeatherForecastService:

```csharp
using Microsoft.AspNetCore.Mvc;
using Livros.Data.Services;

namespace Livros.API.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly IWeatherForecastService _weatherForecastService;

    public WeatherForecastController(IWeatherForecastService weatherForecastService)
    {
        _weatherForecastService = weatherForecastService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var forecasts = _weatherForecastService.GetWeatherForecasts();
        return Ok(forecasts);
    }
}

```

A API deve contrinuar functionando na URL http://localhost:5225/WeatherForecast

Comitar alterações:

`cd ../..`

`git add .`

`git commit -m "feat: Lógica de negócio movida para Livros.Data com injeção de dependência de Services"`

# Criar projeto Livros.Tests com xUnit

Agora que temos a API funcionando com lógica de negócio em um projeto separado, vamos criar testes unitários para garantir que a lógica de negócio está funcionando corretamente.

Quando formos implementar a lógica de negócio de verdade, poderemos escrever os testes primeiro seguindo a metodologia TDD (Test Driven Development).

Vamos criar um projeto de testes unitários com xUnit:

`cd api`

`dotnet new xunit --language C# --framework net9.0 --name Livros.Tests`

`cd Livros.Tests`

`dotnet new gitignore`

## Sobre o warning de ClearCache e UpdateApplication

Se aparecer um warning: "Expected to find a static method 'ClearCache' or 'UpdateApplication' on type 'Microsoft.AspNetCore.Mvc.ViewFeatures.HtmlAttributePropertyHelper".

Ignore pois é temporário da Microsoft e foi corrigido em .NET 10:

https://stackoverflow.com/questions/79229624/how-to-add-clearcache-or-updateapplication-methods-to-razor-page
