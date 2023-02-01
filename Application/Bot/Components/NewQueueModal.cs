using Discord;
using Discord.Interactions;
using Discord.WebSocket;

namespace Application.Bot.Components
{
    public class NewQueueModal : IModal
    {
        public string Title => "New Queue";
        [InputLabel("Max players")]
        [ModalTextInput("max_players_input", Discord.TextInputStyle.Short, placeholder: "0 for unlimited")]
        public string MaxPlayers { get; set; }
    }

    public class NewQueueModalHandler: InteractionModuleBase<SocketInteractionContext>
    {

        [ModalInteraction("new_queue_modal")]
        public async Task HandleNewQueueModal(NewQueueModal modal)
        {
            int input = GetNumber(modal.MaxPlayers.ToString());
            await RespondAsync(embed: CreateEmbedMessage(Context.User), components: new ComponentBuilder().WithButton("+1 teste", "teste_incresed").Build());
        }

        private static int GetNumber(string input)
        {
            string number = new string(input.Where(c => char.IsDigit(c)).ToArray());

            return string.IsNullOrEmpty(number) ? 0 : Convert.ToInt32(number);
        }

        [ComponentInteraction("teste_incresed")]
        public async Task HandleButton()
        {
            await (Context.Interaction as SocketMessageComponent)?.UpdateAsync(x =>
            {
                x.Embed = CreateEmbedMessage(Context.User);
            });

        }

        private Embed CreateEmbedMessage(SocketUser user)
        {
            //TODO: Method to create and save on database
            var newCustom = new Random().Next(1, 100);

            var embedBuiler = new EmbedBuilder()
                .WithAuthor(user.ToString(), user.GetAvatarUrl() ?? user.GetDefaultAvatarUrl())
                .WithTitle($"Queue #{newCustom} created")
                .WithFields(new EmbedFieldBuilder()
                    .WithName("Current players: ")
                    .WithValue(0))
                .WithDescription("Custom queue created, type /join-custom to enter.")
                .WithColor(Color.Green)
                .WithCurrentTimestamp();


            return embedBuiler.Build();
        }

    }
}
