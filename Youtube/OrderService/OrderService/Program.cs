using MassTransit;
using Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

<<<<<<< HEAD
#region �� MassTransit DI
=======
// Add 
>>>>>>> 8643d5ee8176ddfc4d7bdae0b3389648915a69ce
builder.Services.AddMassTransit(config =>
{
    config.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host("amqp://guest:guest@localhost:5672");
    });
});
<<<<<<< HEAD
//builder.Services.AddMassTransitHostedService(); // ò���Ѿ��� Obsolete �� Deprecated, ���� hosted service ���Զ��ӵ� container ��
#endregion


=======


builder.Services.AddMassTransitHostedService();

>>>>>>> 8643d5ee8176ddfc4d7bdae0b3389648915a69ce
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
//        // ע: ������Զ�����һ�� top level �� exchange , ����� Order ������(������� Model.Order)�������Ƽ���� model ��ñ�ɹ���lib
//        // Ȼ����� exchange ���һ�� "temp-queue" ��� exchange (ͬ�����Զ����ɵ�, ���� queue������),  ������Ϣ���� temp-queue ��� exchange
//        // Ȼ����� "temp-queue" exchang ����ȥbinding  temp-queue,  �������Ϣ���� ���queue
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