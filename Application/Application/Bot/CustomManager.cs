using System;
using System.Linq;
using Application.Bot.Commands.Interaction;
using Application.Bot.Commands.Prefix;
using Discord;
using Discord.Interactions;
using Discord.Net;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

public class CustomManager
{
    private readonly IConfiguration _configuration;
    private readonly DiscordSocketClient _client;
    private readonly InteractionService _interactionService;
    private readonly InteractionHandler _interactionHandler;
    private readonly PrefixHandler _prefixHandler;

    public CustomManager(IConfiguration configuration, 
        DiscordSocketClient client, 
        InteractionService interactionService, 
        PrefixHandler prefixHandler, 
        InteractionHandler interactionHandler)
    {
        _configuration = configuration;
        _client = client;
        _interactionService = interactionService;
        _interactionHandler = interactionHandler;
        _prefixHandler = prefixHandler;
        RunAsync();
    }

    private async Task RunAsync()
    {

        await _interactionHandler.InitializeAsync();
        _prefixHandler.AddModule<PrefixModule>();

        _client.Log += Log;
        _interactionService.Log += Log;
        _client.Ready += Client_Ready;

        var token = _configuration.GetSection("DiscordToken").Value;

        await _client.LoginAsync(TokenType.Bot, token);
        await _client.StartAsync();

        await Task.Delay(1);
    }


    private static Task Log(LogMessage msg)
    {
        Console.WriteLine(msg.ToString());
        return Task.CompletedTask;
    }

    private async Task Client_Ready()
    {
        try
        {
            await _interactionService.RegisterCommandsGloballyAsync();
        }
        catch (HttpException exception)
        {
            var json = JsonConvert.SerializeObject(exception.Errors, Formatting.Indented);
            Console.WriteLine(json);
        }
    }
}