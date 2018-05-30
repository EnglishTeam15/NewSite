using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System.Data.Entity;

namespace StartWebSiteEnglish.Models
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime DateRegistration { get; set; }
        public string PhotoUrl { get; set; }
        public int Сomplexity { get; set; }
        public int LevelProgress { get; set; }
        public int Age { get; set; }
    }

    public class ApplicationRole : IdentityRole
    {

    }

    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationContext() :
            base("SiteEnglishCloudDB")
        { }


        public static ApplicationContext Create()
        {
            return new ApplicationContext();
        }
    }

    public class ApplicationDbInitializer : DropCreateDatabaseAlways<ApplicationContext>
    {
        protected override void Seed(ApplicationContext context)
        {
            var roleManager = new RoleManager<ApplicationRole>(new RoleStore<ApplicationRole>(context));

            var role1 = new ApplicationRole { Name = "Admin" };
            var role2 = new ApplicationRole { Name = "User" };

            roleManager.Create(role1);
            roleManager.Create(role2);

            base.Seed(context);
        }
    }

    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            ApplicationContext db = context.Get<ApplicationContext>();
            ApplicationUserManager manager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
            return manager;
        }
    }
}