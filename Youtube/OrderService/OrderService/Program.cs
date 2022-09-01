using MassTransit;
using Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
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

#region Basic code
//var bus = Bus.Factory.CreateUsingRabbitMq(config =>
//{
//config.Host("amqp://guest:guest@localhost:5672");
//config.ReceiveEndpoint("temp-queue", c =>
//{
//c.Handler<Order>(ctx =>
//{
//    return Console.Out.WriteLineAsync(ctx.Message.Name);
//});
//});
//});

//bus.Start();
//bus.Publish(new Order
//{
//    Name = "Test"
//}); 
#endregion

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();