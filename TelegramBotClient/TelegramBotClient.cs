using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TelegramBotClient.Abstractions;

namespace TelegramBotClient
{
    public class TelegramBotClient : ITelegramBotClient
    {
        public readonly string Token;

        private string baseUrl =>
            $"https://api.telegram.org/bot{Token}";
        private string urlWebHook => $"{baseUrl}/setWebHook";
        private string urlTextMessage => $"{baseUrl}/sendMessage";

        public TelegramBotClient(string token)
        {
            Token = token;
        }

        public async Task SetWebhookAsync(string url)
        {
            var postParams = new Dictionary<string, string>()
            {
                {"url", url}
            };
            await SendPostAsync(urlWebHook, postParams);
        }

        public async Task SendTextMessageAsync(int chatId, string text, int replyToMessageId)
        {
            var postParams = new Dictionary<string, string>()
            {
                {"chat_id", chatId.ToString() },
                {"text", text},
                {"reply_to_message_id", replyToMessageId.ToString() }
            };
            await SendPostAsync(urlTextMessage, postParams);
        }

        private async Task SendPostAsync(string url, Dictionary<string, string> postParams = default)
        {
            using var http = new HttpClient();
            var content = new FormUrlEncodedContent(postParams);
            HttpResponseMessage response = await http.PostAsync(url, content);
            response.EnsureSuccessStatusCode();
        }


    }
}
