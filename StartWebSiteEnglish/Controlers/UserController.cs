﻿using System;
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
            var words= LearnWords(user.UserName);
            return View(words);
        }

        private List<Words> LearnWords(string userName)
        {
            List<Words> words = new List<Words>();
            var idwords = SqlQueries.ReadWordDatabase(userName);
            using(MaterialContext db = new MaterialContext())
            {
                foreach(var x in idwords)
                {
                    words.Add(db.Words.FirstOrDefault(s => s.Id == x.Id));
                }
            }
            ViewData["LearnUserWords"] = words;
            return words;
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
        public async Task<ActionResult> EditPassword(EditPassword model, ApplicationUser user=null)
        {
            //HttpCookie userIdCookie = Request.Cookies["IdCookie"];
            if (user == null)
            {
                 user = (ApplicationUser)Session["User"];
            }

            if (ModelState.IsValid)
            {
                var BDuser = await this.UserManager.FindByIdAsync(user.Id);
                if (BDuser != null)
                {
                    var result = await this.UserManager.ChangePasswordAsync(BDuser.Id, model.Password, model.NewPassword);
                    if (result.Succeeded)
                    {
                        ViewBag.ChangePassword = "Пароль изменён";

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
            ViewBag.ChangePassword = "Неудалось изменить пароль";
            return View("Setting");
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
                    ViewBag.ChangeEmail = "Email изменён";
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
            ViewBag.ChangeEmail = "Email не изменён";
            return View();
        }

        [HttpPost]
        public ActionResult EditUserName(string newUserName)
        {
            ApplicationUser user = (ApplicationUser)Session["User"];
            if (user != null)
            {
                SqlQueries.RenameDatabases(user.UserName,newUserName );
                user.UserName = newUserName;
                var result = UserManager.Update(user);
                if (result.Succeeded)
                {
                    ViewBag.ChangeLogin = "Логин изменён";
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
            ViewBag.ChangeLogin = "Логие не изменён";
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


        public ActionResult DeleteWord(int id)
        {
            var user = (ApplicationUser)Session["User"];
            SqlQueries.DeteleWordDatabase(user.UserName, id);
            return Json("Delete sucsess", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult WordTranslate(int[] id)
        {
            ApplicationUser user = Session["User"] as ApplicationUser;
            SetPXLevel(id.Length);
            for (int i = 0; i < id.Length; i++)
            {
                SqlQueries.AddIdToWordDatabase(user.UserName, id[i]);
            }
            return Json(new { response = "Sucsses" }, JsonRequestBehavior.AllowGet);
        }

        private  void SetPXLevel(int count)
        {
            ApplicationUser user = Session["User"] as ApplicationUser;
            if (user != null)
            {

                var outUser =  UserManager.FindById(user.Id);
                outUser.LevelProgress += count;
                
                UserManager.Update(outUser);
               Session["User"] = outUser;
            }
        }

        public ActionResult IsReading(int id)
        {
            ApplicationUser user = Session["User"] as ApplicationUser;
            SqlQueries.AddIdToMaterialTextDatabase(user.UserName, id);
            var material = (MaterialText)Session["TextReading"];
            SetPXLevel(5);
            ViewBag.IsReading = true;
            return View("TextReading", material);
        }
    }
}