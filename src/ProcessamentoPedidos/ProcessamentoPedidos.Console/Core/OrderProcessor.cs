namespace ProcessamentoPedidos.Console.Core;

public abstract class OrderProcessor
{
    // TEMPLATE METHOD (não pode ser alterado pelas subclasses)
    public void ProcessOrder(string id, List<string> items, decimal amount)
    {
        System.Console.WriteLine($"\n=== Processando Pedido {GetChannelName()} ===");

        if (!Validate(id, amount))
            return;

        CheckStock(items);

        Calculate(amount);

        ProcessPayment(amount);

        SeparateItems();

        ScheduleShipping();

        Notify();

        System.Console.WriteLine($"\n✅ Pedido {GetChannelName()} processado com sucesso!");
    }

    // Passos fixos (comuns a todos)
    protected virtual void CheckStock(List<string> items)
    {
        System.Console.WriteLine($"[{GetChannelName()}] Verificando estoque...");
        foreach (var item in items)
        {
            System.Console.WriteLine($"  → {item}: Disponível");
        }
        System.Console.WriteLine("✓ Estoque confirmado");
    }

    protected virtual void SeparateItems()
    {
        System.Console.WriteLine($"[{GetChannelName()}] Separando itens no estoque...");
        System.Console.WriteLine("✓ Itens separados");
    }

    // Passos obrigatórios para customização
    protected abstract bool Validate(string id, decimal amount);
    protected abstract void Calculate(decimal amount);
    protected abstract void ProcessPayment(decimal amount);
    protected abstract void ScheduleShipping();
    protected abstract void Notify();
    protected abstract string GetChannelName();
}