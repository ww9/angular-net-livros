var builder = WebApplication.CreateBuilder(args);

// Configura injeção de depedência dinâmica de Services
var servicesAssembly = typeof(Livros.Application.Services.IWeatherForecastService).Assembly;
Console.WriteLine("Configurando Services para DI do projeto Livros.Data: " + servicesAssembly.FullName);
foreach (var type in servicesAssembly.GetTypes()
    .Where(t => t.Namespace == "Livros.Application.Services" && t.IsClass && !t.IsAbstract))
{
    var serviceInterface = type.GetInterfaces().FirstOrDefault();
    if (serviceInterface != null)
    {
        builder.Services.AddScoped(serviceInterface, type);
        Console.WriteLine("Service registrado dinamicamente: " + serviceInterface.Name);
    }
}

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

// Exportar a classe Program para ser usada em testes de integração
// Ver: https://learn.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-9.0#basic-tests-with-the-default-webapplicationfactory
public partial class Program { }