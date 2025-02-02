using Microsoft.AspNetCore.Mvc;
using Basic.OrderService.DTOs;
using MassTransit;
using Basic.Shared.Events;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMassTransit(config =>
{
    config.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost",settings =>
        {
            settings.Username("guest");
            settings.Password("guest");
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

app.MapPost("/CreateOrder",
    async (IPublishEndpoint publishEndpoint, CreateOrderDto createOrder) =>
    {
        OrderCreated orderCreated = new(Guid.NewGuid(), createOrder.CustomerEmail, createOrder.TotalAmount);
        await publishEndpoint.Publish(orderCreated);
        return Results.Ok(orderCreated);
    });

app.Run();
