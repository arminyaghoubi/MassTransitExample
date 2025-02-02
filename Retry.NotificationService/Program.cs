using MassTransit;
using MassTransit.Middleware;
using Retry.NotificationService;
using Retry.NotificationService.Consumers;
using Retry.Shared.Events;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddMassTransit(config =>
{
    config.AddConsumer<EmailRequestSentConsumer>();

    config.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", c =>
        {
            c.Username("guest");
            c.Password("guest");
        });

        cfg.ReceiveEndpoint("email-queue", c =>
        {
            c.ConfigureConsumer<EmailRequestSentConsumer>(context);

            c.UseMessageRetry(r =>
            {
                r.Interval(3, 5000);
            });

            c.ConfigureDeadLetter(filter =>
            {
                filter.UseFilter(new DeadLetterTransportFilter());
            });
        });
    });
});

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
