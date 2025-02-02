using Basic.Shared.Events;
using MassTransit;

namespace Basic.InvoiceService.Consumers;

public class OrderCreatedConsumer : IConsumer<OrderCreated>
{
    private readonly ILogger<OrderCreatedConsumer> _logger;

    public OrderCreatedConsumer(ILogger<OrderCreatedConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<OrderCreated> context)
    {
        var message = context.Message;

        _logger.LogInformation($"[Invoice Service] Processing invoice for order: {message.OrderId}");

        await Task.Delay(2000);

        _logger.LogInformation($"[Invoice Service] Invoice generated for order: {message.OrderId}");
    }
}
