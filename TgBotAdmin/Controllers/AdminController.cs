using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TgBotAdmin.Controllers
{
    [Route("Admin")]
    public class AdminController : Controller
    {
        const string UrlBase = "https://api.telegram.org/bot";
        private static string Token;

        [Route("Main")]
        public IActionResult Main()
        {
            return View();
        }
        

        [Route("ExecuteTgCommand/{command}")]
        public async Task<IActionResult> ExecuteTgCommand(string command, [FromBody] string parms)
        {
            if (string.IsNullOrEmpty(Token))
                //return Json("Не задан токен!");
                return Content("Не задан токен!");


            string url = UrlBase + Token + "/" + command;

            HttpClient.DefaultProxy = new WebProxy("127.0.0.1:8888");
            var httpClient = new HttpClient();

            var request = new HttpRequestMessage(HttpMethod.Get, url)
            {
                Content = new StringContent(parms, Encoding.UTF8, "application/json")
            };
            HttpResponseMessage response = await httpClient.SendAsync(request);
            return Json(await response.Content.ReadAsStringAsync());                        
        }


        [Route("SetBotToken/{token:regex(^\\d{{10}}:[[\\w\\-]]{{35}}$)}")]
        public async Task<IActionResult> SetBotToken(string token)
        {
            Token = token;
            ITelegramBotClient Bot = new TelegramBotClient(token, new WebProxy("127.0.0.1:8888"));
            User user = await Bot.GetMeAsync();
            UserProfilePhotos userProfilePhotos = await Bot.GetUserProfilePhotosAsync(user.Id);
            File file = await Bot.GetFileAsync(userProfilePhotos.Photos[0][0].FileId);
            return Content("https://api.telegram.org/file/bot" + token + "/" + file.FilePath);
        }

    }

}
