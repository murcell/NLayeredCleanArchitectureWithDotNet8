using App.Domain.Events;
using MassTransit;

namespace App.Bus.Consumer;

public class ProductAddedEventConsumer() : IConsumer<ProductAddedEvent>
{
	public Task Consume(ConsumeContext<ProductAddedEvent> context)
	{
		Console.WriteLine($"Product Add Event Received: {context.Message.Id} - {context.Message.Name} - {context.Message.Price} ");
		return Task.CompletedTask;
	}
}
