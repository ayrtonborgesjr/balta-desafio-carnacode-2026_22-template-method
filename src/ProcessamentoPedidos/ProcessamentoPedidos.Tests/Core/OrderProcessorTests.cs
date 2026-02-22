using ProcessamentoPedidos.Console.Core;

namespace ProcessamentoPedidos.Tests.Core;

public class OrderProcessorTests
{
    // Concrete implementation for testing the abstract base class
    private class TestOrderProcessor : OrderProcessor
    {
        public bool ValidateCalled { get; private set; }
        public bool CalculateCalled { get; private set; }
        public bool ProcessPaymentCalled { get; private set; }
        public bool ScheduleShippingCalled { get; private set; }
        public bool NotifyCalled { get; private set; }
        public bool CheckStockCalled { get; private set; }
        public bool SeparateItemsCalled { get; private set; }

        public bool ShouldValidate { get; set; } = true;

        protected override bool Validate(string id, decimal amount)
        {
            ValidateCalled = true;
            return ShouldValidate;
        }

        protected override void Calculate(decimal amount)
        {
            CalculateCalled = true;
        }

        protected override void ProcessPayment(decimal amount)
        {
            ProcessPaymentCalled = true;
        }

        protected override void ScheduleShipping()
        {
            ScheduleShippingCalled = true;
        }

        protected override void Notify()
        {
            NotifyCalled = true;
        }

        protected override string GetChannelName() => "Test";

        protected override void CheckStock(List<string> items)
        {
            CheckStockCalled = true;
            base.CheckStock(items);
        }

        protected override void SeparateItems()
        {
            SeparateItemsCalled = true;
            base.SeparateItems();
        }
    }

    [Fact]
    public void ProcessOrder_ShouldCallAllMethodsInCorrectOrder_WhenValidationSucceeds()
    {
        // Arrange
        var processor = new TestOrderProcessor();
        var items = new List<string> { "Item1", "Item2" };

        // Act
        processor.ProcessOrder("TEST-001", items, 100.00m);

        // Assert
        Assert.True(processor.ValidateCalled, "Validate should be called");
        Assert.True(processor.CheckStockCalled, "CheckStock should be called");
        Assert.True(processor.CalculateCalled, "Calculate should be called");
        Assert.True(processor.ProcessPaymentCalled, "ProcessPayment should be called");
        Assert.True(processor.SeparateItemsCalled, "SeparateItems should be called");
        Assert.True(processor.ScheduleShippingCalled, "ScheduleShipping should be called");
        Assert.True(processor.NotifyCalled, "Notify should be called");
    }

    [Fact]
    public void ProcessOrder_ShouldStopAfterValidation_WhenValidationFails()
    {
        // Arrange
        var processor = new TestOrderProcessor { ShouldValidate = false };
        var items = new List<string> { "Item1" };

        // Act
        processor.ProcessOrder("TEST-002", items, 50.00m);

        // Assert
        Assert.True(processor.ValidateCalled, "Validate should be called");
        Assert.False(processor.CheckStockCalled, "CheckStock should not be called when validation fails");
        Assert.False(processor.CalculateCalled, "Calculate should not be called when validation fails");
        Assert.False(processor.ProcessPaymentCalled, "ProcessPayment should not be called when validation fails");
        Assert.False(processor.SeparateItemsCalled, "SeparateItems should not be called when validation fails");
        Assert.False(processor.ScheduleShippingCalled, "ScheduleShipping should not be called when validation fails");
        Assert.False(processor.NotifyCalled, "Notify should not be called when validation fails");
    }

    [Fact]
    public void ProcessOrder_ShouldHandleEmptyItemsList()
    {
        // Arrange
        var processor = new TestOrderProcessor();
        var items = new List<string>();

        // Act & Assert (should not throw)
        processor.ProcessOrder("TEST-003", items, 75.00m);
        Assert.True(processor.CheckStockCalled, "CheckStock should be called even with empty items");
    }

    [Fact]
    public void ProcessOrder_ShouldHandleMultipleItems()
    {
        // Arrange
        var processor = new TestOrderProcessor();
        var items = new List<string> { "Item1", "Item2", "Item3", "Item4", "Item5" };

        // Act & Assert (should not throw)
        processor.ProcessOrder("TEST-004", items, 500.00m);
        Assert.True(processor.CheckStockCalled, "CheckStock should be called with multiple items");
    }

    [Fact]
    public void ProcessOrder_ShouldHandleZeroAmount()
    {
        // Arrange
        var processor = new TestOrderProcessor();
        var items = new List<string> { "Free Item" };

        // Act & Assert (should not throw)
        processor.ProcessOrder("TEST-005", items, 0m);
        Assert.True(processor.CalculateCalled, "Calculate should be called with zero amount");
    }

    [Fact]
    public void ProcessOrder_TemplateMethod_ShouldNotBeOverridable()
    {
        // Arrange & Act
        var methodInfo = typeof(OrderProcessor).GetMethod("ProcessOrder");

        // Assert
        Assert.NotNull(methodInfo);
        Assert.False(methodInfo!.IsVirtual || methodInfo.IsAbstract, 
            "ProcessOrder template method should not be virtual or abstract to prevent override");
    }

    [Fact]
    public void CheckStock_ShouldBeVirtual_AllowingOverride()
    {
        // Arrange & Act
        var methodInfo = typeof(OrderProcessor).GetMethod("CheckStock", 
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        // Assert
        Assert.NotNull(methodInfo);
        Assert.True(methodInfo!.IsVirtual, "CheckStock should be virtual to allow override");
    }

    [Fact]
    public void SeparateItems_ShouldBeVirtual_AllowingOverride()
    {
        // Arrange & Act
        var methodInfo = typeof(OrderProcessor).GetMethod("SeparateItems", 
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        // Assert
        Assert.NotNull(methodInfo);
        Assert.True(methodInfo!.IsVirtual, "SeparateItems should be virtual to allow override");
    }

    [Fact]
    public void AbstractMethods_ShouldBeAbstract()
    {
        // Arrange
        var type = typeof(OrderProcessor);

        // Act & Assert
        var validateMethod = type.GetMethod("Validate", 
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        Assert.True(validateMethod?.IsAbstract, "Validate should be abstract");

        var calculateMethod = type.GetMethod("Calculate", 
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        Assert.True(calculateMethod?.IsAbstract, "Calculate should be abstract");

        var processPaymentMethod = type.GetMethod("ProcessPayment", 
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        Assert.True(processPaymentMethod?.IsAbstract, "ProcessPayment should be abstract");

        var scheduleShippingMethod = type.GetMethod("ScheduleShipping", 
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        Assert.True(scheduleShippingMethod?.IsAbstract, "ScheduleShipping should be abstract");

        var notifyMethod = type.GetMethod("Notify", 
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        Assert.True(notifyMethod?.IsAbstract, "Notify should be abstract");

        var getChannelNameMethod = type.GetMethod("GetChannelName", 
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        Assert.True(getChannelNameMethod?.IsAbstract, "GetChannelName should be abstract");
    }
}

