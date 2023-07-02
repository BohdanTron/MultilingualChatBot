using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Localization;
using MultilingualChatBot.Resources;

namespace MultilingualChatBot.Bots
{
    public class MainBot : ActivityHandler
    {
        private readonly IStringLocalizer<BotMessages> _localizer;

        public MainBot(IStringLocalizer<BotMessages> localizer) =>
            _localizer = localizer;

        protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            foreach (var member in membersAdded)
            {
                if (member.Id == turnContext.Activity.Recipient.Id)
                    continue;

                var reply = MessageFactory.Text("Please choose your language");
                reply.SuggestedActions = new SuggestedActions
                {
                    Actions = new List<CardAction>
                    {
                        new() { Title = "English", Type = ActionTypes.ImBack, Value = "English" },
                        new() { Title = "Українська", Type = ActionTypes.ImBack, Value = "Українська" }
                    }
                };
                await turnContext.SendActivityAsync(reply, cancellationToken);
            }
        }

        protected override Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            var language = turnContext.Activity.Text;

            var welcomeMsg = _localizer["WelcomeMessage", language];

            return turnContext.SendActivityAsync(MessageFactory.Text(welcomeMsg, welcomeMsg), cancellationToken);
        }
    }
}
