namespace Basic.Shared.Events;

public record OrderCreated(Guid OrderId, string CustomerEmail, decimal TotalAmount);
