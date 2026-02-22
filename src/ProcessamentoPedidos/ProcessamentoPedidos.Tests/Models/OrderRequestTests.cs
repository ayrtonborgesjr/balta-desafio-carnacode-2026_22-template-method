using ProcessamentoPedidos.Console.Models;

namespace ProcessamentoPedidos.Tests.Models;

public class OrderRequestTests
{
    [Fact]
    public void OrderRequest_ShouldCreateInstanceWithRequiredProperties()
    {
        // Arrange & Act
        var order = new OrderRequest
        {
            Id = "ORD-001",
            Amount = 100.00m,
            Items = new List<string> { "Item1", "Item2" }
        };

        // Assert
        Assert.NotNull(order);
        Assert.Equal("ORD-001", order.Id);
        Assert.Equal(100.00m, order.Amount);
        Assert.NotNull(order.Items);
        Assert.Equal(2, order.Items.Count);
    }

    [Fact]
    public void OrderRequest_ShouldAllowNullItems()
    {
        // Arrange & Act
        var order = new OrderRequest
        {
            Id = "ORD-002",
            Amount = 50.00m,
            Items = null
        };

        // Assert
        Assert.NotNull(order);
        Assert.Equal("ORD-002", order.Id);
        Assert.Equal(50.00m, order.Amount);
        Assert.Null(order.Items);
    }

    [Fact]
    public void OrderRequest_ShouldAllowEmptyItemsList()
    {
        // Arrange & Act
        var order = new OrderRequest
        {
            Id = "ORD-003",
            Amount = 0m,
            Items = new List<string>()
        };

        // Assert
        Assert.NotNull(order);
        Assert.Equal("ORD-003", order.Id);
        Assert.Equal(0m, order.Amount);
        Assert.NotNull(order.Items);
        Assert.Empty(order.Items);
    }

    [Fact]
    public void OrderRequest_ShouldAllowNegativeAmount()
    {
        // Arrange & Act
        var order = new OrderRequest
        {
            Id = "ORD-004",
            Amount = -10.00m
        };

        // Assert
        Assert.Equal(-10.00m, order.Amount);
    }
}

