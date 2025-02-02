using MassTransit;
using Retry.Shared.Events;

namespace Retry.NotificationService.Consumers;

internal class EmailRequestSentConsumer : IConsumer<EmailRequestSent>
{
    private readonly ILogger<EmailRequestSentConsumer> _logger;

    public EmailRequestSentConsumer(ILogger<EmailRequestSentConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<EmailRequestSent> context)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Received Request:\n\t{context.Message.Email}\n\t{context.Message.Message}");
        Console.ResetColor();

        if (new Random().Next(1,4)!=1)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Processing Failed! Retrying...");
            Console.ResetColor();

            throw new Exception("Processing Failed!");
        }

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"[✔] Email processed successfully: {context.Message.Email}");
        Console.ResetColor();
    }
}
