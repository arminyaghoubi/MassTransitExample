using MassTransit;
using MassTransit.Transports;
using Retry.NotificationAPI.DTOs;
using Retry.Shared.Events;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMassTransit(config =>
{
    config.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", c =>
        {
            c.Username("guest");
            c.Password("guest");
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

app.MapPost("/SendEmail", async (IPublishEndpoint publishEndpoint,EmailRequest request) =>
{
    EmailRequestSent requestSent = new(Guid.NewGuid(), request.Email, request.Message, DateTime.Now);
    await publishEndpoint.Publish(requestSent);

    return Results.Ok();
});

app.Run();
