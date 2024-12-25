using MassTransit;
using Microsoft.Extensions.Logging;

namespace Users.Application.User.Add;

public class AddUserEventConsumer : IConsumer<AddUserEvent>
{
    private readonly ILogger<AddUserEventConsumer> _logger;

    public AddUserEventConsumer(ILogger<AddUserEventConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<AddUserEvent> context)
    {
        _logger.LogInformation("--------------------------------");
        _logger.LogInformation($"Event Occured: {nameof(AddUserEventConsumer)}");
        _logger.LogInformation($"Event Id: {context.Message.EventId}");
        _logger.LogInformation($"User Id: {context.Message.UserId}");
        _logger.LogInformation($"Email: {context.Message.Email}");
        _logger.LogInformation($"Event Occured At: {context.Message.OccuredAtUtc}");
        _logger.LogInformation($"Event Consumed At: {DateTime.Now}");
        _logger.LogInformation("--------------------------------");

        return Task.CompletedTask;
    }
}