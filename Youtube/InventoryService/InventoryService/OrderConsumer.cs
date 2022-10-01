using MassTransit;
using Model;

internal class OrderConsumer : IConsumer<Order>
{
    private readonly ILogger<OrderConsumer> _logger;

    public OrderConsumer(ILogger<OrderConsumer> logger) // 查看 OrderConsumer 的 DI 
    {
        this._logger = logger;
    }
    public async Task Consume(ConsumeContext<Order> context)
    {
        await Console.Out.WriteLineAsync(context.Message.Name);
        _logger.LogInformation($"Test Log");
    }
}