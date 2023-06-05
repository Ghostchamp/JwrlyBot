using System;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using JwrlyBot.HelpersClass;

namespace JwrlyBot.MainCommands
{
    public class SlCommands : ApplicationCommandModule
    {
        [SlashCommand("add", "Добавить новую позицию")]
        public async Task AddItem(InteractionContext ctx, [Option("name", "Артикул позиции")] string name, [Option("count","Кол-во")] long? count, [Option("price","Цена")] long? price, [Option("image", "Картинка позиции")] DiscordAttachment picture)
        {
            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder()
                .WithContent("...")
                );
            
            await ctx.Channel.SendMessageAsync(HelperMethods.CreateMessageItem(name, price, count, picture.Url));
        }

    }
}
