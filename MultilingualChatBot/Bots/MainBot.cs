using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;

namespace MultilingualChatBot.Bots
{
    public class MainBot : ActivityHandler
    {
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
            var replyText = $"Your language is **{turnContext.Activity.Text}**";
            return turnContext.SendActivityAsync(MessageFactory.Text(replyText, replyText), cancellationToken);
        }
    }
}
