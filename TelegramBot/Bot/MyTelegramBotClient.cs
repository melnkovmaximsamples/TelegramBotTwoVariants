using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using TelegramBotClient.Abstractions;

namespace TelegramBot.Bot
{
    public class MyTelegramBotClient : TelegramBotClient.TelegramBotClient, ITelegramBotClient
    {
        private readonly BotSettings _settings;

        public MyTelegramBotClient(BotSettings settings)
            : base(settings.BotKey)
        {
            _settings = settings;
            SetWebHook();
        }

        private async void SetWebHook()
        {
            await base.SetWebhookAsync($"{_settings.BotUrl}/api/message/update");
        }
    }
}
