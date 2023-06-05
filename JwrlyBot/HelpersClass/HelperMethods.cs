using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using DSharpPlus;
using System.Xml.Linq;
using DSharpPlus.Entities;

namespace JwrlyBot.HelpersClass
{
	public static class HelperMethods
	{
		public static long GetCount(string str)
		{
			long count = 0;
			string[] digits = Regex.Split(str, @"[а-я]+|[:]|[-]|[\n]|[\s]", RegexOptions.IgnoreCase);
			long.TryParse(digits[9], out count);
			return count;
		}

		public static string GetString(string str)
		{
			string strNum = "";
			string[] newStr = Regex.Split(str, @"[а-я]+|[:]|[-]|[\n]|[\s]", RegexOptions.IgnoreCase);
            strNum = string.Concat(strNum, newStr[3]);
            strNum = string.Concat(strNum, "|");
            strNum = string.Concat(strNum, newStr[9]);
            return strNum;
        }

		public static long GetPrice(string str)
		{
			long price = 0;
            string[] digits = Regex.Split(str, @"[а-я]+|[:]|[-]|[\n]|[\s]",RegexOptions.IgnoreCase);
            long.TryParse(digits[3], out price);
            return price;
		}

		public static DiscordMessageBuilder CreateMessageItem(string name, long? price, long? count, string pictureUrl)
		{
            DiscordButtonComponent writeOfButton = new DiscordButtonComponent(ButtonStyle.Danger, "writeOff", "Списать");
            DiscordButtonComponent addItemButton = new DiscordButtonComponent(ButtonStyle.Success, "addItem", "Добавить");
			var message = new DiscordMessageBuilder()
				.AddEmbed(new DiscordEmbedBuilder()

				.WithColor(DiscordColor.CornflowerBlue)
			.WithTitle(name)
				.WithDescription("Цена: " + price + "\n" + "Кол-во: " + count)
				.WithImageUrl(pictureUrl)
				)
				.AddComponents(writeOfButton)
				.AddComponents(addItemButton);
			return message;
        }
	}
}

