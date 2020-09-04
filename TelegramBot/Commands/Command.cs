﻿using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.Bot
{
    public abstract class Command
    {
        public abstract string Name { get; }
        public abstract void Execute(Message message, ITelegramBotClient client);

        public bool Contains(string command, string botName)
        {
            command = command.ToLower();

            return command.StartsWith($"/{Name}") 
                   && command.Contains($"@{botName}");
        }
    }
}
