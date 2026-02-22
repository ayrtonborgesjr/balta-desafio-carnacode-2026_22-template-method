using ProcessamentoPedidos.Console.Core;

namespace ProcessamentoPedidos.Console.Processors;

public class MarketplaceOrderProcessor : OrderProcessor
{
    protected override bool Validate(string sellerId, decimal amount)
    {
        System.Console.WriteLine("[Marketplace] Validando pedido...");
        if (string.IsNullOrEmpty(sellerId))
        {
            System.Console.WriteLine("❌ Vendedor inválido");
            return false;
        }

        System.Console.WriteLine("✓ Pedido validado");
        return true;
    }

    protected override void Calculate(decimal amount)
    {
        System.Console.WriteLine("[Marketplace] Calculando valores...");
        decimal commission = amount * 0.15m;
        decimal sellerAmount = amount - commission;

        System.Console.WriteLine($"  → Valor total: R$ {amount:N2}");
        System.Console.WriteLine($"  → Comissão (15%): R$ {commission:N2}");
        System.Console.WriteLine($"  → Repasse vendedor: R$ {sellerAmount:N2}");
    }

    protected override void ProcessPayment(decimal amount)
    {
        System.Console.WriteLine("[Marketplace] Processando split payment...");
        System.Console.WriteLine("✓ Pagamento dividido");
    }

    protected override void ScheduleShipping()
    {
        System.Console.WriteLine("[Marketplace] Agendar envio com opção do vendedor...");
        System.Console.WriteLine("✓ Envio agendado");
    }

    protected override void Notify()
    {
        System.Console.WriteLine("[Marketplace] Notificando partes...");
        System.Console.WriteLine("  → Cliente notificado");
        System.Console.WriteLine("  → Vendedor notificado");
        System.Console.WriteLine("✓ Notificações enviadas");
    }

    protected override string GetChannelName() => "Marketplace";
}