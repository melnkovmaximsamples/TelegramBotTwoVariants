using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.Bot
{
    public class HelloCommand : Command
    {
        public override string Name => "hello";
        public override async void Execute(Message message, ITelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;

            await client.SendTextMessageAsync(chatId, "Hello!", replyToMessageId: messageId);
        }
    }
}
