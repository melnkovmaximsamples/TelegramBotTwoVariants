using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.Win32.SafeHandles;
using TelegramBot.Commands;
using TelegramBotClient.Abstractions;
using TelegramBotClient.Models;
using File = System.IO.File;

namespace TelegramBot.Bot
{
    public class HelloCommand : Command
    {
        public override string Name => "hello";
        public override async void Execute(Message message, ITelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;

            await client.SendPhotoAsync(chatId);
            await client.SendTextMessageAsync(chatId, "Hello!", messageId);
        }
    }
}
