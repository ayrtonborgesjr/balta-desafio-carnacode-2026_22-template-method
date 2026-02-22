namespace ProcessamentoPedidos.Console.Models;

public class OrderRequest
{
    public required string Id { get; set; }
    public List<string>? Items { get; set; }
    public decimal Amount { get; set; }
}