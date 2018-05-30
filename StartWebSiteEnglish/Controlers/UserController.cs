using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using StartWebSiteEnglish.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Net;
using StartWebSiteEnglish.ApiClasses;
using System.Net.Mail;
using Newtonsoft.Json;
using System.Text;
using Newtonsoft.Json.Linq;

namespace StartWebSiteEnglish.Controlers
{
    public class UserController : Controller
    {
        

        private ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }


        [Authorize]
        public ActionResult UserPage()
        {
            ApplicationUser user = (ApplicationUser)Session["User"];
            return View();
        }

        public ActionResult Setting()
        {
            return View();
        }

        public ActionResult Dictionary()
        {
            //MaterialContext db = new MaterialContext();
            //var mattex = from s in db.Words select s;
            return View();
        }

        [HttpGet]
        public ActionResult RenameUserName()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> EditPassword(EditPassword model)
        {
            HttpCookie userIdCookie = Request.Cookies["IdCookie"];


            if (ModelState.IsValid && userIdCookie != null)
            {
                var user = await this.UserManager.FindByIdAsync(userIdCookie.Value);
                if (user != null)
                {
                    var result = await this.UserManager.ChangePasswordAsync(userIdCookie.Value, model.Password, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Setting");
                    }
                    else
                    {
                        foreach (string error in result.Errors)
                        {
                            ModelState.AddModelError("", error);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Пользователь не найден");
                }
            }
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> EditEmail(string newEmail)
        {
            ApplicationUser user = (ApplicationUser)Session["User"];
            if (user != null)
            {
                var result = await UserManager.SetEmailAsync(user.Id, newEmail);
                if (result.Succeeded)
                {
                    return RedirectToAction("Setting");
                }
                else
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Пользователь не найден");
            }
            return View();
        }

        [HttpPost]
        public ActionResult EditUserName(string newUserName)
        {
            ApplicationUser user = (ApplicationUser)Session["User"];
            if (user != null)
            {
                user.UserName = newUserName;
                var result = UserManager.Update(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Setting");
                }
                else
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Пользователь не найден");
            }
            return View();
        }


        static int appId = 6477544;
        static int groupId = 166392001;
        static int userId = 487648872;
        static string token = "06be11fb31f5a4f68ff3cf1db3f336c03df90b87471cf5a3913b778a4e80ebb3eef6acabb1b72698fc14f";

        [HttpPost]
        public async Task<ActionResult> Upload(HttpPostedFileBase upload)
        {
            ApplicationUser user = (ApplicationUser)Session["User"];
            if (user != null)
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

                var outUser = await UserManager.FindByIdAsync(user.Id);

                outUser.PhotoUrl = pictureUrl;

                var result = await UserManager.UpdateAsync(outUser);

                if (result.Succeeded)
                {
                    Session["User"] = outUser;
                    return RedirectToAction("UserPage");
                }
                else
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }
            }

            return RedirectToAction("UserPage");
        }
    }
}