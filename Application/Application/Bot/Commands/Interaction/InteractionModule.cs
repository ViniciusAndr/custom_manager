using Discord.Interactions;
using Discord;
using Application.Bot.Components;
using Discord.WebSocket;

namespace Application.Bot.Commands.Interaction
{
    public class InteractionModule : InteractionModuleBase<SocketInteractionContext>
    {
        [EnabledInDm(false)]
        [SlashCommand("create-queue", "Create a new custom queue")]
        public async Task NewCustomQueue()
        {
            //await RespondAsync("aaaa");
            await RespondWithModalAsync<NewQueueModal>("new_queue_modal");
        }
    }
}
