﻿using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Main()
        {
            return View();
        }


        //private async Task<string> ExecQueryAsync(string token, string ApiCommand)
        //{
        //    string url = "https://api.telegram.org/bot" + token + "/" + ApiCommand;

        //    var httpClient = new HttpClient();
        //    //var Proxy = new WebProxy("http://205.144.171.10:8443");
        //    //HttpClient.DefaultProxy = Proxy;
        //    HttpResponseMessage response = httpClient.GetAsync(url).Result;

        //    return await response.Content.ReadAsStringAsync();
        //}

        private async Task<string> ExecQueryAsync(string token, string ApiCommand)
        {
            string url = UrlBase + token + "/" + ApiCommand;

            var httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.GetAsync(url);

            return await response.Content.ReadAsStringAsync();
        }

        //private void GetMethods(object obj)
        //{
        //    Type objType = obj.GetType();
        //    PropertyInfo[] propInfos = objType.GetProperties(BindingFlags.Public | BindingFlags.DeclaredOnly);
        //    //propInfos[0].GetValue
        //}

        // 1273497115:AAFO8svVdoT-Y_dAXHF3zfQu9XH5Y6yiHIY

        public async Task<IActionResult> GetMe(string token)
        {
            //ITelegramBotClient Bot = new TelegramBotClient(token);
            //User user = Bot.GetMeAsync().Result;
            //Bot.GetWebhookInfoAsync().Result
            //return Content(user.ToString());

            //var resp = await ExecQueryAsync(token, "GetMe");
            //var x = JsonConvert.SerializeObject(resp, Formatting.Indented);
            //return View("Main", resp);

            ITelegramBotClient Bot = new TelegramBotClient(token, new WebProxy("127.0.0.1:8888"));
            User user = Bot.GetMeAsync().Result;
            UserProfilePhotos userProfilePhotos = await Bot.GetUserProfilePhotosAsync(user.Id);
            File file = await Bot.GetFileAsync(userProfilePhotos.Photos[0][0].FileId);

            return View("Main", file.FilePath);
        }

        public async Task<IActionResult> GetBotPictLink(string token)
        {
            ITelegramBotClient Bot = new TelegramBotClient(token, new WebProxy("127.0.0.1:8888"));
            User user = await Bot.GetMeAsync();
            UserProfilePhotos userProfilePhotos = await Bot.GetUserProfilePhotosAsync(user.Id);
            File file = await Bot.GetFileAsync(userProfilePhotos.Photos[0][0].FileId);
            return View("Main", file.FilePath);
        }

    }
}
