using Basic.InvoiceService.Consumers;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMassTransit(config =>
{
    config.AddConsumer<OrderCreatedConsumer>();

    config.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", c =>
        {
            c.Username("guest");
            c.Password("guest");
        });

        cfg.ReceiveEndpoint("invoice-queue", e =>
        {
            e.ConfigureConsumer<OrderCreatedConsumer>(context);
        });
    });
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}


app.Run();