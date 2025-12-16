using MediatR;
using Microsoft.Extensions.Logging;

namespace MlkAdmin._2_Application.Events.UserLeft
{
    class UserLeftHandler(
        ILogger<UserLeftHandler> logger) : INotificationHandler<UserLeft>
    {
        public async Task Handle(UserLeft notification, CancellationToken cancellationToken)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                logger.LogError("Error: {Message}\nStackTrace: {StackTrace}", ex.Message, ex.StackTrace);
            }
        }
    }
}
