using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using TelegramBot.Bot;
using TelegramBotClient.Abstractions;
using TelegramBotClient.Models;

namespace TelegramBot.Controllers
{
    [Route("api/message/update")]
    public class MessageController : Controller
    {
        private readonly BotSettings _settings;
        private readonly ITelegramBotClient _client;
        private readonly Invoker _invoker;

        public MessageController(ITelegramBotClient client, Invoker invoker, BotSettings settings)
        {
            _client = client;
            _invoker = invoker;
            _settings = settings;
        }

        [HttpPost]
        public async Task<OkResult> Update([FromBody] Update update)
        {
            if (update == null) return Ok();
            var commands = _invoker.Commands;
            var message = update.Message;

            foreach (var command in commands)
            {
                if (command.Contains(message.Text, _settings.BotName))
                {
                    await Task.Run(() => command.Execute(message, _client));
                    break;
                }
            }

            // If need read full body before create model
            //string result;
            //using (var reader = new StreamReader(Request.Body, Encoding.UTF8))
            //{
            //    result = await reader.ReadToEndAsync();
            //}
            return Ok();
        }
    }
}
