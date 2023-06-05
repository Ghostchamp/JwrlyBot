using System.Text;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Extensions;
using DSharpPlus.SlashCommands;
using JwrlyBot.MainCommands;
using Newtonsoft.Json;
using JwrlyBot.HelpersClass;

namespace JwrlyBot
{
    public class Bot
	{
		public DiscordClient? Client { get; private set; }
		public InteractivityExtension? Interactivity { get; private set; }
		public CommandsNextExtension? Commands { get; private set; }

		public async Task RunAsync()
		{
			var json = string.Empty;
			using (var fs = File.OpenRead("config.json"))
			using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
				json = await sr.ReadToEndAsync();

			var configJson = JsonConvert.DeserializeObject<ConfigJSON>(json);

			var config = new DiscordConfiguration()
			{
				Intents = DiscordIntents.All,
				Token = configJson.Token,
				TokenType = TokenType.Bot,
				AutoReconnect = true,
			};

			Client = new DiscordClient(config);
			Client.UseInteractivity(new InteractivityConfiguration()
			{
				Timeout = TimeSpan.FromMinutes(2)
			});

            Client.Ready += OnClientReady;
            Client.ComponentInteractionCreated += WriteOffButtonPressed;
			
			var slashCommandsConfig = Client.UseSlashCommands();

			slashCommandsConfig.RegisterCommands<SlCommands>(1085877391785721856);


			await Client.ConnectAsync();
			await Task.Delay(-1);
		}

        private async Task WriteOffButtonPressed(DiscordClient sender, ComponentInteractionCreateEventArgs args)
        {
            if (args.Interaction.Data.CustomId == "writeOff")
			{
				string str = args.Message.Embeds[0].Description;

                long digits = HelperMethods.GetCount(str) - 1;
				long price = HelperMethods.GetPrice(str);
				await args.Message.ModifyAsync(HelperMethods.CreateMessageItem(args.Message.Embeds[0].Title, price, digits, args.Message.Embeds[0].Image.Url.ToString()));
				await args.Interaction.CreateResponseAsync(InteractionResponseType.DeferredMessageUpdate);
                
            }
            if (args.Interaction.Data.CustomId == "addItem")
            {
                string str = args.Message.Embeds[0].Description;

                long digits = HelperMethods.GetCount(str) + 1;
                long price = HelperMethods.GetPrice(str);
                await args.Message.ModifyAsync(HelperMethods.CreateMessageItem(args.Message.Embeds[0].Title, price, digits, args.Message.Embeds[0].Image.Url.ToString()));
                await args.Interaction.CreateResponseAsync(InteractionResponseType.DeferredMessageUpdate);

            }
        }

        private Task OnClientReady(DiscordClient sender, ReadyEventArgs args)
        {
			return Task.CompletedTask;
        }

		
	}
}

