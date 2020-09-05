﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBotClient.Abstractions
{
    public interface ITelegramBotClient
    {
        Task SetWebhookAsync(string url);
        Task SendTextMessageAsync(int chatId, string message, int messageId);
    }
}