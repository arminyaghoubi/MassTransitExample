using MassTransit;
using Outbox.TopupService.Consumers;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddMassTransit(config =>
{
    config.AddConsumer<TopupSaleCompletedConsumer>();

    config.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", c =>
        {
            c.Username("guest");
            c.Password("guest");
        });

        cfg.ReceiveEndpoint("topup-sale-completed-queue", cfg =>
        {
            cfg.ConfigureConsumer<TopupSaleCompletedConsumer>(context);
        });
    });
});

var host = builder.Build();
host.Run();
