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
using System.Net.Mail;

namespace StartWebSiteEnglish.Controlers
{
    public class HomeController : Controller
    {
        // GET: Home
        private ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Index()
        {
            if (Session["User"] != null)
            {
                var user = Session["User"] as ApplicationUser;
                return RedirectToAction("Main", "Main");
            }
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    EmailConfirmed = false,
                    DateRegistration = DateTime.Now,
                    PhotoUrl = "https://zalatina.myhappyco.com/images/img-profile.png"
                };
                if(model.Password != model.ConfirmPassword)
                {
                    ViewBag.RegisterError = true;
                    ModelState.AddModelError("", "Неверно подтверждён пароль");
                }
                //добавление пользователя
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    //наш email с заголовком письма
                    MailAddress from = new MailAddress("kuzya2k123@gmail.com", "Web Registration");
                    // кому отправляем
                    MailAddress to = new MailAddress(user.Email);
                    // создаем объект сообщения
                    MailMessage mail = new MailMessage(from, to);
                    // тема письма
                    mail.Subject = "Email confirmation";
                    // текст письма - включаем в него ссылку
                    mail.Body = string.Format("Для завершения регистрации перейдите по ссылке:" +
                                    "<a href=\"{0}\" title=\"Подтвердить регистрацию\">{0}</a>",
                        Url.Action("ConfirmEmail", "Home", new { Token = user.Id, Email = user.Email }, Request.Url.Scheme));
                    mail.IsBodyHtml = true;
                    // адрес smtp-сервера, с которого мы и будем отправлять письмо
                    SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                    // логин и пароль
                    smtp.Credentials = new NetworkCredential("englishsitepel@gmail.com", "205114qa");
                    smtp.EnableSsl = true;
                    smtp.Send(mail);

                    await UserManager.AddToRoleAsync(user.Id, "User");

                    SqlQueries.CreateDatabases(user.UserName);
                    return PartialView("ResultRegister");
                }
                else
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                        ViewBag.RegisterError = true;
                    }
                    return View("Index");
                }
            }
            return View("Index");
        }

        [AllowAnonymous]
        public string Confirm(string Email)
        {
            return "На почтовый адрес " + Email + " Вам высланы дальнейшие" +
                    "инструкции по завершению регистрации";
        }

        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string Token, string Email)
        {
            ApplicationUser user = this.UserManager.FindById(Token);
            if (user != null)
            {
                if (user.Email == Email)
                {
                    user.EmailConfirmed = true;
                    await UserManager.UpdateAsync(user);
                    ClaimsIdentity claim = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
                    AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = true }, claim);
                    //await AuthenticationManager.SignIn(user);

                    Session["User"] = user;
                    Session["UserRole"] = UserManager.IsInRole(user.Id, "Admin");
                    return RedirectToAction("Main", "Main", new { ConfirmedEmail = user.Email });
                }
                else
                {
                    return RedirectToAction("Confirm", "Home", new { Email = user.Email });
                }
            }
            else
            {
                return RedirectToAction("Confirm", "Home", new { Email = "" });
            }
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await UserManager.FindAsync(model.UserName, model.Password);
                if (user != null)
                {
                    if (user.EmailConfirmed == true)
                    {
                        ClaimsIdentity claim = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
                        AuthenticationManager.SignOut();
                        AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = true }, claim);

                        Session["User"] = user;
                        Session["UserRole"] = UserManager.IsInRole(user.Id, "Admin");
                        FormsAuthentication.SetAuthCookie(model.UserName, true);
                        return RedirectToAction("Main", "Main");
                    }
                    else
                    {
                        ViewBag.LoginError = true;
                        ModelState.AddModelError("", "Не подтвержден email.");
                    }
                }
                else
                {
                    ViewBag.LoginError = true;
                    ModelState.AddModelError("", "Неверный логин или пароль");
                }
            }
            return View("Index");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByEmailAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    return View("ForgotPasswordConfirmation");
                }
                string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                var callbackUrl = Url.Action("ResetPassword", "Account",
                    new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                await UserManager.SendEmailAsync(user.Id, "Сброс пароля",
                    "Для сброса пароля, перейдите по ссылке <a href=\"" + callbackUrl + "\">сбросить</a>");
                return RedirectToAction("ForgotPasswordConfirmation", "Home");
            }
            return View(model);
        }


        [Authorize]
        public ActionResult Exid()
        {
            Session["User"] = null;

            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
    }


    //<add name = "UsersDB" connectionString="Data Source=ZENBOOK-UX510\SQLEXPRESS;AttachDbFilename='|DataDirectory|\AuthUsers.mdf" providerName="System.Data.SqlClient" />
    //<add name = "EnglishSiteDB" connectionString="Data Source=VIKA-PC\SQLVIKA;Integrated Security=True" providerName="System.Data.SqlClient" />
}