//using DataRead.AppConfig;
//using DataRead.Application.UseCases;

//var builder = WebApplication.CreateBuilder(args);
//var app = builder.Build();

//app.MapGet("/", () => "Hello World, Hello Giuliano!");

//app.Run();



//// Caminho do arquivo JSON
//string caminhoArquivoJson = @"C:\dev\DataRead\Data\Data.json";

//// Configurar os serviços
//var services = new ServiceCollection();
//services.AddInfrastructure(caminhoArquivoJson);

//// Construir o provedor de serviços
//var serviceProvider = services.BuildServiceProvider();

//// Obter o caso de uso FiltrarProdutosUseCase
//var filterProductUseCase = serviceProvider.GetRequiredService<FilterProductUseCase>();

//// Executar o caso de uso
//var produtosFiltrados = filterProductUseCase.Executar(20000);

//// Exibir os resultados
//foreach (var produto in produtosFiltrados)
//{
//    Console.WriteLine($"Dia: {produto.Dia}, Valor: {produto.Valor}");
//}

//Console.WriteLine("O produto filtrado da posição 4 é:");
//Console.WriteLine(produtosFiltrados[4].Valor);

//// Liberar recursos
//serviceProvider.Dispose();

using DataRead.AppConfig;
using DataRead.Application.UseCases;
using DataRead.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Caminho do arquivo JSON
string caminhoArquivoJson = @"C:\dev\DataRead\Data\Data.json";

// Configurar os serviços e a injeção de dependências
builder.Services.AddInfrastructure(caminhoArquivoJson);

var app = builder.Build();

// Definir uma rota para verificar a execução do caso de uso
app.MapGet("/", async (FilterProductUseCase filterProductUseCase) =>
{
    // Executar o caso de uso
    var produtosFiltrados = filterProductUseCase.Executar(20000);

    // Exibir os resultados
    foreach (var produto in produtosFiltrados)
    {
        Console.WriteLine($"Dia: {produto.Dia}, Valor: {produto.Valor}");
    }

    Console.WriteLine("O produto filtrado da posição 4 é:");
    Console.WriteLine(produtosFiltrados[4].Valor);

    return "Produtos filtrados processados com sucesso!";
});

app.Run();


