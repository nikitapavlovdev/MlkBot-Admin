using MediatR;
using Microsoft.Extensions.Logging;

namespace MlkAdmin._2_Application.Events.SelectMenuExecuted;

class SelectMenuExecutedHandler(ILogger<SelectMenuExecutedHandler> logger) : INotificationHandler<SelectMenuExecuted>
{
    public async Task Handle(SelectMenuExecuted notification, CancellationToken cancellationToken)
    {
		try
		{
            await notification.SocketMessageComponent.DeferAsync();

            switch (notification.SocketMessageComponent.Data.CustomId)
            {
                case "choice_color_name":
                    break;

                default:
                    break;
            }
		}
		catch (Exception exception)
		{
            logger.LogError(
                exception, 
                "Error: {Message} StackTrace: {StackTrace}", 
                exception.Message, 
                exception.StackTrace);
        }
    }
}
