using MassTransit;
using Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region 用 MassTransit DI
builder.Services.AddMassTransit(config =>
{
    config.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host("amqp://guest:guest@localhost:5672");
    });
});
//builder.Services.AddMassTransitHostedService(); // 貌似已经被 Obsolete 或 Deprecated, 现在 hosted service 会自动加到 container 里
#endregion


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
//    config.Host("amqp://guest:guest@localhost:5672");
//    config.ReceiveEndpoint("temp-queue", c =>
//    {
//        c.Handler<Order>(ctx =>
//        // 注: 这里会自动生成一个 top level 的 exchange , 以这个 Order 来命名(这里就是 Model.Order)。所以推荐这个 model 最好变成共享lib
//        // 然后这个 exchange 会绑定一个 "temp-queue" 这个 exchange (同样是自动生成的, 根据 queue的名字),  而且消息传给 temp-queue 这个 exchange
//        // 然后这个 "temp-queue" exchang 会再去binding  temp-queue,  把这个消息传给 这个queue
//        {
//            return Console.Out.WriteLineAsync(ctx.Message.Name);
//        });
//    });
//});

//bus.Start();
//bus.Publish(new Order
//{
//    Name = "Test name"
//});
#endregion

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();