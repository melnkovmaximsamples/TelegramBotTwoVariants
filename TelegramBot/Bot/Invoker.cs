using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TelegramBot.Commands;
using TelegramBotClient.Abstractions;

namespace TelegramBot.Bot
{
    public class Invoker
    {
        private readonly List<Command> _commandList;
        public IReadOnlyList<Command> Commands => _commandList.AsReadOnly();

        public Invoker()
        {
            _commandList = new List<Command>();
            AddCommands();
        }
         
        private void AddCommands()
        {
            Assembly.GetAssembly(typeof(Command))
                .GetTypes()
                .Where(type => type.IsClass && !type.IsAbstract && type.IsSubclassOf(typeof(Command)))
                .ToList()
                .ForEach(type => _commandList.Add((Command)Activator.CreateInstance(type)));
        }
    }
}
