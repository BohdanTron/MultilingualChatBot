using Microsoft.Bot.Builder.Integration.AspNet.Core;
using Microsoft.Bot.Connector.Authentication;

namespace MultilingualChatBot
{
    public class AdapterWithErrorHandler : CloudAdapter
    {
        public AdapterWithErrorHandler(
            LocalizationMiddleware localizationMiddleware, 
            BotFrameworkAuthentication auth,
            ILogger<IBotFrameworkHttpAdapter> logger) : base(auth, logger)

        {
            Use(localizationMiddleware);

            OnTurnError = async (turnContext, exception) =>
            {
                // Log any leaked exception from the application.
                logger.LogError(exception, $"[OnTurnError] unhandled error : {exception.Message}");

                // Send a message to the user
                await turnContext.SendActivityAsync("The bot encountered an error or bug.");
                await turnContext.SendActivityAsync("To continue to run this bot, please fix the bot source code.");
            };
        }
    }
}
