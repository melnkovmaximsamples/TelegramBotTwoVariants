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

            // API sendPhoto
            using var http = new HttpClient();
            using var fileStream = new FileStream(@"C:/Users/Maks/Desktop/112589.jpg", FileMode.Open, FileAccess.Read, FileShare.Read);
            var form = new MultipartFormDataContent();
            form.Add(new StringContent(chatId.ToString()), "chat_id");
            form.Add(new StreamContent(fileStream), "photo", "photo");
            var postUrl = $"https://api.telegram.org/bot1366437273:AAGpXWEEC-Q2v6jUwIlGPluBbzWIdrfobGo/sendPhoto";
            HttpResponseMessage response = await http.PostAsync(postUrl, form);
            response.EnsureSuccessStatusCode();
            // end API sendPhoto

            await client.SendTextMessageAsync(chatId, "Hello!", messageId);
        }
    }
}
