using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TgBotAdmin.Controllers
{
    public class AdminController : Controller
    {
        const string UrlBase = "https://api.telegram.org/bot";

        private static string Token;

        public IActionResult Main()
        {
            return View();
        }
        

        [Route("Admin/ExecuteTgCommand/{command}")]
        public async Task<IActionResult> ExecuteTgCommand(string command, string parms)
        {
            if (string.IsNullOrEmpty(Token))
                return Content("Не задан токен!");
            
            string url = UrlBase + Token + "/" + command + (parms == null ? ("") : ("?" + parms));

            var httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.GetAsync(url);
            return Content(await response.Content.ReadAsStringAsync());                        
        }


        public async Task<IActionResult> SetBotToken(string token)
        {
            Token = token;
            ITelegramBotClient Bot = new TelegramBotClient(token, new WebProxy("127.0.0.1:8888"));
            User user = await Bot.GetMeAsync();
            UserProfilePhotos userProfilePhotos = await Bot.GetUserProfilePhotosAsync(user.Id);
            File file = await Bot.GetFileAsync(userProfilePhotos.Photos[0][0].FileId);
            return Content(file.FilePath);
        }

    }
}
