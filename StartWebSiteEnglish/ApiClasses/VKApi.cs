using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Text;

namespace StartWebSiteEnglish.ApiClasses
{
    public class VKApi
    {
        static int appId = 6477544;
        static int groupId = 166392001;
        static int userId = 487648872;
        static string token = "06be11fb31f5a4f68ff3cf1db3f336c03df90b87471cf5a3913b778a4e80ebb3eef6acabb1b72698fc14f";

        public string UploadPhoto(HttpPostedFileBase upload)
        {
            string filename = upload.FileName;
            var client = new WebClient();

            var urlForServer = "https://api.vk.com/method/photos.getWallUploadServer?access_token=" + token + "&v=5.74";
            var reqForServer = client.DownloadString(urlForServer);
            var jsonForServer = JsonConvert.DeserializeObject(reqForServer) as JObject;

            var urlUploadServer = jsonForServer["response"]["upload_url"].ToString();
            var reqUploadServer = Encoding.UTF8.GetString(client.UploadFile(urlUploadServer, "POST", filename));
            var jsonUploadServer = JsonConvert.DeserializeObject(reqUploadServer) as JObject;

            var urlSavePhoto = "https://api.vk.com/method/photos.saveWallPhoto?access_token=" + token
                     + "&server=" + jsonUploadServer["server"]
                     + "&photo=" + jsonUploadServer["photo"]
                     + "&hash=" + jsonUploadServer["hash"]
                     + "&v=5.74";
            var reqSavePhoto = client.DownloadString(urlSavePhoto);
            var jsonSavePhoto = JsonConvert.DeserializeObject(reqSavePhoto) as JObject;

            var pictureUrl = jsonSavePhoto["response"][0]["photo_1280"].ToString();

            return pictureUrl;

            //вика, вставь это во фронт енд части
            //@using (Html.BeginForm("UploadPhoto", "VKApi", FormMethod.Post, new { enctype = "multipart/form-data" }))
            //{
            //<input type="file" name="upload"/> <br>
            //<input type="submit" value="Загрузить" />
            //}

        }
    }
}