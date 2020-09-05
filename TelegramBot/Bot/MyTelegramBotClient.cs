using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;

namespace TelegramBot.Bot
{
    public class MyTelegramBotClient : TelegramBotClient.TelegramBotClient
    {
        private const string ServiceUrl = @"https://cee5521d82ce.ngrok.io";
        private const string BotKey = "1366437273:AAGpXWEEC-Q2v6jUwIlGPluBbzWIdrfobGo";

        public MyTelegramBotClient()
            : base(BotKey)
        {
            SetWebHook();
        }

        private async void SetWebHook()
        {
            await base.SetWebhookAsync($"{ServiceUrl}/api/message/update");
        }
    }
}
