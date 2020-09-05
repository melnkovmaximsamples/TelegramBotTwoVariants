using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using TelegramBot.Bot;
using TelegramBotClient.Abstractions;
using TelegramBotClient.Models;

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
