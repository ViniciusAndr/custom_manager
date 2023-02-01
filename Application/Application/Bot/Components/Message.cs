using Discord;
using Discord.Commands;
using Discord.Interactions;
using Discord.WebSocket;

namespace custom_manager_back.Bot.Components
{
    public class Message: InteractionModuleBase
    {
        public async Task CreateEmbedMessage(SocketSlashCommand command)
        {
            //TODO: Method to create and save on database
            var newCustom = new Random().Next(1,100);

            var guildUser = (SocketGuildUser)command.User;
            
            var embedBuiler = new EmbedBuilder()
                .WithAuthor(guildUser.ToString(), guildUser.GetAvatarUrl() ?? guildUser.GetDefaultAvatarUrl())
                .WithTitle($"Queue #{newCustom} created")
                .WithDescription("Custom queue created, type /join-custom to enter.")
                .WithColor(Color.Green)
                .WithCurrentTimestamp();
            
            await command.RespondAsync(embed: embedBuiler.Build());
        }
    }
}