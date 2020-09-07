using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace TelegramBot.Bot
{
    public class BotSettings
    {
        private readonly IConfiguration _config;

        public BotSettings(IConfiguration config)
        {
            _config = config;
        }

        public string BotName => _config.GetSection("Bot:Name").Value;
        public string BotKey => _config.GetSection("Bot:Key").Value;
        public string BotUrl => _config.GetSection("Bot:Url").Value;
        public string BotType => _config.GetSection("Bot:Type").Value;
    }
}
