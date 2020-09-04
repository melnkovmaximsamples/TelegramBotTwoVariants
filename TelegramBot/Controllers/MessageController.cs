using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Bot;

namespace TelegramBot.Controllers
{
    [Route("api/message/update")]
    public class MessageController : Controller
    {
        private const string BotName = "mybot";
        private ITelegramBotClient _client;
        private Invoker _invoker;
        private IConfiguration _config;

        public MessageController(ITelegramBotClient client, Invoker invoker, IConfiguration config)
        {
            _client = client;
            _invoker = invoker;
            _config = config;
        }

        [HttpPost]
        public async Task<OkResult> Update([FromBody] Update update)
        {
            if (update == null) return Ok();
            var commands = _invoker.Commands;
            var message = update.Message;

            foreach (var command in commands)
            {
                if (command.Contains(message.Text, BotName))
                {
                    await Task.Run(() => command.Execute(message, _client));
                    break;
                }
            }

            return Ok();
        }
    }
}
