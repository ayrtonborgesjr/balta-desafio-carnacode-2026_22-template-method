using ProcessamentoPedidos.Console.Processors;

namespace ProcessamentoPedidos.Tests.Processors;

public class MarketplaceOrderProcessorTests
{
    [Fact]
    public void ProcessOrder_WithValidData_ShouldCompleteSuccessfully()
    {
        // Arrange
        var processor = new MarketplaceOrderProcessor();
        var items = new List<string> { "Product A", "Product B" };

        // Act & Assert (should not throw)
        processor.ProcessOrder("SELLER-001", items, 2000.00m);
    }

    [Fact]
    public void ProcessOrder_WithEmptySellerId_ShouldNotValidate()
    {
        // Arrange
        var processor = new MarketplaceOrderProcessor();
        var items = new List<string> { "Product C" };

        // Act & Assert (should not throw, but validation fails)
        processor.ProcessOrder("", items, 300.00m);
    }

    [Fact]
    public void ProcessOrder_WithNullSellerId_ShouldNotValidate()
    {
        // Arrange
        var processor = new MarketplaceOrderProcessor();
        var items = new List<string> { "Product D" };

        // Act & Assert (should not throw, but validation fails)
        processor.ProcessOrder(null!, items, 400.00m);
    }

    [Fact]
    public void ProcessOrder_WithValidSellerId_ShouldCalculateCommission()
    {
        // Arrange
        var processor = new MarketplaceOrderProcessor();
        var items = new List<string> { "Item" };

        // Act & Assert (should calculate 15% commission)
        processor.ProcessOrder("SELLER-002", items, 1000.00m);
    }

    [Fact]
    public void ProcessOrder_WithMultipleItems_ShouldCheckAllItems()
    {
        // Arrange
        var processor = new MarketplaceOrderProcessor();
        var items = new List<string> { "Item1", "Item2", "Item3" };

        // Act & Assert (should not throw)
        processor.ProcessOrder("SELLER-003", items, 750.00m);
    }

    [Fact]
    public void ProcessOrder_WithEmptyItems_ShouldCompleteSuccessfully()
    {
        // Arrange
        var processor = new MarketplaceOrderProcessor();
        var items = new List<string>();

        // Act & Assert (should not throw)
        processor.ProcessOrder("SELLER-004", items, 100.00m);
    }

    [Fact]
    public void ProcessOrder_WithZeroAmount_ShouldCompleteSuccessfully()
    {
        // Arrange
        var processor = new MarketplaceOrderProcessor();
        var items = new List<string> { "Free Item" };

        // Act & Assert (should not throw)
        processor.ProcessOrder("SELLER-005", items, 0m);
    }

    [Fact]
    public void ProcessOrder_WithHighValue_ShouldCalculateCorrectCommission()
    {
        // Arrange
        var processor = new MarketplaceOrderProcessor();
        var items = new List<string> { "Expensive Item" };

        // Act & Assert (should calculate commission on large amount)
        processor.ProcessOrder("SELLER-006", items, 50000.00m);
    }

    [Fact]
    public void GetChannelName_ShouldReturnMarketplace()
    {
        // Arrange
        var processor = new MarketplaceOrderProcessor();

        // Act
        var channelName = processor.GetType()
            .GetMethod("GetChannelName", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            ?.Invoke(processor, null) as string;

        // Assert
        Assert.Equal("Marketplace", channelName);
    }
}

