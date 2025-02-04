using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Outbox.Shared.Events;
using Outbox.TopupAPI.Contexts;
using Outbox.TopupAPI.DTOs;
using Outbox.TopupAPI.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<TopupAPIContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("TopupAPIContext")));

builder.Services.AddMassTransit(config =>
{
    config.SetKebabCaseEndpointNameFormatter();

    config.AddEntityFrameworkOutbox<TopupAPIContext>(c =>
    {
        c.UseSqlServer();
        c.UseBusOutbox();
    });

    config.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", c =>
        {
            c.Username("guest");
            c.Password("guest");
        });
    });
});

var app = builder.Build();

app.MapPost("/Topup/Sale",
    async ([FromServices] TopupAPIContext context,
        [FromServices] IPublishEndpoint endpoint,
        [FromBody] TopupSaleRequest request) =>
        {
            var transaction = new TopupTransaction
            {
                TerminalId = request.TerminalId,
                SystemTrace = request.SystemTrace,
                Mobile = request.Mobile,
                Amount = request.Amount,
                CreationDateTime = DateTime.Now
            };

            await context.TopupTransactions.AddAsync(transaction);


            TopupSaleCompleted topupSaleCompleted = new(transaction.TerminalId, transaction.SystemTrace, transaction.Mobile, transaction.Amount);
            await endpoint.Publish(topupSaleCompleted);

            await context.SaveChangesAsync();

            return Results.Ok(transaction);
        });

app.Run();