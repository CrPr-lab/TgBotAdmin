using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TgBotAdmin.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Main()
        {
            return View();
        }

        private async Task<string> ExecQueryAsync(string token, string ApiCommand)
        {
            string url = "https://api.telegram.org/bot" + token + "/" + ApiCommand;

            var httpClient = new HttpClient();
            //var Proxy = new WebProxy("http://205.144.171.10:8443");
            //HttpClient.DefaultProxy = Proxy;
            HttpResponseMessage response = httpClient.GetAsync(url).Result;

            return await response.Content.ReadAsStringAsync();
        }

        public IActionResult GetMe(string token)
        {
            ITelegramBotClient Bot = new TelegramBotClient(token);
            User user = Bot.GetMeAsync().Result;
            
            return Content(user.ToString());
            //return Content(ExecQueryAsync(token, "GetMe").Result);
        }
    }
}
