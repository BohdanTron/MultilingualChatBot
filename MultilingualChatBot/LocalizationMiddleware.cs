using System.Globalization;
using Microsoft.Bot.Builder;
using IMiddleware = Microsoft.Bot.Builder.IMiddleware;

namespace MultilingualChatBot
{
    public class LocalizationMiddleware : IMiddleware
    {
        public Task OnTurnAsync(ITurnContext turnContext, NextDelegate next, CancellationToken cancellationToken = new())
        {
            var culture = turnContext.Activity.Text switch
            {
                "English" => "en-US",
                "Українська" => "uk-UA",
                _ => "en-US"
            };

            CultureInfo.CurrentCulture = new CultureInfo(culture);
            CultureInfo.CurrentUICulture = new CultureInfo(culture);

            return next(cancellationToken);
        }
    }
}
