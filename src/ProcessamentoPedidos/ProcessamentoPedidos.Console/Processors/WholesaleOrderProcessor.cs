using ProcessamentoPedidos.Console.Core;

namespace ProcessamentoPedidos.Console.Processors;

public class WholesaleOrderProcessor : OrderProcessor
{
    protected override bool Validate(string companyId, decimal amount)
    {
        System.Console.WriteLine("[Atacado] Validando pedido...");

        if (string.IsNullOrEmpty(companyId))
        {
            System.Console.WriteLine("❌ Empresa inválida");
            return false;
        }

        if (amount < 1000.00m)
        {
            System.Console.WriteLine("❌ Pedido mínimo de R$ 1.000,00 para atacado");
            return false;
        }

        System.Console.WriteLine("✓ Pedido validado");
        return true;
    }

    protected override void Calculate(decimal amount)
    {
        System.Console.WriteLine("[Atacado] Calculando valores...");
        decimal discount = amount * 0.10m;
        decimal total = amount - discount;

        System.Console.WriteLine($"  → Subtotal: R$ {amount:N2}");
        System.Console.WriteLine($"  → Desconto (10%): -R$ {discount:N2}");
        System.Console.WriteLine($"  → Total: R$ {total:N2}");
    }

    protected override void ProcessPayment(decimal amount)
    {
        System.Console.WriteLine("[Atacado] Gerando boleto bancário...");
        System.Console.WriteLine("✓ Boleto gerado");
    }

    protected override void ScheduleShipping()
    {
        System.Console.WriteLine("[Atacado] Agendando coleta com transportadora...");
        System.Console.WriteLine("✓ Coleta agendada");
    }

    protected override void Notify()
    {
        System.Console.WriteLine("[Atacado] Notificando empresa...");
        System.Console.WriteLine("  → Email enviado");
        System.Console.WriteLine("  → SMS enviado");
        System.Console.WriteLine("✓ Notificações enviadas");
    }

    protected override string GetChannelName() => "Atacado";
}