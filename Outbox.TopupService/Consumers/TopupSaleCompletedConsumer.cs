using MassTransit;
using Outbox.Shared.Events;

namespace Outbox.TopupService.Consumers;

internal class TopupSaleCompletedConsumer : IConsumer<TopupSaleCompleted>
{
    private readonly ILogger<TopupSaleCompletedConsumer> _logger;

    public TopupSaleCompletedConsumer(ILogger<TopupSaleCompletedConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<TopupSaleCompleted> context)
    {
        // Processing for call operator

        _logger.LogInformation("Topup Completed Received: {TerminalId}-{SystemTrace}-{Mobile}-{Amount}",
            context.Message.TerminalId, context.Message.SystemTrace, context.Message.Mobile, context.Message.Amount);
    }
}
