using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using TelegramBotClient.Abstractions;

namespace TelegramBot.Bot
{
    public class LibTelegramBotClient : Telegram.Bot.TelegramBotClient, ITelegramBotClient
    {
        private readonly BotSettings _settings;

        public LibTelegramBotClient(BotSettings settings)
            : base(settings.BotKey)
        {
            _settings = settings;
            SetWebHook();
        }

        private async void SetWebHook()
        {
            await base.SetWebhookAsync($"{_settings.BotUrl}/api/message/update");
        }

        public async Task SendTextMessageAsync(int chatId, string text, int replyToMessageId)
        {
            await base.SendTextMessageAsync(chatId, text, replyToMessageId: replyToMessageId);
        }

        public Task SendPhotoAsync(int chatId)
        {
            throw new NotImplementedException();
        }
    }
}
