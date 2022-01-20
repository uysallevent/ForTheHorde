using DemandManagement.MessageContracts.Consts;
using DemandManagement.MessageContracts.Interfaces;
using DemandMenagement.Api.Model;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapPost("/post", async ([FromBody] RegisterDemandModel demand) =>
{
    var bus = BusConfigurator.ConfigureBus();

    var sendToUri = new Uri($"{RabbitMqConsts.RabbitMqUri}{RabbitMqConsts.RegisterDemandServiceQueue}");
    var endPoint = await bus.GetSendEndpoint(sendToUri);

    await endPoint.Send<IRegisterDemandCommand>(demand);

    return "Ok";
});

app.Run();

