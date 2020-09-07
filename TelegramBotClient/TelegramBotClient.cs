using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.InlineQueryResults;
using TelegramBotClient.Abstractions;

namespace TelegramBotClient
{
    public class TelegramBotClient : ITelegramBotClient
    {
        public readonly string Token;

        private string baseUrl =>
            $"https://api.telegram.org/bot{Token}";
        private string urlWebHook => $"{baseUrl}/setWebHook";
        private string urlSendMessage => $"{baseUrl}/sendMessage";
        private string urlSendPhoto => $"{baseUrl}/sendPhoto";

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
            await SendPostAsync(urlSendMessage, postParams);
        }

        public async Task SendPhotoAsync(int chatId)
        {
            using var http = new HttpClient();
            using var fileStream = new FileStream(@"C:/Users/Maks/Desktop/112589.jpg", FileMode.Open, FileAccess.Read, FileShare.Read);
            var form = new MultipartFormDataContent();
            form.Add(new StringContent(chatId.ToString()), "chat_id");
            form.Add(new StreamContent(fileStream), "photo", "photo");

            await SendPostMultipartFormDataAsync(urlSendPhoto, form);
        }

        private async Task SendPostAsync(string url, Dictionary<string, string> postParams = default)
        {
            using var http = new HttpClient();
            var content = new FormUrlEncodedContent(postParams);
            HttpResponseMessage response = await http.PostAsync(url, content);
            response.EnsureSuccessStatusCode();
        }

        private async Task SendPostMultipartFormDataAsync(string url, MultipartFormDataContent content = default)
        {
            using var http = new HttpClient();
            HttpResponseMessage response = await http.PostAsync(url, content);
            response.EnsureSuccessStatusCode();
        }
    }
}
