using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using StartWebSiteEnglish.Models;
using System.Net.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using MimeKit;
using MailKit.Net.Smtp;

namespace StartWebSiteEnglish.App_Start
{
    public class EmailService : IIdentityMessageService
    {

        public Task SendAsync(IdentityMessage message)
        {
            // настройка логина, пароля отправителя
            var from = "somemail@yandex.ru";
            var pass = "password";

            // адрес и порт smtp-сервера, с которого мы и будем отправлять письмо
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient("smtp.yandex.ru", 25);

            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(from, pass);
            client.EnableSsl = true;

            // создаем письмо: message.Destination - адрес получателя
            var mail = new MailMessage(from, message.Destination);
            mail.Subject = message.Subject;
            mail.Body = message.Body;
            mail.IsBodyHtml = true;

            return client.SendMailAsync(mail);
        }

        //public Task SendAsync(IdentityMessage message)
        //{
        //    // настройка логина, пароля отправителя
        //    var from = "somemail@yandex.ru";
        //    var pass = "password12";

        //    // адрес и порт smtp-сервера, с которого мы и будем отправлять письмо
        //    SmtpClient client = new SmtpClient("smtp.yandex.ru", 25);

        //    client.DeliveryMethod = SmtpDeliveryMethod.Network;
        //    client.UseDefaultCredentials = false;
        //    client.Credentials = new System.Net.NetworkCredential(from, pass);
        //    client.EnableSsl = true;

        //    // создаем письмо: message.Destination - адрес получателя
        //    var mail = new MailMessage(from, message.Destination);
        //    mail.Subject = message.Subject;
        //    mail.Body = message.Body;
        //    mail.IsBodyHtml = true;

        //    return client.SendMailAsync(mail);
        //}

        //public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        //{
        //    var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationContext>()));

        //    manager.EmailService = new EmailService();
        //    //manager.SmsService = new SmsService();
        //    return manager;
        //}
    }
}