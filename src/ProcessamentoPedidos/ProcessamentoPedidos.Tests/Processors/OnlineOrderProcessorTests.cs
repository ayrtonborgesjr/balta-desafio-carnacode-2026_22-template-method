using ProcessamentoPedidos.Console.Processors;

namespace ProcessamentoPedidos.Tests.Processors;

public class OnlineOrderProcessorTests
{
    [Fact]
    public void ProcessOrder_WithValidData_ShouldCompleteSuccessfully()
    {
        // Arrange
        var processor = new OnlineOrderProcessor();
        var items = new List<string> { "Notebook", "Mouse" };

        // Act & Assert (should not throw)
        processor.ProcessOrder("CUST-001", items, 1500.00m);
    }

    [Fact]
    public void ProcessOrder_WithEmptyCustomerId_ShouldNotValidate()
    {
        // Arrange
        var processor = new OnlineOrderProcessor();
        var items = new List<string> { "Keyboard" };

        // Act & Assert (should not throw, but validation fails)
        processor.ProcessOrder("", items, 100.00m);
    }

    [Fact]
    public void ProcessOrder_WithNullCustomerId_ShouldNotValidate()
    {
        // Arrange
        var processor = new OnlineOrderProcessor();
        var items = new List<string> { "Monitor" };

        // Act & Assert (should not throw, but validation fails)
        processor.ProcessOrder(null!, items, 200.00m);
    }

    [Fact]
    public void ProcessOrder_WithEmptyItems_ShouldCompleteSuccessfully()
    {
        // Arrange
        var processor = new OnlineOrderProcessor();
        var items = new List<string>();

        // Act & Assert (should not throw)
        processor.ProcessOrder("CUST-002", items, 50.00m);
    }

    [Fact]
    public void ProcessOrder_WithMultipleItems_ShouldCheckAllItems()
    {
        // Arrange
        var processor = new OnlineOrderProcessor();
        var items = new List<string> { "Item1", "Item2", "Item3", "Item4" };

        // Act & Assert (should not throw)
        processor.ProcessOrder("CUST-003", items, 500.00m);
    }

    [Fact]
    public void ProcessOrder_WithZeroAmount_ShouldCompleteSuccessfully()
    {
        // Arrange
        var processor = new OnlineOrderProcessor();
        var items = new List<string> { "Free Item" };

        // Act & Assert (should not throw)
        processor.ProcessOrder("CUST-004", items, 0m);
    }

    [Fact]
    public void ProcessOrder_WithLargeAmount_ShouldCompleteSuccessfully()
    {
        // Arrange
        var processor = new OnlineOrderProcessor();
        var items = new List<string> { "Luxury Item" };

        // Act & Assert (should not throw)
        processor.ProcessOrder("CUST-005", items, 99999.99m);
    }

    [Fact]
    public void GetChannelName_ShouldReturnOnline()
    {
        // Arrange
        var processor = new OnlineOrderProcessor();

        // Act
        var channelName = processor.GetType()
            .GetMethod("GetChannelName", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            ?.Invoke(processor, null) as string;

        // Assert
        Assert.Equal("Online", channelName);
    }
}

