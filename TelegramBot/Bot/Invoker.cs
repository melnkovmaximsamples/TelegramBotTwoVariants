﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Telegram.Bot;

namespace TelegramBot.Bot
{
    public class Invoker
    {
        private readonly ITelegramBotClient _client;
        private readonly List<Command> _commandList;
        public IReadOnlyList<Command> Commands => _commandList.AsReadOnly();

        public Invoker(ITelegramBotClient client)
        {
            _client = client;
            _commandList = new List<Command>();
            AddCommands();
        }
         
        private void AddCommands()
        {
            Assembly.GetAssembly(typeof(Command))
                .GetTypes()
                .Where(type => type.IsClass && !type.IsAbstract && type.IsSubclassOf(typeof(Command)))
                .ToList()
                .ForEach(type => _commandList((Command)Activator.CreateInstance(type)));
        }
    }
}