using Application.Bot.Commands.Interaction;
using Application.Bot.Commands.Prefix;
using Discord.Commands;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public class Program
{
    public static void Main(string[] args)
    {
        var host = CreateHostBuilder(args);
        ActivatorUtilities.CreateInstance<CustomManager>(host.Services);
        Console.ReadLine();
    }


    public static IHost CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration((context, configuration) =>
            {
                configuration.Sources.Clear();
                var environment = Environment.GetEnvironmentVariable("DOTNETCORE_ENVIRONMENT");
                configuration.AddJsonFile($"appsettings.{environment}.json");
                configuration.AddEnvironmentVariables();
                configuration.AddCommandLine(args);
            })
            .ConfigureServices((context, services) =>
            {
                var config = new DiscordSocketConfig()
                {
                    GatewayIntents = Discord.GatewayIntents.AllUnprivileged,
                    AlwaysDownloadUsers = true
                };

                services.AddSingleton(context.Configuration);
                services.AddSingleton(config);
                services.AddSingleton<DiscordSocketClient>();
                services.AddSingleton(x => new InteractionService(x.GetRequiredService<DiscordSocketClient>()));
                services.AddSingleton<InteractionHandler>();
                services.AddSingleton(x => new CommandService());
                services.AddSingleton<PrefixHandler>();
            })
            .Build();
    }
}