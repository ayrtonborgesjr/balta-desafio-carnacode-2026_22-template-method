using ProcessamentoPedidos.Console.Core;

namespace ProcessamentoPedidos.Console.Processors;

public class OnlineOrderProcessor : OrderProcessor
{
    protected override bool Validate(string customerId, decimal amount)
    {
        System.Console.WriteLine("[Online] Validando pedido...");
        if (string.IsNullOrEmpty(customerId))
        {
            System.Console.WriteLine("❌ Cliente inválido");
            return false;
        }

        System.Console.WriteLine("✓ Pedido validado");
        return true;
    }

    protected override void Calculate(decimal amount)
    {
        System.Console.WriteLine("[Online] Calculando valores...");
        decimal shipping = 15.00m;
        decimal total = amount + shipping;

        System.Console.WriteLine($"  → Subtotal: R$ {amount:N2}");
        System.Console.WriteLine($"  → Frete: R$ {shipping:N2}");
        System.Console.WriteLine($"  → Total: R$ {total:N2}");
    }

    protected override void ProcessPayment(decimal amount)
    {
        System.Console.WriteLine("[Online] Processando pagamento com cartão...");
        System.Console.WriteLine("✓ Pagamento aprovado");
    }

    protected override void ScheduleShipping()
    {
        System.Console.WriteLine("[Online] Agendando envio via Correios...");
        System.Console.WriteLine("✓ Envio agendado");
    }

    protected override void Notify()
    {
        System.Console.WriteLine("[Online] Enviando email de confirmação...");
        System.Console.WriteLine("✓ Email enviado");
    }

    protected override string GetChannelName() => "Online";
}