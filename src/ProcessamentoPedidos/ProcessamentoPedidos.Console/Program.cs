using ProcessamentoPedidos.Console.Core;
using ProcessamentoPedidos.Console.Processors;

Console.WriteLine("=== Sistema de Processamento com Template Method ===");

var items = new List<string> { "Notebook", "Mouse", "Teclado" };

OrderProcessor online = new OnlineOrderProcessor();
online.ProcessOrder("CUST001", items, 2500.00m);

OrderProcessor wholesale = new WholesaleOrderProcessor();
wholesale.ProcessOrder("COMP001", items, 5000.00m);

OrderProcessor marketplace = new MarketplaceOrderProcessor();
marketplace.ProcessOrder("SELL001", items, 3000.00m);

Console.WriteLine("\n=== BENEFÍCIOS ===");
Console.WriteLine("✓ Estrutura do algoritmo definida em um único lugar");
Console.WriteLine("✓ Sequência garantida");
Console.WriteLine("✓ Código comum reutilizado");
Console.WriteLine("✓ Fácil adicionar novo canal");
Console.WriteLine("✓ Fácil adicionar novo passo no fluxo");