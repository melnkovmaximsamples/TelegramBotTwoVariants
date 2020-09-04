using System.IO;
using Telegram.Bot;

namespace TelegramBot.Bot
{
    public class MyTelegramBotClient : TelegramBotClient
    {
        private const string ServiceUrl = @"https://95a5e44fe98e.ngrok.io/";
        private const string BotKey = "1366437273:AAGpXWEEC-Q2v6jUwIlGPluBbzWIdrfobGo";

        public MyTelegramBotClient()
            : base(BotKey)
        {
            SetWebHook();
        }

        private async void SetWebHook()
        {
            await base.SetWebhookAsync(Path.Combine(ServiceUrl, "api/message/update"));
        }
    }
}
