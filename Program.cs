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
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.DependencyInjection;
using System;

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

    //Questão 01
    int INDICE = 13, SOMA = 0, K = 0;
    while (K < INDICE)
    {
        K = K + 1;
        SOMA = SOMA + K;
    }

    //Questão 02
    int numeroInformado = 21; // Número informado para verificação
    List<int> fibonacciSequence = new List<int> { 0, 1 };
    while (fibonacciSequence[^1] + fibonacciSequence[^2] <= numeroInformado)
    {
        fibonacciSequence.Add(fibonacciSequence[^1] + fibonacciSequence[^2]);
    }
    bool pertence = fibonacciSequence.Contains(numeroInformado);






    //Questão  05

    string input = "Target";
    char[] chars = input.ToCharArray();

    // Usando dois índices para realizar a troca
    int left = 0;
    int right = chars.Length - 1;

    while (left < right)
    {
        
        char temp = chars[left];
        chars[left] = chars[right];
        chars[right] = temp;

        
        left++;
        right--;
    }

    Console.WriteLine(chars.ToString());

    var diasComFaturamento = produtosFiltrados
        .Where(produto => produto.Valor > 0) // Ignora dias com faturamento 0
        .ToList();

    double menorValor = diasComFaturamento.Min(produto => produto.Valor);
    double maiorValor = diasComFaturamento.Max(produto => produto.Valor);
    double mediaMensal = diasComFaturamento.Average(produto => produto.Valor);
    int diasAcimaDaMedia = diasComFaturamento.Count(produto => produto.Valor > mediaMensal);

    // Questão 04
    var faturamentoPorEstado = new Dictionary<string, double>
    {
        { "SP", 67836.43 },
        { "RJ", 36678.66 },
        { "MG", 29229.88 },
        { "ES", 27165.48 },
        { "Outros", 19849.53 }
    };

    double totalFaturamento = faturamentoPorEstado.Values.Sum();
    var percentuaisPorEstado = faturamentoPorEstado
        .Select(kvp => new { Estado = kvp.Key, Percentual = (kvp.Value / totalFaturamento) * 100 })
        .ToList();

    // Montar o HTML de retorno
    var htmlBuilder = new System.Text.StringBuilder();
    htmlBuilder.Append("<!DOCTYPE html>");
    htmlBuilder.Append("<html lang='en'>");
    htmlBuilder.Append("<head>");
    htmlBuilder.Append("<meta charset='UTF-8'>");
    htmlBuilder.Append("<meta name='viewport' content='width=device-width, initial-scale=1.0'>");
    htmlBuilder.Append("<title>Análise de Faturamento</title>");
    htmlBuilder.Append("<style>body { font-family: Arial; margin: 20px; }</style>");
    htmlBuilder.Append("</head>");
    htmlBuilder.Append("<body>");
    htmlBuilder.Append("<h1>Análise de Faturamento</h1>");

    // Resultado b) Faturamento diário
    htmlBuilder.Append("<h2>Faturamento Diário</h2>");
    htmlBuilder.Append($"<p>Menor valor de faturamento: {menorValor:C}</p>");
    htmlBuilder.Append($"<p>Maior valor de faturamento: {maiorValor:C}</p>");
    htmlBuilder.Append($"<p>Média mensal de faturamento: {mediaMensal:C}</p>");
    htmlBuilder.Append($"<p>Dias com faturamento acima da média: {diasAcimaDaMedia}</p>");

    // Resultado 4) Percentuais por estado
    htmlBuilder.Append("<h2>Percentual de Faturamento por Estado</h2>");
    htmlBuilder.Append("<ul>");
    foreach (var estado in percentuaisPorEstado)
    {
        htmlBuilder.Append($"<li>{estado.Estado}: {estado.Percentual:F2}%</li>");
    }
    htmlBuilder.Append("</ul>");

    htmlBuilder.Append("</body>");
    htmlBuilder.Append("</html>");

    return Results.Text(htmlBuilder.ToString(), "text/html");

    
});

app.Run();


