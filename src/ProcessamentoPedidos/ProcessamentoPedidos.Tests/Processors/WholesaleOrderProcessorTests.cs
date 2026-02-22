using ProcessamentoPedidos.Console.Processors;

namespace ProcessamentoPedidos.Tests.Processors;

public class WholesaleOrderProcessorTests
{
    [Fact]
    public void ProcessOrder_WithValidDataAboveMinimum_ShouldCompleteSuccessfully()
    {
        // Arrange
        var processor = new WholesaleOrderProcessor();
        var items = new List<string> { "Bulk Item A", "Bulk Item B" };

        // Act & Assert (should not throw)
        processor.ProcessOrder("COMPANY-001", items, 5000.00m);
    }

    [Fact]
    public void ProcessOrder_WithAmountBelowMinimum_ShouldNotValidate()
    {
        // Arrange
        var processor = new WholesaleOrderProcessor();
        var items = new List<string> { "Item" };

        // Act & Assert (should not throw, but validation fails - minimum is R$ 1,000.00)
        processor.ProcessOrder("COMPANY-002", items, 999.99m);
    }

    [Fact]
    public void ProcessOrder_WithExactMinimumAmount_ShouldValidate()
    {
        // Arrange
        var processor = new WholesaleOrderProcessor();
        var items = new List<string> { "Item" };

        // Act & Assert (should validate - exactly R$ 1,000.00)
        processor.ProcessOrder("COMPANY-003", items, 1000.00m);
    }

    [Fact]
    public void ProcessOrder_WithEmptyCompanyId_ShouldNotValidate()
    {
        // Arrange
        var processor = new WholesaleOrderProcessor();
        var items = new List<string> { "Product" };

        // Act & Assert (should not throw, but validation fails)
        processor.ProcessOrder("", items, 1500.00m);
    }

    [Fact]
    public void ProcessOrder_WithNullCompanyId_ShouldNotValidate()
    {
        // Arrange
        var processor = new WholesaleOrderProcessor();
        var items = new List<string> { "Product" };

        // Act & Assert (should not throw, but validation fails)
        processor.ProcessOrder(null!, items, 2000.00m);
    }

    [Fact]
    public void ProcessOrder_WithValidData_ShouldApplyTenPercentDiscount()
    {
        // Arrange
        var processor = new WholesaleOrderProcessor();
        var items = new List<string> { "Bulk Product" };

        // Act & Assert (should apply 10% discount)
        processor.ProcessOrder("COMPANY-004", items, 10000.00m);
    }

    [Fact]
    public void ProcessOrder_WithMultipleItems_ShouldCheckAllItems()
    {
        // Arrange
        var processor = new WholesaleOrderProcessor();
        var items = new List<string> { "Item1", "Item2", "Item3", "Item4", "Item5" };

        // Act & Assert (should not throw)
        processor.ProcessOrder("COMPANY-005", items, 3000.00m);
    }

    [Fact]
    public void ProcessOrder_WithEmptyItems_ShouldCompleteSuccessfully()
    {
        // Arrange
        var processor = new WholesaleOrderProcessor();
        var items = new List<string>();

        // Act & Assert (should not throw)
        processor.ProcessOrder("COMPANY-006", items, 1200.00m);
    }

    [Fact]
    public void ProcessOrder_WithHighValue_ShouldCalculateCorrectDiscount()
    {
        // Arrange
        var processor = new WholesaleOrderProcessor();
        var items = new List<string> { "Expensive Bulk Item" };

        // Act & Assert (should calculate 10% discount on large amount)
        processor.ProcessOrder("COMPANY-007", items, 100000.00m);
    }

    [Fact]
    public void ProcessOrder_BelowMinimum_WithValidCompanyId_ShouldFailValidation()
    {
        // Arrange
        var processor = new WholesaleOrderProcessor();
        var items = new List<string> { "Small Order" };

        // Act & Assert (should fail both on amount)
        processor.ProcessOrder("COMPANY-008", items, 500.00m);
    }

    [Fact]
    public void ProcessOrder_BelowMinimum_WithInvalidCompanyId_ShouldFailValidation()
    {
        // Arrange
        var processor = new WholesaleOrderProcessor();
        var items = new List<string> { "Small Order" };

        // Act & Assert (should fail on both company id and amount)
        processor.ProcessOrder("", items, 500.00m);
    }

    [Fact]
    public void GetChannelName_ShouldReturnAtacado()
    {
        // Arrange
        var processor = new WholesaleOrderProcessor();

        // Act
        var channelName = processor.GetType()
            .GetMethod("GetChannelName", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            ?.Invoke(processor, null) as string;

        // Assert
        Assert.Equal("Atacado", channelName);
    }
}

